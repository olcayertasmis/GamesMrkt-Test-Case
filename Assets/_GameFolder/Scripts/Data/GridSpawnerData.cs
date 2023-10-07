using UnityEngine;

namespace _GameFolder.Scripts.Data
{
    [CreateAssetMenu(fileName = "Grid Spawner Data", menuName = "New Grid Spawner Data")]
    public class GridSpawnerData : ScriptableObject
    {
        [SerializeField] private GameObject maskAreaPrefab;
        [SerializeField] private Fruit fruitScript;

        #region Getters

        public GameObject MaskAreaPrefab => maskAreaPrefab;
        public Fruit FruitScript => fruitScript;

        #endregion
    }
}