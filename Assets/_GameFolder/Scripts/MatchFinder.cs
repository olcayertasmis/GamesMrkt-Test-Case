using System;
using _GameFolder.Scripts.GridSystem;
using UnityEngine;

namespace _GameFolder.Scripts
{
    public class MatchFinder : MonoBehaviour
    {
        private GridSpawner _gridSpawner;

        private void Awake()
        {
            _gridSpawner = FindObjectOfType<GridSpawner>();
        }

        private void Start()
        {
            Invoke(nameof(FindAllMatches), 1f);
        }

        public void FindAllMatches()
        {
            for (int x = 0; x < _gridSpawner.FruitRows.Count; x++)
            {
                for (int y = 0; y < _gridSpawner.FruitColumns.Count; y++)
                {
                    Fruit currentFruit = _gridSpawner.Cells[x, y].FruitInCell;

                    if (currentFruit != null)
                    {
                        Debug.Log("Fruit boş değil");

                        if (x > 0 && x < _gridSpawner.FruitRows.Count - 1) // Yatay eşleşme var mı diye kontrol
                        {
                            Fruit fruitTheLeft = _gridSpawner.Cells[x - 1, y].FruitInCell;
                            Fruit fruitTheRight = _gridSpawner.Cells[x + 1, y].FruitInCell;

                            if (fruitTheLeft != null && fruitTheRight != null)
                            {
                                Debug.Log("Sağ ve Sol fruit boş değil");
                                if (fruitTheLeft.FruitColorType == currentFruit.FruitColorType && fruitTheRight.FruitColorType == currentFruit.FruitColorType)
                                {
                                    Debug.Log("Sol ve Sağ fruit ile renklerim eşleşti");
                                }
                            }
                        }

                        if (y > 0 && y < _gridSpawner.FruitColumns.Count - 1)
                        {
                            Fruit fruitTheUnder = _gridSpawner.Cells[x, y - 1].FruitInCell;
                            Fruit fruitTheAbove = _gridSpawner.Cells[x, y + 1].FruitInCell;

                            if (fruitTheUnder != null && fruitTheAbove != null)
                            {
                                Debug.Log("Altındaki ve Üstündeki fruit boş değil");
                                if (fruitTheUnder.FruitColorType == currentFruit.FruitColorType && fruitTheAbove.FruitColorType == currentFruit.FruitColorType)
                                {
                                    Debug.Log("Altındaki ve Üstündeki fruit ile renklerim eşleşti");
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}