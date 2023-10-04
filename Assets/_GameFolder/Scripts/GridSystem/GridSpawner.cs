using _GameFolder.Scripts.Data;
using _GameFolder.Scripts.ManagerScripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _GameFolder.Scripts.GridSystem
{
    public class GridSpawner : MonoBehaviour
    {
        private Cell[,] _cells;

        private int _rowCount;
        private int _columnCount;

        private GameObject _cellPrefab;

        private AllFruits _allFruits;

        private void Awake()
        {
            var dataManager = Managers.Instance.DataManager;

            _allFruits = dataManager.AllFruits;

            var activeLevelIndex = Managers.Instance.LevelManager.GetActiveLevel();
            var activeLevel = dataManager.AllLevels.LevelList[activeLevelIndex];
            _rowCount = activeLevel.RowCount;
            _columnCount = activeLevel.ColumnCount;

            _cellPrefab = activeLevel.CellPrefab;
        }

        private void Start()
        {
            InitializeGrid();
        }

        private void InitializeGrid()
        {
            _cells = new Cell[_rowCount, _columnCount];

            for (int x = 0; x < _rowCount; x++)
            {
                for (int y = 0; y < _columnCount; y++)
                {
                    Vector2 pos = new Vector2(x, y);
                    var cell = SpawnCell(pos);

                    int fruitToUse = Random.Range(0, _allFruits.FruitList.Length);

                    var fruit = new Fruit();
                    fruit.FruitSpawn(_allFruits.FruitList[fruitToUse], pos, cell.transform);
                }
            }
        }

        private GameObject SpawnCell(Vector2 pos)
        {
            GameObject cell = Instantiate(_cellPrefab, pos, Quaternion.identity);
            cell.transform.parent = transform;
            cell.name = "Cell - " + pos.x + "," + pos.y;

            return cell;
        }
    }
}