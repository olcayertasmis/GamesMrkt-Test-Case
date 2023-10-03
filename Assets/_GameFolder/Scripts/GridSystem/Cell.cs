using UnityEngine;

namespace _GameFolder.Scripts.GridSystem
{
    public class Cell : MonoBehaviour
    {
        public bool IsEmpty { get; private set; }
        public Fruit FruitInCell { get; private set; }

        public void SetFruits(Fruit fruit)
        {
            IsEmpty = false;
            FruitInCell = fruit;
        }

        public void ClearCell()
        {
            IsEmpty = true;
            FruitInCell = null;
        }
    }
}