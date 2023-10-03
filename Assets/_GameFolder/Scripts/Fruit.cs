using _GameFolder.Scripts.Enums;
using UnityEngine;

namespace _GameFolder.Scripts
{
    [System.Serializable]
    public class Fruit : MonoBehaviour
    {
        public FruitColor FruitColor { get; private set; }
        public int ScoreValue { get; private set; }

        public Fruit(FruitColor fruitColor)
        {
            FruitColor = fruitColor;
        }
    }
}