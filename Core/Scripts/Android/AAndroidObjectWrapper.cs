using System;
using System.Collections.Generic;
using Blatand.Utils;
using UnityEngine;

namespace Blatand.Core.Android
{
    public abstract class AAndroidObjectWrapper
    {
        protected AndroidJavaObject m_androidObject;
        public AndroidJavaObject AndroidObject {
            get { return m_androidObject; }
            internal set { m_androidObject = value; }
        }

        internal AAndroidObjectWrapper()
        {
            m_androidObject = null;
        }

        internal AAndroidObjectWrapper(AndroidJavaObject androidObject)
        {
            m_androidObject = androidObject;
        }
    }
}