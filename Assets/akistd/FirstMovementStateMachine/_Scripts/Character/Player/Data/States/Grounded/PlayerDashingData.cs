using System;
using System.Collections.Generic;
using UnityEngine;

namespace akistd.FirstPerson
{
    [Serializable]
    public class PlayerDashingData 
    {
        [field: SerializeField] [field:Range(1f,3f)] public float SpeedModifier { get;private set; }

    }
}
