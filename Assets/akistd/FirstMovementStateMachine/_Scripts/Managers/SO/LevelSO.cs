using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace akistd
{
    [System.Serializable]
    [CreateAssetMenu(menuName = "Level/New levelSO", fileName = "level_int")]
    public class LevelSO : ScriptableObject
    {

        [Header("SceneMain config")]
        public int sceneCode;
        public enum SceneType
        {
            MainMenu,
            Level
        }
        public SceneType sceneType;

        public Sprite thumbnail;


        public AudioClip bgm;

        [Header("Level Achievement")]
        public string timeComplete;
        public bool unlocked=false;
    }
}
