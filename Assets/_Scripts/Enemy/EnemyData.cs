using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace akistd
{
    [CreateAssetMenu(menuName = "Enemy/EnemyInfo", fileName ="enemyName")]
    public class EnemyData : ScriptableObject
    {
        public float enemyFullHealth = 100f;

        [field: Header("Audio")]
        public List<AudioClip> enemyDET;

    }
}
