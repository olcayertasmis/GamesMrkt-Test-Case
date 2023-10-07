using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace _GameFolder.Scripts.Data
{
    [CreateAssetMenu(fileName = "Grid Spawner Data", menuName = "New Grid Spawner Data")]
    public class GridSpawnerData : ScriptableObject
    {
        [SerializeField] private GameObject maskAreaPrefab;
        [SerializeField] private Fruit fruitScript;
        [SerializeField] private Transform emptyTransform;

        #region Getters

        public GameObject MaskAreaPrefab => maskAreaPrefab;
        public Fruit FruitScript => fruitScript;
        public Transform EmptyTransform => emptyTransform;
        //public Transform SpawnedFruitTransform => _spawnedFruitTransform;

        #endregion
    }
}