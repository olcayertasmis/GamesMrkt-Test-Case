using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _GameFolder.Scripts.Enums;
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
        private GameManager _gameManager;

        [Header("List")]
        private List<Fruit> _currentMatches = new List<Fruit>();

        private Transform _fruitSpawnTransform;

        private void Awake()
        {
            _gridSpawner = FindObjectOfType<GridSpawner>();

            _dataManager = Managers.Instance.DataManager;

            _gameManager = Managers.Instance.GameManager;
        }

        private void Start()
        {
            _fruitSpawnTransform = GameObject.Find("Fruits").transform;

            Invoke(nameof(FindAllMatches), .5f);
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
                                    _currentMatches.Add(fruitTheLeft);
                                    _currentMatches.Add(currentFruit);
                                    _currentMatches.Add(fruitTheRight);
                                    _gameManager.MatchValueControl(currentFruit.FruitColorType);
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
                                    _currentMatches.Add(fruitTheUnder);
                                    _currentMatches.Add(currentFruit);
                                    _currentMatches.Add(fruitTheAbove);
                                    _gameManager.MatchValueControl(currentFruit.FruitColorType);
                                }
                            }
                        }
                    }
                }
            }

            if (_currentMatches.Count > 0) _currentMatches = _currentMatches.Distinct().ToList();

            DestroyMatchedFruits();
        }

        private void DestroyMatchedFruits()
        {
            List<GameObject> fx = new List<GameObject>();
            foreach (var fruit in _currentMatches)
            {
                Vector2 pos = fruit.transform.position;
                _gridSpawner.Cells[(int)pos.x, (int)pos.y].ClearCell();

                fruit.gameObject.SetActive(false);
                fx.Add(GetFx(fruit));
            }

            _gridSpawner.StartCoroutine(SlideFruitsDown(fx));

            _currentMatches.Clear();
        }

        private GameObject GetFx(Fruit fruit)
        {
            switch (fruit.FruitColorType)
            {
                case FruitColor.Blue:
                    var fxBlue = Instantiate(_dataManager.MatchFinderData.BlueFx, fruit.transform.position, Quaternion.identity);
                    return fxBlue;
                case FruitColor.Red:
                    var fxRed = Instantiate(_dataManager.MatchFinderData.RedFx, fruit.transform.position, Quaternion.identity);
                    return fxRed;
                case FruitColor.Green:
                    var fxGreen = Instantiate(_dataManager.MatchFinderData.GreenFx, fruit.transform.position, Quaternion.identity);
                    return fxGreen;
                case FruitColor.Yellow:
                    var fxYellow = Instantiate(_dataManager.MatchFinderData.YellowFx, fruit.transform.position, Quaternion.identity);
                    return fxYellow;
            }

            return null;
        }

        private IEnumerator SlideFruitsDown(List<GameObject> fx)
        {
            yield return new WaitForSeconds(.2f);

            foreach (var x in fx)
            {
                x.gameObject.SetActive(false);
            }

            int nullCounter = 0;

            for (int x = 0; x < _gridSpawner.FruitRows.Count; x++)
            {
                for (int y = 0; y < _gridSpawner.FruitColumns.Count; y++)
                {
                    if (_gridSpawner.Cells[x, y].IsEmpty)
                    {
                        nullCounter++;
                    }
                    else if (nullCounter > 0)
                    {
                        var fruit = _gridSpawner.Cells[x, y].fruitInCell;
                        var fruitPos = fruit.transform.position;

                        var newFruitPosY = fruitPos.y - nullCounter;

                        fruit.transform.DOMoveY(newFruitPosY, .1f);

                        _gridSpawner.Cells[x, y - nullCounter].UpdateMatchFinder(_gridSpawner.Cells[x, y].fruitInCell);
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
                    if (_gridSpawner.Cells[x, y].IsEmpty)
                    {
                        int fruitToUse = Random.Range(0, _dataManager.AllFruits.FruitList.Length);

                        var spawnedFruit = Fruit.FruitSpawn(_dataManager.AllFruits.FruitList[fruitToUse], new Vector2(x, y), _fruitSpawnTransform);

                        _gridSpawner.Cells[x, y].UpdateMatchFinder(spawnedFruit);
                    }
                }
            }

            Invoke(nameof(FindAllMatches), .5f);
            
        }
    }
}