using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace akistd
{
    [Serializable]
    public class LevelData
    {

        public int sceneCode;

        public string timeComplete;
        public bool unlocked = false;

        public LevelData(LevelSO sceneList)
        {
            sceneCode = sceneList.sceneCode;
            timeComplete = sceneList.timeComplete;
            unlocked = sceneList.unlocked; 
        }
    }
}
