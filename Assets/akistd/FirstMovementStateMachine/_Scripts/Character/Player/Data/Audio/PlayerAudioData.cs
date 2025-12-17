using System;
using System.Collections.Generic;
using UnityEngine;

namespace akistd.FirstPerson
{
    [Serializable]
    public class PlayerAudioData
    {
        [field: SerializeField] public PlayerMovementAudioData PlayerMovementAudioData { get; private set; }

    }
}
