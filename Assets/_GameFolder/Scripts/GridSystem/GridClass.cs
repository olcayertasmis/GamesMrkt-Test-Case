using UnityEngine;

namespace _GameFolder.Scripts.GridSystem
{
    public class GridClass : MonoBehaviour
    {
        private Cell[,] _cells;
        private int _gridSizeX;
        private int _gridSizeY;

        public GridClass(int gridSizeX, int gridSizeY)
        {
            _gridSizeX = gridSizeX;
            _gridSizeY = gridSizeY;

            _cells = new Cell[gridSizeX, gridSizeY];

            for (int x = 0; x < gridSizeX; x++)
            {
                for (int y = 0; y < gridSizeY; y++)
                {
                    _cells[x, y] = gameObject.AddComponent<Cell>();
                }
            }
        }

        public void SetCellFruit(int x, int y, Fruit fruit)
        {
            _cells[x, y].SetFruits(fruit);
        }
    }
}