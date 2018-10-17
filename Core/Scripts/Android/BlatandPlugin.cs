using System;
using System.Collections.Generic;
using Blatand.Utils;
using UnityEngine;

namespace Blatand.Core.Android
{
    public class BlatandPlugin : AAndroidObjectWrapper
    {
        private static string s_className = "com.blatand.Plugin";

        private static AndroidJavaClass s_class;

        private static AndroidJavaClass InstanceClass {
            get {
                if (s_class == null) {
                    s_class = new AndroidJavaClass(s_className);
                }
                return s_class;
            }
        }

        private static AndroidJavaObject s_instance;

        public static BlatandPlugin Instance {
            get {
                if (s_instance == null) {
				    s_instance = InstanceClass.CallStatic<AndroidJavaObject>("getInstance");
			    }
                return new BlatandPlugin(s_instance);
            }
        }

        private BlatandPlugin(AndroidJavaObject androidObject) : base(androidObject)
        {
        }

        public AndroidJavaObject GetSystemAdapter()
        {
            return AndroidObject.Call<AndroidJavaObject>("getSystemAdapter");
        }

        public void AttemptEnablingBluetooth()
        {
            AndroidObject.Call("attemptEnablingBluetooth");
        }

        public void StartLeScan(AndroidJavaObject callback)
        {
            AndroidObject.Call("startLeScan", callback);
        }

        public void StopLeScan(AndroidJavaObject callback)
        {
            AndroidObject.Call("stopLeScan", callback);
        }
    }
}