using Blatand.Utils;
using UnityEngine;

namespace Blatand.Core
{
    /// <summary>
    /// This abstract class is used to implement BluetoothGatt callbacks.
    /// </summary>
    public abstract class GattCallback : AndroidJavaProxy
    {
        public GattCallback() : base("com.blatand.GattCallback")
        {
        }

        /// <summary>
        /// Callback invoked when the list of remote services, characteristics and descriptors for the remote device
        /// have been updated, ie new services have been discovered.
        /// </summary>
        /// <param name="gatt">BluetoothGatt: GATT client invoked BluetoothGatt.discoverServices()</param>
        /// <param name="status">int: BluetoothGatt.GATT_SUCCESS if the remote device has been explored
        /// successfully.</param>
        public virtual void OnServicesDiscovered(/* BluetoothGatt */ AndroidJavaObject gatt, int status)
        {
        }

        /// <summary>
        /// Callback reporting the result of a characteristic read operation.
        /// </summary>
        /// <param name="gatt">BluetoothGatt: GATT client invoked</param>
        /// <param name="characteristic"> BluetoothGattCharacteristic: Characteristic that was read from the associated
        /// remote device.</param>
        /// <param name="status"> int: BluetoothGatt.GATT_SUCCESS if the read operation was completed
        /// successfully.</param>
        public virtual void OnCharacteristicRead(/* BluetoothGatt */ AndroidJavaObject gatt,
                                                 /* BluetoothGattCharacteristic */ AndroidJavaObject characteristic,
                                                 int status)
        {
        }

        /// <summary>
        /// Callback triggered as a result of a remote characteristic notification.
        /// </summary>
        /// <param name="gatt">BluetoothGatt: GATT client the characteristic is associated with</param>
        /// <param name="characteristic">BluetoothGattCharacteristic: Characteristic that has been updated as a result
        /// of a remote notification event. </param>
        public virtual void OnCharacteristicChanged(/* BluetoothGatt */ AndroidJavaObject gatt,
                                                    /* BluetoothGattCharacteristic */ AndroidJavaObject characteristic)
        {
        }

        /// <summary>
        /// Callback indicating the result of a characteristic write operation.
        ///
        /// If this callback is invoked while a reliable write transaction is in progress, the value of the
        /// characteristic represents the value reported by the remote device. An application should compare this value
        /// to the desired value to be written. If the values don't match, the application must abort the reliable
        /// write transaction.
        /// </summary>
        /// <param name="gatt">BluetoothGatt: GATT client invoked</param>
        /// <param name="androidCharacteristic">BluetoothGattCharacteristic: Characteristic that was written to the associated remote device.</param>
        /// <param name="status">int: The result of the write operation</param>
        public virtual void OnCharacteristicWrite(/* BluetoothGatt */ AndroidJavaObject gatt,
                                                  /* BluetoothGattCharacteristic */ AndroidJavaObject androidCharacteristic,
                                                  int status)
        {
        }

        /// <summary>
        /// Callback indicating the result of a descriptor write operation.
        /// </summary>
        /// <param name="gatt">BluetoothGatt: GATT client invoked</param>
        /// <param name="androidDescriptor">BluetoothGattDescriptor: Descriptor that was writte to the associated remote device.</param>
        /// <param name="status">int: The result of the write operation</param>
        public virtual void OnDescriptorWrite(/* BluetoothGatt */ AndroidJavaObject gatt,
                                              /* BluetoothGattDescriptor */ AndroidJavaObject androidDescriptor,
                                              int status)
        {
        }

        /// <summary>
        /// Callback indicating when GATT client has connected/disconnected to/from a remote GATT server.
        /// </summary>
        /// <param name="gatt">BluetoothGatt: GATT client</param>
        /// <param name="status">int: Status of the connect or disconnect operation. BluetoothGatt.GATT_SUCCESS if the operation succeeds.</param>
        /// <param name="newState">int: Returns the new connection state. Can be one of BluetoothProfile.STATE_DISCONNECTED or BluetoothProfile.STATE_CONNECTED</param>
        /// <seealso href="https://developer.android.com/reference/android/bluetooth/BluetoothGattCallback#onConnectionStateChange(android.bluetooth.BluetoothGatt,%20int,%20int)">
        public virtual void OnConnectionStateChange(/* BluetoothGatt */ AndroidJavaObject gatt,
                                                    int status,
                                                    int newState)
        {
        }
    }
}

