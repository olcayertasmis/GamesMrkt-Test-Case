using UnityEngine;

namespace _GameFolder.Scripts.Data.LevelSystem
{
    [CreateAssetMenu(fileName = "All Levels", menuName = "New All Levels")]
    public class AllLevels : ScriptableObject
    {
        [Header("Levels Array")]
        [SerializeField] private Level[] levelList;

        public int activeLevel;

        #region Getters

        public Level[] LevelList => levelList;

        #endregion
    } // END CLASS
}