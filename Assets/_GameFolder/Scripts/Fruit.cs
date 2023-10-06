using _GameFolder.Scripts.Enums;
using UnityEngine;

namespace _GameFolder.Scripts
{
    [System.Serializable]
    public class Fruit : MonoBehaviour
    {
        [SerializeField] private FruitColor fruitColor;

        public Fruit FruitSpawn(Fruit prefab, Vector2 pos, Transform parent)
        {
            var fruit = Instantiate(prefab, pos, Quaternion.identity);
            //fruit.transform.parent = parent;
            fruit.name = "Fruit : " + pos.x + ", " + pos.y;

            return fruit;
        }
    }
}