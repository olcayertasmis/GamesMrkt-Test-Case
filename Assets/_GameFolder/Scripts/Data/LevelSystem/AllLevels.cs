using UnityEngine;
using UnityEngine.Serialization;

namespace _GameFolder.Scripts.Data.LevelSystem
{
    [CreateAssetMenu(fileName = "All Levels", menuName = "New All Levels")]
    public class AllLevels : ScriptableObject
    {
        [Header("Levels Array")]
        [SerializeField] private Level[] levelList;

        #region Getters

        public Level[] LevelList => levelList;

        #endregion
    }
}