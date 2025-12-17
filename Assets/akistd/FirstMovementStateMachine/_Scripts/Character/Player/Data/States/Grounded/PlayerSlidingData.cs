using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace akistd.FirstPerson
{
    [Serializable]
    public class PlayerSlidingData
    {
        [field: SerializeField][field: Range(0f, 5f)] public float SpeedRunModifier { get; private set; } = 2f;
        [field: SerializeField][field: Range(0f, 5f)] public float SpeedWalkModifier { get; private set; } = 1.3f;
        [field: SerializeField][field: Range(0f, 4f)] public float TimeToBeConsecutive { get; private set; } = 1f;
        [field: SerializeField][field: Range(0f, 4f)] public float ConsecutiveSlidingAmount { get; private set; } = 1f;
        [field: SerializeField][field: Range(0f, 4f)] public float CoolDown { get; private set; } = 1.75f;

    }
}
