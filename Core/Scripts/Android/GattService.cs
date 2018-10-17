using System.Collections.Generic;
using System.Linq;
using Blatand.Utils;
using UnityEngine;
using UnityEngine.Assertions;

namespace Blatand.Core.Android
{
    public class GattService : AAndroidObjectWrapper
    {
        private Uuid m_uuid;
        public Uuid Uuid {
            get {
                if(m_uuid != null) {
                    return m_uuid;
                }
                return m_uuid = Uuid.FromJavaObject(m_androidObject.Call<AndroidJavaObject>("getUuid"));
            }
        }

        // Caching is neccessary for event persistence
        Dictionary<string, GattCharacteristic> m_characteristics;

        public GattCharacteristic[] Characteristics {
            get {
                /* List<BluetoothGattCharacteristic> */ AndroidJavaObject characteristicList = m_androidObject.Call<AndroidJavaObject>("getCharacteristics");

                int listCount = characteristicList.Call<int>("size");
                for(int i = 0; i < listCount; i++) {
                    AndroidJavaObject characteristic = characteristicList.Call<AndroidJavaObject>("get", i);

                    GattCharacteristic gattCharac = new GattCharacteristic(characteristic, this);
                    string serviceID = gattCharac.Uuid.ToString();

                    if(!m_characteristics.ContainsKey(serviceID)) {
                        m_characteristics.Add(serviceID, gattCharac);
                    }
                }

                return m_characteristics.Values.ToArray();
            }
        }

        public Gatt Gatt {
            get; private set;
        }

        internal GattService(AndroidJavaObject androidObject, Gatt gatt) : base (androidObject)
        {
            m_characteristics = new Dictionary<string, GattCharacteristic>();
            Gatt = gatt;
        }

        public GattCharacteristic FindCharacteristic(Uuid uuid)
        {
            string uuidStr = uuid.ToString();

            if(m_characteristics.ContainsKey(uuidStr)) {
                return m_characteristics[uuidStr];
            } else {
                Assert.IsNotNull(AndroidObject);
                /* BluetoothGattCharacteristic */ AndroidJavaObject characteristic = m_androidObject.Call<AndroidJavaObject>("getCharacteristic", uuid.ToJavaObject());

                if(characteristic == null) {
                    return null;
                }

                var gattcharacteristic = new GattCharacteristic(characteristic, this);
                m_characteristics.Add(uuidStr, gattcharacteristic);
                return gattcharacteristic;
            }
        }
    }
}

