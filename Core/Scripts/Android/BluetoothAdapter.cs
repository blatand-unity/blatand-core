using System;
using System.Collections.Generic;
using Blatand.Utils;
using UnityEngine;

namespace Blatand.Core.Android
{
    public class BluetoothAdapter : AAndroidObjectWrapper
    {

        public static BluetoothAdapter GetSystemAdapter()
        {
            AndroidJavaObject adapter = BlatandPlugin.Instance.GetSystemAdapter();
            return new BluetoothAdapter(adapter);
        }

        private BluetoothAdapter(AndroidJavaObject androidObject) : base(androidObject)
        {
        }

        public bool IsEnabled()
        {
            return AndroidObject.Call<bool>("isEnabled");
        }

        public void Enable()
        {
            AndroidObject.Call<bool>("enable");
        }

        public void Disable()
        {
            AndroidObject.Call<bool>("disable");
        }
    }
}