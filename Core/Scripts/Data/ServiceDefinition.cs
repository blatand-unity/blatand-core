using Blatand.Utils;
using UnityEngine;

namespace Blatand.Core
{
    [CreateAssetMenu(menuName="Blatand/Bluetooth LE/Service Definition", fileName="New Service")]
    public class ServiceDefinition : ScriptableObject
    {
        public Uuid Id;
    }
}

