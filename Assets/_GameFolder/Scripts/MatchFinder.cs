using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _GameFolder.Scripts.GridSystem;
using _GameFolder.Scripts.ManagerScripts;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _GameFolder.Scripts
{
    public class MatchFinder : MonoBehaviour
    {
        [Header("Other Scripts")]
        private GridSpawner _gridSpawner;
        private DataManager _dataManager;

        [Header("List")]
        [SerializeField] private List<Fruit> currentMatches;

        private Transform _fruitSpawnTransform;

        private void Awake()
        {
            _gridSpawner = FindObjectOfType<GridSpawner>();

            _dataManager = Managers.Instance.DataManager;
        }

        private void Start()
        {
            Invoke(nameof(FindAllMatches), .5f);

            _fruitSpawnTransform = GameObject.Find("Fruits").transform;
        }

        public void FindAllMatches()
        {
            for (int x = 0; x < _gridSpawner.FruitRows.Count; x++)
            {
                for (int y = 0; y < _gridSpawner.FruitColumns.Count; y++)
                {
                    Fruit currentFruit = _gridSpawner.Cells[x, y].fruitInCell;

                    if (currentFruit != null)
                    {
                        if (x > 0 && x < _gridSpawner.FruitRows.Count - 1) // Yatay eşleşme var mı diye kontrol
                        {
                            Fruit fruitTheLeft = _gridSpawner.Cells[x - 1, y].fruitInCell;
                            Fruit fruitTheRight = _gridSpawner.Cells[x + 1, y].fruitInCell;

                            if (fruitTheLeft != null && fruitTheRight != null)
                            {
                                if (fruitTheLeft.FruitColorType == currentFruit.FruitColorType && fruitTheRight.FruitColorType == currentFruit.FruitColorType)
                                {
                                    currentMatches.Add(fruitTheLeft);
                                    currentMatches.Add(currentFruit);
                                    currentMatches.Add(fruitTheRight);
                                }
                            }
                        }

                        if (y > 0 && y < _gridSpawner.FruitColumns.Count - 1) // Dikey eşleşme var mı diye kontrol
                        {
                            Fruit fruitTheUnder = _gridSpawner.Cells[x, y - 1].fruitInCell;
                            Fruit fruitTheAbove = _gridSpawner.Cells[x, y + 1].fruitInCell;

                            if (fruitTheUnder != null && fruitTheAbove != null)
                            {
                                if (fruitTheUnder.FruitColorType == currentFruit.FruitColorType && fruitTheAbove.FruitColorType == currentFruit.FruitColorType)
                                {
                                    currentMatches.Add(fruitTheUnder);
                                    currentMatches.Add(currentFruit);
                                    currentMatches.Add(fruitTheAbove);
                                }
                            }
                        }
                    }
                }
            }

            if (currentMatches.Count > 0) currentMatches = currentMatches.Distinct().ToList();
            DestroyMatchedFruits();
        }

        private void DestroyMatchedFruits()
        {
            foreach (var fruit in currentMatches)
            {
                Vector2 pos = fruit.transform.position;
                _gridSpawner.Cells[(int)pos.x, (int)pos.y].ClearCell();
                fruit.gameObject.SetActive(false);
                StartCoroutine(SlideFruitsDown());
            }

            currentMatches.Clear();
        }

        private IEnumerator SlideFruitsDown()
        {
            yield return new WaitForSeconds(.3f);

            int nullCounter = 0;

            for (int x = 0; x < _gridSpawner.FruitRows.Count; x++)
            {
                for (int y = 0; y < _gridSpawner.FruitColumns.Count; y++)
                {
                    if (_gridSpawner.Cells[x, y].IsEmpty) nullCounter++;
                    else if (nullCounter > 0)
                    {
                        var fruit = _gridSpawner.Cells[x, y].fruitInCell;
                        var fruitPos = fruit.transform.position;

                        var newFruitPosY = fruitPos.y - nullCounter;

                        fruit.transform.DOMoveY(newFruitPosY, .1f);

                        _gridSpawner.Cells[x, y - nullCounter].UpdateFruits(_gridSpawner.Cells[x, y].fruitInCell);
                        _gridSpawner.Cells[x, y].ClearCell();
                    }
                }

                nullCounter = 0;
            }

            StartCoroutine(RefillCell());
        }

        private IEnumerator RefillCell()
        {
            yield return new WaitForSeconds(.5f);

            for (int x = 0; x < _gridSpawner.FruitColumns.Count; x++)
            {
                for (int y = 0; y < _gridSpawner.FruitRows.Count; y++)
                {
                    int fruitToUse = Random.Range(0, _dataManager.AllFruits.FruitList.Length);

                    Fruit.FruitSpawn(_dataManager.AllFruits.FruitList[fruitToUse], new Vector2(x, y), _fruitSpawnTransform);
                }
            }
        }
    }
}