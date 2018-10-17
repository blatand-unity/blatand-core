using System;
using System.Collections.Generic;
using System.Linq;
using Blatand.Utils;
using UnityEngine;
using UnityEngine.Assertions;

namespace Blatand.Core.Android
{
    public class Gatt : AAndroidObjectWrapper
    {
        internal class InternalGattCallback : GattCallback
        {
            /// <summary>
            /// Gatt linked with this callabacks
            /// </summary>
            Gatt m_source;

			public InternalGattCallback(Gatt source) : base() {
				m_source = source;
			}

            public override void OnServicesDiscovered(/* BluetoothGatt */ AndroidJavaObject gatt, int status)
            {
                UnityMainThreadDispatcher.Instance().Enqueue(() => {
                    m_source.OnServicesDiscovered();
                });
            }

            public override void OnCharacteristicRead(/* BluetoothGatt */ AndroidJavaObject gatt,
                                                      /* BluetoothGattCharacteristic */ AndroidJavaObject androidCharacteristic,
                                                      int status)
            {
                m_source.PerformNext();
                Uuid serviceID = Uuid.FromJavaObject(androidCharacteristic.Call<AndroidJavaObject>("getService").Call<AndroidJavaObject>("getUuid"));
                GattCharacteristic characteristic = m_source.FindService(serviceID).FindCharacteristic(Uuid.FromJavaObject(androidCharacteristic.Call<AndroidJavaObject>("getUuid")));
                UnityMainThreadDispatcher.Instance().Enqueue(() => {
                    characteristic.TriggerOnValueRead();
                });
            }

            public override void OnCharacteristicChanged(/* BluetoothGatt */ AndroidJavaObject gatt,
                                                         /* BluetoothGattCharacteristic */ AndroidJavaObject androidCharacteristic)
            {
                Uuid serviceID = Uuid.FromJavaObject(androidCharacteristic.Call<AndroidJavaObject>("getService").Call<AndroidJavaObject>("getUuid"));
                GattCharacteristic characteristic = m_source.FindService(serviceID).FindCharacteristic(Uuid.FromJavaObject(androidCharacteristic.Call<AndroidJavaObject>("getUuid")));
                UnityMainThreadDispatcher.Instance().Enqueue(() => {
                    characteristic.TriggerOnValueChanged();
                });
            }

            public override void OnCharacteristicWrite(/* BluetoothGatt */ AndroidJavaObject gatt,
                                                       /* BluetoothGattCharacteristic */ AndroidJavaObject androidCharacteristic,
                                                       int status)
            {
                m_source.PerformNext();
            }

            public override void OnDescriptorWrite(/* BluetoothGatt */ AndroidJavaObject gatt,
                                                   /* BluetoothGattDescriptor */ AndroidJavaObject androidDescriptor,
                                                   int status)
            {
                m_source.PerformNext();
            }

            public override void OnConnectionStateChange(/* BluetoothGatt */ AndroidJavaObject gatt,
                                                         int status,
                                                         int newState)
            {
                if(newState == 2) {
                    UnityMainThreadDispatcher.Instance().Enqueue(() => {
                        m_source.OnConnected(m_source);
                    });
                } else if(newState == 0) {
                    UnityMainThreadDispatcher.Instance().Enqueue(() => {
                        m_source.OnDisconnected(m_source);
                    });
                }
            }
        }
        internal /* GattCallbackInterfacer */ AndroidJavaObject m_callbacks;

        // Caching is neccessary for event persistence
        Dictionary<string, GattService> m_services;

        public GattService[] Services {
            get {
                /* List<BluetoothGattService> */ AndroidJavaObject serviceList = m_androidObject.Call<AndroidJavaObject>("getServices");

                int listCount = serviceList.Call<int>("size");
                for(int i = 0; i < listCount; i++) {
                    AndroidJavaObject service = serviceList.Call<AndroidJavaObject>("get", i);

                    GattService gattService = new GattService(service, this);
                    string serviceID = gattService.Uuid.ToString();

                    if(!m_services.ContainsKey(serviceID)) {
                        m_services.Add(serviceID, gattService);
                    }
                }

                return m_services.Values.ToArray();
            }
        }

        private Queue<Action> m_requestQueue;
        private bool m_isPerformingActions = false;

        #region Events

        public event Action OnServicesDiscovered;

        public event Action<Gatt> OnConnected;

        public event Action<Gatt> OnDisconnected;

        #endregion

        internal Gatt() : base ()
        {
            m_services = new Dictionary<string, GattService>();
            m_requestQueue = new Queue<Action>();
        }

        public GattService FindService(Uuid uuid) {
            string uuidStr = uuid.ToString();

            if(m_services.ContainsKey(uuidStr)) {
                return m_services[uuidStr];
            } else {
                Assert.IsNotNull(AndroidObject);
                /* BluetoothGattService */ AndroidJavaObject service = AndroidObject.Call<AndroidJavaObject>("getService", uuid.ToJavaObject());

                if(service == null) {
                    return null;
                }

                var gattService = new GattService(service, this);
                m_services.Add(uuidStr, gattService);
                return gattService;
            }
        }

        public GattService FindService(ServiceDefinition serviceDef) {
            return FindService(serviceDef.Id);
        }

        public GattCharacteristic FindCharacteristic(CharacteristicDefinition characteristicDef) {

            var service = FindService(characteristicDef.ServiceDefinition);

            if(service == null) {
                return null;
            }

            return service.FindCharacteristic(characteristicDef.Id);
        }

        public void DiscoverServices() {
            Assert.IsNotNull(AndroidObject);
            AndroidObject.Call<bool>("discoverServices");
        }

        public void Close() {
            Assert.IsNotNull(AndroidObject);
            AndroidObject.Call("close");
        }

        /// <summary>
        /// Reads the requested characteristic from the associated remote device.
        ///
        /// This is an asynchronous operation. The result of the read operation is reported by the BluetoothGattCallback.onCharacteristicRead(BluetoothGatt, BluetoothGattCharacteristic, int) callback.
        /// </summary>
        /// <param name="characteristic">Characteristic to read from the remote device</param>
        /// <seealso href="https://developer.android.com/reference/android/bluetooth/BluetoothGatt#readCharacteristic(android.bluetooth.BluetoothGattCharacteristic)"/>
        public void ReadCharacteristic(GattCharacteristic characteristic) {
            AddRequest(() => {
                if(!AndroidObject.Call<bool>("readCharacteristic", characteristic.AndroidObject)) {
                    PerformNext();
                }
            });
        }

        /// <summary>
        /// Writes a given characteristic and its values to the associated remote device.
        /// </summary>
        /// <seealso href="https://developer.android.com/reference/android/bluetooth/BluetoothGatt.html#writeCharacteristic(android.bluetooth.BluetoothGattCharacteristic)">

        public void WriteCharacteristic(GattCharacteristic characteristic) {
            AddRequest(() => {
                if(!AndroidObject.Call<bool>("writeCharacteristic", characteristic.AndroidObject)) {
                    PerformNext();
                }
            });
        }

        private void AddRequest(Action request)
        {
            m_requestQueue.Enqueue(request);
            if(!m_isPerformingActions) {
                PerformNext();
            }
        }

        private void PerformNext()
        {
            if(m_requestQueue.Count > 0) {
                var action = m_requestQueue.Dequeue();
                action();
                m_isPerformingActions = true;
            } else {
                m_isPerformingActions = false;
            }
        }

        /// <summary>
        /// Enable or disable notifications/indications for a given characteristic.
        ///
        /// Once notifications are enabled for a characteristic, a BluetoothGattCallback.onCharacteristicChanged(BluetoothGatt, BluetoothGattCharacteristic) callback will be triggered if the remote device indicates that the given characteristic has changed.
        /// </summary>
        /// <param name="characteristic">The characteristic for which to enable notifications</param>
        /// <param name="enable">Set to true to enable notifications/indications</param>
        /// <returns>true, if the requested notification status was set successfully </returns>
        /// <seealso href="https://developer.android.com/reference/android/bluetooth/BluetoothGatt#setCharacteristicNotification(android.bluetooth.BluetoothGattCharacteristic,%20boolean)"/>
        public bool SetCharacteristicNotification(GattCharacteristic characteristic, bool enable)
        {
            Assert.IsNotNull(AndroidObject);
            Assert.IsNotNull(characteristic.AndroidObject);

            var BluetoothGattDescriptorClass = new AndroidJavaClass("android.bluetooth.BluetoothGattDescriptor");

            byte[] notificationValue;
            if(enable) {
                notificationValue = BluetoothGattDescriptorClass.GetStatic<byte[]>("ENABLE_NOTIFICATION_VALUE");
            } else {
                notificationValue = BluetoothGattDescriptorClass.GetStatic<byte[]>("DISABLE_NOTIFICATION_VALUE");
            }

            if(AndroidObject.Call<bool>("setCharacteristicNotification", characteristic.AndroidObject, enable)) {
                /* BluetoothGattDescriptor */ var descriptor = characteristic.AndroidObject.Call<AndroidJavaObject>("getDescriptor", GattCharacteristic.CLIENT_CHARACTERISTIC_CONFIGURATION.ToJavaObject());
                if(descriptor.Call<bool>("setValue", notificationValue)) {
                    Debug.Log("Permissions: " + descriptor.Call<int>("getPermissions"));

                    AddRequest(() => {
                        if(!AndroidObject.Call<bool>("writeDescriptor", descriptor)) {
                            PerformNext();
                        }
                    });
                }
            }
            return false;
        }
    }
}

