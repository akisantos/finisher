using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace akistd.FirstPerson
{
    [Serializable]
    public class PlayerRunData
    {
        [field: SerializeField][field: Range(0f, 1f)] public float SpeedModifier { get; private set; } = 1f;
    }
}
