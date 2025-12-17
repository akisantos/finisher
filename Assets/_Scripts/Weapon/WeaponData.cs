using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace akistd
{
    [CreateAssetMenu(menuName ="AudioData/SwordData")]
    public class WeaponData : ScriptableObject
    {

        [field: SerializeField] public FirstPerson.PlayerMovementAudioData.AudioList swordSFX;

    }
}
