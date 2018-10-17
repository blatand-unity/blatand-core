using Blatand.Utils;
using UnityEngine;

namespace Blatand.Core
{
    /// <summary>
    /// This abstract class is used to implement BluetoothGatt callbacks. 
    /// </summary>
    public abstract class ScanCallback : AndroidJavaProxy
    {
        private static AndroidJavaClass m_androidScanCallbackClass = new AndroidJavaClass("android.bluetooth.le.ScanCallback");

        private static int m_SCAN_FAILED_ALREADY_STARTED;
        public static int SCAN_FAILED_ALREADY_STARTED {
            get {
                if(m_SCAN_FAILED_ALREADY_STARTED != null) {
                    return m_SCAN_FAILED_ALREADY_STARTED;
                }
                return m_SCAN_FAILED_ALREADY_STARTED = m_androidScanCallbackClass.GetStatic<int>("SCAN_FAILED_ALREADY_STARTED");
            }
        }

        private static int m_SCAN_FAILED_APPLICATION_REGISTRATION_FAILED;
        public static int SCAN_FAILED_APPLICATION_REGISTRATION_FAILED {
            get {
                if(m_SCAN_FAILED_APPLICATION_REGISTRATION_FAILED != null) {
                    return m_SCAN_FAILED_APPLICATION_REGISTRATION_FAILED;
                }
                return m_SCAN_FAILED_APPLICATION_REGISTRATION_FAILED = m_androidScanCallbackClass.GetStatic<int>("SCAN_FAILED_APPLICATION_REGISTRATION_FAILED");
            }
        }

        private static int m_SCAN_FAILED_FEATURE_UNSUPPORTED;
        public static int SCAN_FAILED_FEATURE_UNSUPPORTED {
            get {
                if(m_SCAN_FAILED_ALREADY_STARTED != null) {
                    return m_SCAN_FAILED_FEATURE_UNSUPPORTED;
                }
                return m_SCAN_FAILED_FEATURE_UNSUPPORTED = m_androidScanCallbackClass.GetStatic<int>("SCAN_FAILED_FEATURE_UNSUPPORTED");
            }
        }

        private static int m_SCAN_FAILED_INTERNAL_ERROR;
        public static int SCAN_FAILED_INTERNAL_ERROR {
            get {
                if(m_SCAN_FAILED_INTERNAL_ERROR != null) {
                    return m_SCAN_FAILED_INTERNAL_ERROR;
                }
                return m_SCAN_FAILED_INTERNAL_ERROR = m_androidScanCallbackClass.GetStatic<int>("SCAN_FAILED_INTERNAL_ERROR");
            }
        }

        public ScanCallback() : base("com.blatand.ScanCallback") 
        {
        }

        /// <summary>
        /// Callback when batch results are delivered.
        /// </summary>
        /// <param name="results">List<ScanResult>: List of scan results that are previously scanned.</param>
        public virtual void OnBatchScanResults(/* List<ScanResult> */ AndroidJavaObject results) 
        {
		}

        /// <summary>
        /// Callback when scan could not be started.
        /// </summary>
        /// <param name="errorCode">int: Error code (one of SCAN_FAILED_*) for scan failure.</param>
        public virtual void OnScanFailed(int errorCode)
        {
        }

        /// <summary>
        /// Callback when a BLE advertisement has been found.
        /// </summary>
        /// <param name="callbackType"> int: Determines how this callback was triggered. Could be one of
        /// ScanSettings.CALLBACK_TYPE_ALL_MATCHES, ScanSettings.CALLBACK_TYPE_FIRST_MATCH or 
        /// ScanSettings.CALLBACK_TYPE_MATCH_LOST</param>
        /// <param name="result">ScanResult: A Bluetooth LE scan result. </param>
        public virtual void OnScanResult(int callbackType, /* ScanResult */ AndroidJavaObject result)
        {
        }
    }
}

