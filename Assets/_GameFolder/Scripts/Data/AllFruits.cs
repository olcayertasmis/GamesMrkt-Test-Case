using _GameFolder.Scripts.GridSystem;
using UnityEngine;

namespace _GameFolder.Scripts.Data
{
    [CreateAssetMenu(fileName = "All Fruits", menuName = "New All Fruits")]
    public class AllFruits : ScriptableObject
    {
        [Header("Fruits List")]
        [SerializeField] private Fruit[] fruitList;

        #region Getters

        public Fruit[] FruitList => fruitList;

        #endregion
    } // END CLASS
}