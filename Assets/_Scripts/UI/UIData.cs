using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace akistd
{
    [CreateAssetMenu(menuName ="UI/UIdata")]
    public class UIData : ScriptableObject
    {
        [Serializable]
        public struct UIInfo
        {
            public string name;
            public Canvas canvas;
            
        }

        public List<UIInfo> canvasesList;
    }
}
