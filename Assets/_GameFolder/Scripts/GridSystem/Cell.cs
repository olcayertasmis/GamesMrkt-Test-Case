using System;
using UnityEngine;

namespace _GameFolder.Scripts.GridSystem
{
    public class Cell : MonoBehaviour
    {
        private Vector2 _cellPos;

        public bool IsEmpty { get; private set; }
        public Fruit fruitInCell;

        public Action<Vector2, Fruit> OnChangedFruit;

        public Action<Vector2, Fruit> OnMatchedFruit;


        public static Cell SpawnCell(GameObject cellPrefab, Vector2 pos, Transform cellSpawnTransform)
        {
            GameObject cell = Instantiate(cellPrefab, pos, Quaternion.identity);
            cell.transform.parent = cellSpawnTransform;
            cell.name = "Cell - " + pos.x + "," + pos.y;

            return cell.GetComponent<Cell>();
        }

        public void Initialize(Fruit fruit, Vector2 pos)
        {
            _cellPos = pos;

            fruitInCell = fruit;
            IsEmpty = false;
        }

        public void UpdateFruits(Fruit fruit)
        {
            fruitInCell = fruit;
            IsEmpty = false;

            OnChangedFruit?.Invoke(_cellPos, fruit);
        }

        public void UpdateMatchFinder(Fruit fruit)
        {
            fruitInCell = fruit;
            IsEmpty = false;

            OnMatchedFruit?.Invoke(_cellPos, fruit);
        }

        public void ClearCell()
        {
            fruitInCell = null;
            IsEmpty = true;
        }
    }
}