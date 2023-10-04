using _GameFolder.Scripts.Enums;
using UnityEngine;

namespace _GameFolder.Scripts
{
    [System.Serializable]
    public class Fruit : MonoBehaviour
    {
        public FruitColor FruitColor { get; private set; }

        public void FruitSpawn(Fruit prefab, Vector2 pos, Transform parent)
        {
            var fruit = Instantiate(prefab, pos, Quaternion.identity);
            fruit.transform.parent = parent;
            fruit.name = "Fruit - " + pos.x + ", " + pos.y;
        }
    }
}