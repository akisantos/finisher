using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace akistd.FirstPerson
{
    [CreateAssetMenu(fileName ="Player", menuName ="Custom/Character/PlayerFirstPerson")]
    public class PlayerSO : ScriptableObject
    {

        [field: SerializeField] public PlayerGroundedData GroundedData { get; private set; }
        [field: SerializeField] public PlayerAirboneData AirboneData { get; private set; }

        [field: SerializeField] public PlayerAudioData AudioData { get; private set; }
    }
}
