using System.Collections.Generic;
using _GameFolder.Scripts.Enums;
using UnityEngine;

namespace _GameFolder.Scripts.Data.LevelSystem
{
    [CreateAssetMenu(fileName = "NewLevel", menuName = "Level")]
    public class Level : ScriptableObject
    {
        public int gridSizeX, gridSizeY;
        public Dictionary<FruitColor, int> RequiredMatches;
    }
}