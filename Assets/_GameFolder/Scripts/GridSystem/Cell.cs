using System;
using UnityEngine;

namespace _GameFolder.Scripts.GridSystem
{
    public class Cell : MonoBehaviour
    {
        private Vector2 _cellPos;

        public bool IsEmpty { get; private set; }
        public Fruit FruitInCell { get; private set; }

        public Action<Vector2, Fruit> OnChangedFruit;

        public void Initialize(Fruit fruit, Vector2 pos)
        {
            _cellPos = pos;

            FruitInCell = fruit;
            IsEmpty = false;
        }

        public void UpdateFruits(Fruit fruit)
        {
            FruitInCell = fruit;
            IsEmpty = false;

            OnChangedFruit?.Invoke(_cellPos, fruit);
        }

        public void ClearCell()
        {
            FruitInCell = null;
            IsEmpty = true;
        }
    }
}