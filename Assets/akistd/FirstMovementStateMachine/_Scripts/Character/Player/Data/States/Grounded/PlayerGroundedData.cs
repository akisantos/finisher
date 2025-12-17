using System;
using UnityEngine;

namespace akistd.FirstPerson
{
    [Serializable]
    public class PlayerGroundedData
    {

        [field: SerializeField] [field : Range(0f, 25f)]  public float BaseSpeed { get; set; } = 5f;
        [field: SerializeField] public AnimationCurve SlopeSpeedAngles { get; private set; }    
        [field: SerializeField] public PlayerRotationData BaseRotationData { get; private set; }
        [field: SerializeField] public PlayerWalkData WalkData { get; private set; }
        [field: SerializeField] public PlayerRunData RunData { get; private set; }
        [field: SerializeField] public PlayerSlidingData SlidingData { get; private set; }

        [field:SerializeField] public PlayerStopData StopData { get; private set; }

        [field: SerializeField] public PlayerDashingData DashData { get; private set; }
    }
}
