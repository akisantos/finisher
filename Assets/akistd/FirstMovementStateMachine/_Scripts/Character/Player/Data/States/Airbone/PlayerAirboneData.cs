using System;
using System.Collections.Generic;
using UnityEngine;

namespace akistd.FirstPerson
{
    [Serializable]
    public class PlayerAirboneData
    {
        [field: SerializeField] public PlayerJumpData JumpData { get; private set; }
    }
}
