using System;
using UnityEngine;

namespace akistd.FirstPerson
{
    [Serializable]
    public class PlayerRotationData 
    {
        [field: SerializeField] public Vector3 TargetRotationReachTime { get; private set; }
    }
}
