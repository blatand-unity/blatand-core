using System;
using Blatand.Utils;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace Blatand.Core.Android
{
    public class GattCharacteristic : AAndroidObjectWrapper
    {
        private static AndroidJavaClass m_androidClass = new AndroidJavaClass("android.bluetooth.BluetoothGattCharacteristic");

        /// <summary>
        /// UUID of the "Client Characteristic Configuration" descriptor which defines how the characteristic may be
        /// configured by a specific client.
        ///
        /// It is a constant value available in GATT specifications: 0x2902
        /// </summary>
        /// <returns>UUID of the "Client Characteristic Configuration" descriptor</returns>
        /// <seealso href="https://www.bluetooth.com/specifications/gatt/viewer?attributeXmlFile=org.bluetooth.descriptor.gatt.client_characteristic_configuration.xml">
        public static readonly Uuid CLIENT_CHARACTERISTIC_CONFIGURATION = new Uuid("00002902-0000-1000-8000-00805f9b34fb");

        private Uuid m_uuid;
        /// <summary>
        /// Returns the UUID of this characteristic
        /// </summary>
        /// <value>UUID of this characteristic</value>
        /// <seealso href="https://developer.android.com/reference/android/bluetooth/BluetoothGattCharacteristic#getUuid()">
        public Uuid Uuid {
            get {
                if(m_uuid != null) {
                    return m_uuid;
                }
                return m_uuid = Uuid.FromJavaObject(m_androidObject.Call<AndroidJavaObject>("getUuid"));
            }
        }

        #region Value

        public class GattCharacteristicValue
        {
            protected GattCharacteristic m_characteristic;

            internal GattCharacteristicValue(GattCharacteristic characteristic)
            {
                m_characteristic = characteristic;
            }

            /// <summary>
            /// Set the locally stored value of this characteristic.
            ///
            /// See <see cref="SetBytes">SetBytes()</see> for details.
            /// </summary>
            /// <param name="value">New value for this characteristic</param>
            /// <returns>true if the locally stored value has been set </returns>
            /// <seealso href="https://developer.android.com/reference/android/bluetooth/BluetoothGattCharacteristic.html#setValue(java.lang.String)"/>
            public bool SetString(string value)
            {
                Assert.IsNotNull(m_characteristic.AndroidObject);
                return m_characteristic.AndroidObject.Call<bool>("setValue", value);
            }

            /// <summary>
            /// Return the stored value of this characteristic.
            /// </summary>
            /// <param>Offset at which the string value can be found.</param>
            /// <returns>Cached value of the characteristic</returns>
            /// <seealso href="https://developer.android.com/reference/android/bluetooth/BluetoothGattCharacteristic#getStringValue(int)"/>
            public string ToString(int offset = 0)
            {
                Assert.IsNotNull(m_characteristic.AndroidObject);
                return m_characteristic.AndroidObject.Call<string>("getStringValue", offset);
            }

            #region Integer

            private static int m_FORMAT_SINT16 = 0;
            /// <summary>
            /// Characteristic value format type sint16
            /// </summary>
            /// <seealso href="https://developer.android.com/reference/android/bluetooth/BluetoothGattCharacteristic#FORMAT_SINT16"/>
            public static int FORMAT_SINT16 {
                get {
                    if(m_FORMAT_SINT16 != 0) {
                        return m_FORMAT_SINT16;
                    }
                    return m_FORMAT_SINT16 = m_androidClass.GetStatic<int>("FORMAT_SINT16");
                }
            }

            private static int m_FORMAT_SINT32 = 0;
            /// <summary>
            /// Characteristic value format type sint32
            /// </summary>
            /// <seealso href="https://developer.android.com/reference/android/bluetooth/BluetoothGattCharacteristic#FORMAT_SINT32"/>
            public static int FORMAT_SINT32 {
                get {
                    if(m_FORMAT_SINT32 != 0) {
                        return m_FORMAT_SINT32;
                    }
                    return m_FORMAT_SINT32 = m_androidClass.GetStatic<int>("FORMAT_SINT32");
                }
            }

            private static int m_FORMAT_SINT8 = 0;
            /// <summary>
            /// Characteristic value format type sint8
            /// </summary>
            /// <seealso href="https://developer.android.com/reference/android/bluetooth/BluetoothGattCharacteristic#FORMAT_SINT8"/>
            public static int FORMAT_SINT8 {
                get {
                    if(m_FORMAT_SINT8 != 0) {
                        return m_FORMAT_SINT8;
                    }
                    return m_FORMAT_SINT8 = m_androidClass.GetStatic<int>("FORMAT_SINT8");
                }
            }

            private static int m_FORMAT_UINT16 = 0;
            /// <summary>
            /// Characteristic value format type uint16
            /// </summary>
            /// <seealso href="https://developer.android.com/reference/android/bluetooth/BluetoothGattCharacteristic#FORMAT_UINT16"/>
            public static int FORMAT_UINT16 {
                get {
                    if(m_FORMAT_UINT16 != 0) {
                        return m_FORMAT_UINT16;
                    }
                    return m_FORMAT_UINT16 = m_androidClass.GetStatic<int>("FORMAT_UINT16");
                }
            }

            private static int m_FORMAT_UINT32 = 0;
            /// <summary>
            /// Characteristic value format type uint32
            /// </summary>
            /// <seealso href="https://developer.android.com/reference/android/bluetooth/BluetoothGattCharacteristic#FORMAT_UINT32"/>
            public static int FORMAT_UINT32 {
                get {
                    if(m_FORMAT_UINT32 != 0) {
                        return m_FORMAT_UINT32;
                    }
                    return m_FORMAT_UINT32 = m_androidClass.GetStatic<int>("FORMAT_UINT32");
                }
            }

            private static int m_FORMAT_UINT8 = 0;
            /// <summary>
            /// Characteristic value format type uint8
            /// </summary>
            /// <seealso href="https://developer.android.com/reference/android/bluetooth/BluetoothGattCharacteristic#FORMAT_UINT8"/>
            public static int FORMAT_UINT8 {
                get {
                    if(m_FORMAT_UINT8 != 0) {
                        return m_FORMAT_UINT8;
                    }
                    return m_FORMAT_UINT8 = m_androidClass.GetStatic<int>("FORMAT_UINT8");
                }
            }

            /// <summary>
            /// Set the locally stored value of this characteristic.
            /// See <see cref="SetBytes">SetBytes</see> for details.
            /// </summary>
            /// <param name="formatType">New value for this characteristic</param>
            /// <param name="formatType">Integer format type used to transform the value parameter</param>
            /// <param name="offset">Offset at which the value should be placed</param>
            /// <returns>true if the locally stored value has been set </returns>
            /// <seealso href="https://developer.android.com/reference/android/bluetooth/BluetoothGattCharacteristic#setValue(int,%20int,%20int)"/>
            public bool SetInt(int value, int formatType, int offset = 0)
            {
                Assert.IsNotNull(m_characteristic.AndroidObject);
                return m_characteristic.AndroidObject.Call<bool>("setValue", value, formatType, offset);
            }

            /// <summary>
            /// Return the stored value of this characteristic.
            ///
            /// The formatType parameter determines how the characteristic value is to be interpreted. For example,
            /// setting formatType to <see cref="FORMAT_UINT16">FORMAT_UINT16</see specifies that the first two bytes
            /// of the characteristic value at the given offset are interpreted to generate the return value.
            /// </summary>
            /// <param name="formatType">The format type used to interpret the characteristic value.</param>
            /// <param name="offset">Offset at which the integer value can be found.</param>
            /// <returns>Cached value of the characteristic or null of offset exceeds value size.</returns>
            /// <seealso href="https://developer.android.com/reference/android/bluetooth/BluetoothGattCharacteristic#getIntValue(int,%20int)"/>
            public int ToInt(int formatType, int offset = 0)
            {
                Assert.IsNotNull(m_characteristic.AndroidObject);
                return m_characteristic.AndroidObject.Call</* Integer */ AndroidJavaObject>("getIntValue", formatType, offset)
                    .Call<int>("intValue");
            }

            #endregion

            /// <summary>
            /// This function modifies the locally stored cached value of this characteristic. To send the value to the
            /// remote device, call <see cref="Gatt.WriteCharacteristic">Gatt.WriteCharacteristic</see> to send the
            /// value to the remote device.
            /// </summary>
            /// <param name="value">New value for this characteristic</param>
            /// <returns>true if the locally stored value has been set, false if the requested value could not be stored locally. </returns>
            /// <seealso href="https://developer.android.com/reference/android/bluetooth/BluetoothGattCharacteristic.html#setValue(byte[])"/>
            public bool SetBytes(byte[] value)
            {
                Assert.IsNotNull(m_characteristic.AndroidObject);
                return m_characteristic.AndroidObject.Call<bool>("setValue", value);
            }

            /// <summary>
            /// Get the stored value for this characteristic.
            ///
            /// This function returns the stored value for this characteristic as retrieved by calling
            /// BluetoothGatt.readCharacteristic(BluetoothGattCharacteristic). The cached value of the characteristic is
            /// updated as a result of a read characteristic operation or if a characteristic update notification has
            /// been received.
            /// </summary>
            /// <returns>Cached value of the characteristic</returns>
            /// <seealso href="https://developer.android.com/reference/android/bluetooth/BluetoothGattCharacteristic#getValue()"/>
            public byte[] ToBytes()
            {
                Assert.IsNotNull(m_characteristic.AndroidObject);
                return m_characteristic.AndroidObject.Call<byte[]>("getValue");
            }

            #region Float

            private static int m_FORMAT_FLOAT = 0;
            /// <summary>
            /// Characteristic value format type float (32-bit float)
            /// </summary>
            /// <seealso href="https://developer.android.com/reference/android/bluetooth/BluetoothGattCharacteristic#FORMAT_FLOAT"/>
            public static int FORMAT_FLOAT {
                get {
                    if(m_FORMAT_FLOAT != 0) {
                        return m_FORMAT_FLOAT;
                    }
                    return m_FORMAT_FLOAT = m_androidClass.GetStatic<int>("FORMAT_FLOAT");
                }
            }

            private static int m_FORMAT_SFLOAT = 0;
            /// <summary>
            /// Characteristic value format type sfloat (16-bit float)
            /// </summary>
            /// <seealso href="https://developer.android.com/reference/android/bluetooth/BluetoothGattCharacteristic#FORMAT_SFLOAT"/>
            public static int FORMAT_SFLOAT {
                get {
                    if(m_FORMAT_SFLOAT != 0) {
                        return m_FORMAT_SFLOAT;
                    }
                    return m_FORMAT_SFLOAT = m_androidClass.GetStatic<int>("FORMAT_SFLOAT");
                }
            }

            /// <summary>
            /// Return the stored value of this characteristic.
            /// </summary>
            /// <param name="formatType">The format type used to interpret the characteristic value. (One of FORMAT_*FLOAT)</param>
            /// <param name="offset">Offset at which the float value can be found.</param>
            /// <returns>Cached value of the characteristic at a given offset or null if the requested offset exceeds the value size.</returns>
            /// <seealso href="https://developer.android.com/reference/android/bluetooth/BluetoothGattCharacteristic#getFloatValue(int,%20int)"/>
            public float ToFloat(int formatType = /* FORMAT_FLOAT */ 0x00000034, int offset = 0)
            {
                return m_characteristic.AndroidObject.Call</* Float */ AndroidJavaObject>("getFloatValue", formatType, offset)
                    .Call<float>("floatValue");
            }

            #endregion
        }

        private GattCharacteristicValue m_value = null;
        /// <summary>
        /// Get the stored value for this characteristic.
        ///
        /// This function returns the stored value for this characteristic as retrieved by calling
        /// BluetoothGatt.readCharacteristic(BluetoothGattCharacteristic).
        /// The cached value of the characteristic is updated as a result of a read characteristic operation or if a
        /// characteristic update notification has been received.
        /// </summary>
        /// <value>Cached value of the characteristic</value>
        /// <seealso href="https://developer.android.com/reference/android/bluetooth/BluetoothGattCharacteristic#getValue()"/>
        public GattCharacteristicValue Value {
            get {
                if(m_value != null) {
                    return m_value;
                }
                return m_value = new GattCharacteristicValue(this);
            }
        }

        #endregion

        #region Permissions

        private static int m_PERMISSION_READ = 0;
        /// <summary>
        /// Characteristic read permission
        /// </summary>
        /// <seealso href="https://developer.android.com/reference/android/bluetooth/BluetoothGattCharacteristic#PERMISSION_READ"/>
        public static int PERMISSION_READ {
            get {
                if(m_PERMISSION_READ != 0) {
                    return m_PERMISSION_READ;
                }
                return m_PERMISSION_READ = m_androidClass.GetStatic<int>("PERMISSION_READ");
            }
        }

        private static int m_PERMISSION_READ_ENCRYPTED = 0;
        /// <summary>
        /// Characteristic permission: Allow encrypted read operations
        /// </summary>
        /// <seealso href="https://developer.android.com/reference/android/bluetooth/BluetoothGattCharacteristic#PERMISSION_READ_ENCRYPTED"/>
        public static int PERMISSION_READ_ENCRYPTED {
            get {
                if(m_PERMISSION_READ_ENCRYPTED != 0) {
                    return m_PERMISSION_READ_ENCRYPTED;
                }
                return m_PERMISSION_READ_ENCRYPTED = m_androidClass.GetStatic<int>("PERMISSION_READ_ENCRYPTED");
            }
        }

        private static int m_PERMISSION_READ_ENCRYPTED_MITM = 0;
        /// <summary>
        /// Characteristic permission: Allow reading with man-in-the-middle protection
        /// </summary>
        /// <seealso href="https://developer.android.com/reference/android/bluetooth/BluetoothGattCharacteristic#PERMISSION_READ_ENCRYPTED_MITM"/>
        public static int PERMISSION_READ_ENCRYPTED_MITM {
            get {
                if(m_PERMISSION_READ_ENCRYPTED_MITM != 0) {
                    return m_PERMISSION_READ_ENCRYPTED_MITM;
                }
                return m_PERMISSION_READ_ENCRYPTED_MITM = m_androidClass.GetStatic<int>("PERMISSION_READ_ENCRYPTED_MITM");
            }
        }

        private static int m_PERMISSION_WRITE = 0;
        /// <summary>
        /// Characteristic write permission
        /// </summary>
        /// <seealso href="https://developer.android.com/reference/android/bluetooth/BluetoothGattCharacteristic#PERMISSION_WRITE"/>
        public static int PERMISSION_WRITE {
            get {
                if(m_PERMISSION_WRITE != 0) {
                    return m_PERMISSION_WRITE;
                }
                return m_PERMISSION_WRITE = m_androidClass.GetStatic<int>("PERMISSION_WRITE");
            }
        }

        private static int m_PERMISSION_WRITE_ENCRYPTED = 0;
        /// <summary>
        /// Characteristic permission: Allow encrypted writes
        /// </summary>
        /// <seealso href="https://developer.android.com/reference/android/bluetooth/BluetoothGattCharacteristic#PERMISSION_WRITE_ENCRYPTED"/>
        public static int PERMISSION_WRITE_ENCRYPTED {
            get {
                if(m_PERMISSION_WRITE_ENCRYPTED != 0) {
                    return m_PERMISSION_WRITE_ENCRYPTED;
                }
                return m_PERMISSION_WRITE_ENCRYPTED = m_androidClass.GetStatic<int>("PERMISSION_WRITE_ENCRYPTED");
            }
        }

        private static int m_PERMISSION_WRITE_ENCRYPTED_MITM = 0;
        /// <summary>
        /// Characteristic permission: Allow encrypted writes with man-in-the-middle protection
        /// </summary>
        /// <seealso href="https://developer.android.com/reference/android/bluetooth/BluetoothGattCharacteristic#PERMISSION_WRITE_ENCRYPTED_MITM"/>
        public static int PERMISSION_WRITE_ENCRYPTED_MITM {
            get {
                if(m_PERMISSION_WRITE_ENCRYPTED_MITM != 0) {
                    return m_PERMISSION_WRITE_ENCRYPTED_MITM;
                }
                return m_PERMISSION_WRITE_ENCRYPTED_MITM = m_androidClass.GetStatic<int>("PERMISSION_WRITE_ENCRYPTED_MITM");
            }
        }

        private static int m_PERMISSION_WRITE_SIGNED = 0;
        /// <summary>
        /// Characteristic permission: Allow signed write operations
        /// </summary>
        /// <seealso href="https://developer.android.com/reference/android/bluetooth/BluetoothGattCharacteristic#PERMISSION_WRITE_SIGNED"/>
        public static int PERMISSION_WRITE_SIGNED {
            get {
                if(m_PERMISSION_WRITE_SIGNED != 0) {
                    return m_PERMISSION_WRITE_SIGNED;
                }
                return m_PERMISSION_WRITE_SIGNED = m_androidClass.GetStatic<int>("PERMISSION_WRITE_SIGNED");
            }
        }

        private static int m_PERMISSION_WRITE_SIGNED_MITM = 0;
        /// <summary>
        /// Characteristic permission: Allow signed write operations with man-in-the-middle protection
        /// </summary>
        /// <seealso href="https://developer.android.com/reference/android/bluetooth/BluetoothGattCharacteristic#PERMISSION_WRITE_SIGNED_MITM"/>
        public static int PERMISSION_WRITE_SIGNED_MITM {
            get {
                if(m_PERMISSION_WRITE_SIGNED_MITM != 0) {
                    return m_PERMISSION_WRITE_SIGNED;
                }
                return m_PERMISSION_WRITE_SIGNED_MITM = m_androidClass.GetStatic<int>("PERMISSION_WRITE_SIGNED_MITM");
            }
        }

        /// <summary>
        /// Returns the permissions for this characteristic.
        /// </summary>
        /// <value>Permissions of this characteristic</value>
        /// <seealso href="https://developer.android.com/reference/android/bluetooth/BluetoothGattCharacteristic#getPermissions()">
        public int Permissions {
            get {
                return m_androidObject.Call<int>("getPermissions");
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Triggered as a result of a characteristic read operation or a remote characteristic notification.
        /// </summary>
        public event Action<GattCharacteristic> OnValueFetched;

        /// <summary>
        /// Triggered as a result the result of a characteristic read operation with
        /// <see cref="Gatt.ReadCharacteristic">Gatt.ReadCharacteristic</see>.
        /// </summary>
        public event Action<GattCharacteristic> OnValueRead;

        /// <summary>
        /// Triggered as a result of the remote characteristic notification.
        /// </summary>
        public event Action<GattCharacteristic> OnValueChanged;

        #endregion

        internal GattService Service {
            get; private set;
        }

        internal GattCharacteristic(AndroidJavaObject androidObject, GattService service) : base (androidObject)
        {
            Service = service;
        }

        /// <summary>
        /// Enable notifications/indications for the characteristic.
        /// </summary>
        /// <returns>true, if the requested notification status was set successfully</returns>
        public bool EnableNotification()
        {
            return Service.Gatt.SetCharacteristicNotification(this, true);
        }

        /// <summary>
        /// Disable notifications/indications for the characteristic.
        /// </summary>
        /// <returns>true, if the requested notification status was set successfully</returns>
        public bool DisableNotification()
        {
            return Service.Gatt.SetCharacteristicNotification(this, false);
        }

        /// <summary>
        /// Writes a given characteristic and its values to the associated remote device.
        /// </summary>
        /// <seealso cref="Gatt.WriteCharacteristic">
        public void Write()
        {
            Service.Gatt.WriteCharacteristic(this);
        }

        /// <summary>
        /// Read a given characteristic and its values from the associated remote device.
        /// </summary>
        /// <seealso cref="Gatt.ReadCharacteristic">
        public void Read()
        {
            Service.Gatt.ReadCharacteristic(this);
        }

        internal void TriggerOnValueChanged()
        {
            if(OnValueRead != null) {
                OnValueChanged(this);
            }
            if(OnValueFetched != null) {
                OnValueFetched(this);
            }
        }

        internal void TriggerOnValueRead()
        {
            if(OnValueRead != null) {
                OnValueRead(this);
            }
            if(OnValueFetched != null) {
                OnValueFetched(this);
            }
        }
    }
}
