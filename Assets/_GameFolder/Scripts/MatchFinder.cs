using System;
using System.Collections.Generic;
using System.Linq;
using _GameFolder.Scripts.GridSystem;
using UnityEngine;

namespace _GameFolder.Scripts
{
    public class MatchFinder : MonoBehaviour
    {
        [Header("Other Scripts")]
        private GridSpawner _gridSpawner;

        [Header("List")]
        [SerializeField] private List<Fruit> currentMatches;

        private void Awake()
        {
            _gridSpawner = FindObjectOfType<GridSpawner>();
        }

        private void Start()
        {
            Invoke(nameof(FindAllMatches), .5f);
        }

        public void FindAllMatches()
        {
            currentMatches.Clear();

            for (int x = 0; x < _gridSpawner.FruitRows.Count; x++)
            {
                for (int y = 0; y < _gridSpawner.FruitColumns.Count; y++)
                {
                    Fruit currentFruit = _gridSpawner.Cells[x, y].FruitInCell;

                    if (currentFruit != null)
                    {
                        if (x > 0 && x < _gridSpawner.FruitRows.Count - 1) // Yatay eşleşme var mı diye kontrol
                        {
                            Fruit fruitTheLeft = _gridSpawner.Cells[x - 1, y].FruitInCell;
                            Fruit fruitTheRight = _gridSpawner.Cells[x + 1, y].FruitInCell;

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
                            Fruit fruitTheUnder = _gridSpawner.Cells[x, y - 1].FruitInCell;
                            Fruit fruitTheAbove = _gridSpawner.Cells[x, y + 1].FruitInCell;

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
        }
    }
}