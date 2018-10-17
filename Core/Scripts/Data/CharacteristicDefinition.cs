using Blatand.Utils;
using UnityEngine;

namespace Blatand.Core
{
    [CreateAssetMenu(menuName="Blatand/Bluetooth LE/Characteristic Definition", fileName="New Characteristic")]
    public class CharacteristicDefinition : ScriptableObject
    {
        public Uuid Id;

        public ServiceDefinition ServiceDefinition;
    }
}

