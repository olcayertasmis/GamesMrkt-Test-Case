using System.Collections.Generic;
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

        public AllFruits allFruits;

        [SerializeField] private Fruit fruit;

        public List<List<GameObject>> FruitColumns;
        public List<List<GameObject>> FruitRows;


        private void Awake()
        {
            _cells = null;

            var dataManager = Managers.Instance.DataManager;

            allFruits = dataManager.AllFruits;

            var activeLevelIndex = Managers.Instance.LevelManager.GetActiveLevel();
            var activeLevel = dataManager.AllLevels.LevelList[activeLevelIndex];
            _rowCount = activeLevel.RowCount;
            _columnCount = activeLevel.ColumnCount;

            _cellPrefab = activeLevel.CellPrefab;
        }

        private void Start()
        {
            InitializeGrid();
            //ListControl();
        }

        private void Update()
        {
        }


        private void InitializeGrid()
        {
            _cells = new Cell[_rowCount, _columnCount];
            FruitColumns = new List<List<GameObject>>();
            FruitRows = new List<List<GameObject>>();

            for (int x = 0; x < _rowCount; x++)
            {
                var column = new List<GameObject>();

                for (int y = 0; y < _columnCount; y++)
                {
                    Vector2 pos = new Vector2(x, y);
                    _cells[x, y] = SpawnCell(new Vector2(x, y));

                    int fruitToUse = Random.Range(0, allFruits.FruitList.Length);

                    var spawnedFruit = fruit.FruitSpawn(allFruits.FruitList[fruitToUse], pos, _cells[x, y].transform);
                    _cells[x, y].SetFruits(spawnedFruit);

                    column.Add(spawnedFruit.gameObject);
                }

                FruitColumns.Add(column);
            }

            for (int y = 0; y < _columnCount; y++)
            {
                var row = new List<GameObject>();

                for (int x = 0; x < _rowCount; x++)
                {
                    row.Add(FruitColumns[x][y]);
                }

                FruitRows.Add(row);
            }
        }

        private void ListControl()
        {
            for (int i = 0; i < FruitColumns.Count; i++)
            {
                Debug.Log("Column " + i + " has " + FruitColumns[i].Count + " fruits.");
            }

            foreach (var column in FruitColumns)
            {
                Debug.Log(column);
                foreach (var fruit in column)
                {
                    Debug.Log(fruit.name);
                }
            }

            for (int i = 0; i < FruitRows.Count; i++)
            {
                Debug.Log("Row " + i + " has " + FruitRows[i].Count + " fruits.");
            }

            foreach (var row in FruitRows)
            {
                Debug.Log(row);
                foreach (var fruit in row)
                {
                    Debug.Log(fruit.name);
                }
            }
        }

        private Cell SpawnCell(Vector2 pos)
        {
            GameObject cell = Instantiate(_cellPrefab, pos, Quaternion.identity);
            cell.transform.parent = transform;
            cell.name = "Cell - " + pos.x + "," + pos.y;

            return cell.GetComponent<Cell>();
        }
    }
}