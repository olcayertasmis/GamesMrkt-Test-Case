using _GameFolder.Scripts.Data.LevelSystem;
using UnityEngine;

namespace _GameFolder.Scripts.ManagerScripts
{
    [CreateAssetMenu(fileName = "Data Manager", menuName = "New Data Manager")]
    public class DataManager : ScriptableObject
    {
        [Header("Data")]
        [SerializeField] private AllLevels allLevels;


        #region Data Getters

        public AllLevels AllLevels => allLevels;

        #endregion
    } // END CLASS
}