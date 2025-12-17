using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace akistd
{
    [System.Serializable]
    public class LevelDataList
    {
        public List<LevelData> lvDataList;

        public LevelDataList(List<LevelData> lvDataList)
        {
            this.lvDataList = lvDataList;
        }
    }
}
