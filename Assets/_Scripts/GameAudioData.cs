using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace akistd
{
    [System.Serializable]
    [CreateAssetMenu(menuName ="AudioData/GameAudio")]
    public class GameAudioData : ScriptableObject
    {
        [System.Serializable]
        public struct AudioData
        {
            public string name;
            public AudioClip clip;
        }


        [SerializeField]
        public AudioData[] audioData;
    }
}
