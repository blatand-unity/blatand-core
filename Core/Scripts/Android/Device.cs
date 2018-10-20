using System;
using System.Collections.Generic;
using Blatand.Utils;
using UnityEngine;

namespace Blatand.Core.Android
{
    public class Device : AAndroidObjectWrapper
    {
        /// <summary>
        /// Get the friendly Bluetooth name of the remote device.
        /// </summary>
        /// <value>the Bluetooth name, or null if there was a problem.</value>
        public string Name {
            get {
                return m_androidObject.Call<string>("getName");
            }
        }

        /// <summary>
        /// Returns the hardware address of this Bluetooth device.
        /// </summary>
        /// <value>Bluetooth hardware address as string</value>
        public string Address {
            get {
                return m_androidObject.Call<string>("getAddress");
            }
        }

        private Gatt m_gatt;
        public Gatt Gatt {
            get { return m_gatt; }
        }

        #region Events

        public event Action<Device, Gatt> OnConnected;

        #endregion

        public Device(AndroidJavaObject androidObject) : base(androidObject)
        {
        }

        public Gatt Connect()
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			var activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

            m_gatt = new Gatt();
            m_gatt.OnConnected += TriggerOnConnected;
            var callbacks = new AndroidJavaObject("com.blatand.GattCallbackInterfacer", new Gatt.InternalGattCallback(m_gatt));
            m_gatt.m_callbacks = callbacks;
            if (GetSDKInt() >= 23)
                m_gatt.AndroidObject = m_androidObject.Call<AndroidJavaObject>("connectGatt", activity, false, callbacks, 2);
            else
                m_gatt.AndroidObject = m_androidObject.Call<AndroidJavaObject>("connectGatt", activity, false, callbacks);

            return m_gatt;
        }

        void TriggerOnConnected(Gatt gatt) {
            OnConnected(this, gatt);
        }

        static int GetSDKInt()
        {
            using (var version = new AndroidJavaClass("android.os.Build$VERSION"))
            {
                return version.GetStatic<int>("SDK_INT");
            }
        }
    }
}
