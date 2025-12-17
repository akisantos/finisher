using System;
using System.Collections.Generic;
using UnityEngine;

namespace akistd.FirstPerson
{
    [Serializable]
    public class PlayerJumpData
    {
        [field: SerializeField] public Vector3 RootForce { get;private set; }
        [field: SerializeField] public Vector3 WeakForce { get;private set; }
        [field: SerializeField] public Vector3 MediumForce { get; set; }
        [field: SerializeField] public Vector3 StrongForce { get;private set; }

        [field: SerializeField][field: Range(0f,5f)] public float JumpToGroundRayDistance { get; private set; } = 2f;
        [field: SerializeField] public AnimationCurve JumpForceUpdateUpwards { get; private set; }
        [field: SerializeField] public AnimationCurve JumpForceUpdateDownwards { get; private set; }
    }
}
