using System;
using System.Collections.Generic;
using Blatand.Utils;
using UnityEngine;
using UnityEngine.Assertions;

namespace Blatand.Core.Android
{
    public class Scanner : AAndroidObjectWrapper
    {
        private class InternalScanCallback : ScanCallback
        {
            /// <summary>
            /// Scanner linked with this callabacks
            /// </summary>
            Scanner m_source;

			public InternalScanCallback(Scanner source) : base() {
				m_source = source;
			}

            public override void OnScanResult(int callbackType, /* ScanResult */ AndroidJavaObject result)
            {
                Device device = new Device(result.Call<AndroidJavaObject>("getDevice"));
                UnityMainThreadDispatcher.Instance().Enqueue(() => {
                    m_source.OnDeviceDetected(device);
                });
            }
        }

        private class InternalDeprecatedScanCallback : DeprecatedScanCallback
        {
            /// <summary>
            /// Scanner linked with this callback
            /// </summary>
            Scanner m_source;

            public InternalDeprecatedScanCallback(Scanner source) : base()
            {
                m_source = source;
      
            }

            public override void OnLeScan(AndroidJavaObject androidDevice, int rssi)
            {
                Device device = new Device(androidDevice);
                UnityMainThreadDispatcher.Instance().Enqueue(() =>
                {
                    m_source.OnDeviceDetected(device);
                });

            }
        }
        private AndroidJavaObject m_callbacks;

        private bool m_isScanning = false;
        public bool IsScanning {
            get { return m_isScanning; }
        }

        #region Events

        /// <summary>
        /// Triggered when a BLE device is detected.
        /// </summary>
        public event Action<Device> OnDeviceDetected;

        #endregion

        public static Scanner NewScanner()
        {       
            if (Scanner.GetSDKInt() >= 21)
            {
                return new Scanner(BluetoothAdapter.GetSystemAdapter().AndroidObject.Call<AndroidJavaObject>("getBluetoothLeScanner"));
            }
            else
            {
                return new Scanner(BluetoothAdapter.GetSystemAdapter().AndroidObject);
            }
        }

        internal Scanner(AndroidJavaObject androidObject) : base (androidObject)
        {
            Assert.IsNotNull(androidObject);
        }


        static int GetSDKInt()
        {
            using (var version = new AndroidJavaClass("android.os.Build$VERSION"))
            {
                return version.GetStatic<int>("SDK_INT");
            }
        }

        /// <summary>
        /// Start Bluetooth LE scan.
        /// </summary>
        public void Start()
        {
            if(m_isScanning) {
                return;
            }
            if(m_callbacks == null) {
                if (GetSDKInt() >= 21)
                {
                    m_callbacks = new AndroidJavaObject("com.blatand.ScanCallbackInterfacer", new InternalScanCallback(this));
                    m_androidObject.Call("startScan", m_callbacks);
                }
                else
                {
                    m_callbacks = new AndroidJavaObject("com.blatand.DeprecatedScanCallbackInterfacer", new InternalDeprecatedScanCallback(this));
                    BlatandPlugin.Instance.StartLeScan(m_callbacks);
                }
            }
            m_isScanning = true;
        }

        /// <summary>
        /// Stops an ongoing Bluetooth LE scan.
        /// </summary>
        public void Stop()
        {
            if(!m_isScanning) {
                return;
            }

            if (GetSDKInt() >= 21)
            {
                m_androidObject.Call("stopScan", m_callbacks);
            }
            else
            {
                BlatandPlugin.Instance.StopLeScan(m_callbacks);
            }

            m_isScanning = false;
        }
    }
}

