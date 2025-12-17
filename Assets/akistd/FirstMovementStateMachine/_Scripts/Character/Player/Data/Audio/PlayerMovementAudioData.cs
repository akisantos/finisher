using System;
using System.Collections.Generic;
using UnityEngine;

namespace akistd.FirstPerson
{
    [Serializable]
    public class PlayerMovementAudioData
    {
        public enum AudioCate
        {
            Footstep,
            SFX,
            BGM
        }

        [Serializable]
        public struct AudioList
        {
            public AudioCate Cate;
            public List<AudioClip> audioList;

        }

        /*[field: SerializeField] public List<AudioClip> Footstep { get; private set; }*/

        [field: SerializeField] public AudioList Footstep { get; private set; }
        [field: SerializeField] public AudioClip Jump { get; private set; }


        [field:Header("SwordSFX")]
        [field: SerializeField] public AudioList SwordAttackAir { get; private set; }
        [field: SerializeField] public AudioList SwordAttackHit { get; private set; }

        [field: Header("TakeDamage")]
        [field: SerializeField] public AudioList HurtAudioList { get; private set; }
    }
}
