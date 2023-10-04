using UnityEngine;

namespace _GameFolder.Scripts.GridSystem
{
    public class Cell : MonoBehaviour
    {
        public bool IsEmpty { get; private set; }
        public Fruit FruitInCell { get; private set; }

        public void SetFruits(Fruit fruit)
        {
            FruitInCell = fruit;
            IsEmpty = false;
        }

        public void ClearCell()
        {
            FruitInCell = null;
            IsEmpty = true;
        }
    }
}