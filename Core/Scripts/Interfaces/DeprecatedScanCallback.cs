using Blatand.Utils;
using UnityEngine;

namespace Blatand.Core
{
    /// <summary>
    /// This abstract class is used to implement BluetoothGatt callbacks.
    /// </summary>
    public class DeprecatedScanCallback : AndroidJavaProxy
    {

        public DeprecatedScanCallback() : base("com.blatand.DeprecatedScanCallback") { }

        public virtual void OnLeScan(AndroidJavaObject androidDevice, int rssi)
        {

        }
    }
}

