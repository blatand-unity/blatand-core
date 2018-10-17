using System;
using UnityEngine;

namespace Blatand.Utils
{
    /// <summary>
    /// Represents a universally unique identifier (UUID).
    ///
    /// The term globally unique identifier (GUID) is used on Windows.
    ///
    /// You can convert <see cref="Uuid">Uuid</see> to <see cref="System.Guid">System.Guid</see> with method
    /// <see cref="Uuid.ToGuid">toGuid</see>
    /// </summary>
    [Serializable]
    public class Uuid : ISerializationCallbackReceiver
    {
        protected System.Guid m_guid;

        /// <summary>
        /// A string that contains a GUID
        /// </summary>
        [HideInInspector]
        [SerializeField]
        protected string m_uuid;

        public static Uuid FromJavaObject(AndroidJavaObject uuidObject) {
            return new Uuid(uuidObject.Call<string>("toString"));
        }
        public Uuid(string uuidStr) {
            m_guid = new System.Guid(uuidStr);
        }

        /// <inheritdoc />
        public void OnAfterDeserialize()
        {
            try {
                m_guid = new System.Guid(m_uuid);
            } catch (System.FormatException e) {
                Debug.LogError("Invalid format for uuid: " + m_uuid);
            }
        }

        /// <inheritdoc />
        public void OnBeforeSerialize()
        {
            m_uuid = this.ToString("D");
        }

        /// <summary>
        /// Converts Uuid to <see cref="string">string</see>
        /// </summary>
        /// <param name="format">A single format specifier that indicates how to format the value of this Uuid. Ccan be
        /// "N", "D", "B", "P", or "X". "D" is used by default. </param>
        /// <returns>The Uuid convert to string</returns>
        public string ToString(string format = "D")
        {
            return m_guid.ToString(format);
        }

        /// <summary>
        /// Converts Uuid to <see cref="System.Guid">System.Guid</see>
        /// </summary>
        /// <returns>The Guid</returns>
        public System.Guid ToGuid()
        {
            return m_guid;
        }

        /// <summary>
        /// Converts Uuid to <see cref="AndroidJavaObject">java.util.Uuid object</see>
        /// </summary>
        /// <returns>A Java object encapsulating java.util.Uuid</returns>
        public AndroidJavaObject ToJavaObject()
        {
            return new AndroidJavaClass("java.util.UUID").CallStatic</* UUID */ AndroidJavaObject>("fromString", ToString("D"));
        }
    }
}