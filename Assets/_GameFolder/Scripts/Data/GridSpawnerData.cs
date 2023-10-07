using UnityEngine;

namespace _GameFolder.Scripts.Data
{
    [CreateAssetMenu(fileName = "Grid Spawner Data", menuName = "New Grid Spawner Data")]
    public class GridSpawnerData : ScriptableObject
    {
        [SerializeField] private GameObject maskAreaPrefab;
        [SerializeField] private Fruit fruitScript;
        [SerializeField] private Transform spawnedCellTransform;
        [SerializeField] private Transform spawnedFruitTransform;


        #region Getters

        public GameObject MaskAreaPrefab => maskAreaPrefab;
        public Fruit FruitScript => fruitScript;
        public Transform SpawnedCellTransform => spawnedCellTransform;
        public Transform SpawnedFruitTransform => spawnedFruitTransform;

        #endregion
    }
}