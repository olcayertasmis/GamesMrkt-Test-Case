using System.Collections.Generic;
using UnityEngine;

namespace _GameFolder.Scripts.Data.LevelSystem
{
    [CreateAssetMenu(fileName = "NewAllLevels", menuName = "AllLevels")]
    public class AllLevels : ScriptableObject
    {
        public List<Level> levels;
    }
}