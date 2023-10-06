using _GameFolder.Scripts.Data;
using _GameFolder.Scripts.Data.LevelSystem;
using UnityEngine;

namespace _GameFolder.Scripts.ManagerScripts
{
    [CreateAssetMenu(fileName = "Data Manager", menuName = "New Data Manager")]
    public class DataManager : ScriptableObject
    {
        [Header("Data")]
        [SerializeField] private AllLevels allLevels;
        [SerializeField] private AllFruits allFruits;
        [SerializeField] private GridSpawnerData gridSpawnerData;

        #region Data Getters

        public AllLevels AllLevels => allLevels;
        public AllFruits AllFruits => allFruits;
        public GridSpawnerData GridSpawnerData => gridSpawnerData;

        #endregion
    } // END CLASS
}