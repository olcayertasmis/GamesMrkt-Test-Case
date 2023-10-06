using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace _GameFolder.Scripts.GridSystem
{
    public class GridMovement : MonoBehaviour
    {
        [Header("Input Variable")]
        private Vector2 _firstTouchPos, _finalTouchPos;
        private bool _isTouch;
        private float _swipeAngle;
        private bool _canInteract = true;
        private bool _canInteract2;
        private RaycastHit2D _firstHitInformation;

        [Header("Components")]
        private Camera _cam;
        private GridSpawner _gridSpawner;

        private void Awake()
        {
            _cam = Camera.main;

            _gridSpawner = FindObjectOfType<GridSpawner>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && _canInteract) OnInteraction();
            if (Input.GetMouseButtonUp(0) && _canInteract2) NotInteraction();
        }

        private void OnInteraction()
        {
            _canInteract = false;
            _canInteract2 = true;

            _firstTouchPos = _cam.ScreenToWorldPoint(Input.mousePosition);
            _firstHitInformation = Physics2D.Raycast(_firstTouchPos, _cam.transform.forward);
            //Debug.Log("first " + _firstTouchPos);
        }

        private void NotInteraction()
        {
            _canInteract2 = false;
            _canInteract = true;

            if (_firstHitInformation.collider == null) return;

            _finalTouchPos = _cam.ScreenToWorldPoint(Input.mousePosition);
            //Debug.Log("final " + _finalTouchPos);

            CalculateAngle();
        }

        private void CalculateAngle()
        {
            Debug.Log("CalculateAngle");

            _swipeAngle = Mathf.Atan2(_finalTouchPos.y - _firstTouchPos.y, _finalTouchPos.x - _firstTouchPos.x);
            _swipeAngle = _swipeAngle * 180 / Mathf.PI;
            if (Vector3.Distance(_firstTouchPos, _finalTouchPos) > .5f)
            {
                MoveCells();
            }
        }

        private void MoveCells()
        {
            switch (_swipeAngle)
            {
                case < 45 and > -45: // sağa 
                    Debug.Log("SAĞ");
                    SlideRow(1);
                    break;
                case > 45 and <= 135: // yukarı
                    Debug.Log("YUKARI");
                    SlideColumn(1);
                    break;
                case < -45 and >= -135: // aşağı
                    Debug.Log("AŞAĞI");
                    SlideColumn(-1);
                    break;
                case > 135 or < -135: // sola
                    Debug.Log("SOLA");
                    SlideRow(-1);
                    break;
            }
        }

        #region Column Scrolling Operations

        private void SlideColumn(int addOrSubtract)
        {
            var fruitPos = _firstHitInformation.transform.position;
            var columnOfFruit = _gridSpawner.FruitColumns[(int)fruitPos.x];

            GameObject newFruit = null;


            switch (addOrSubtract)
            {
                case > 0: // Yukarı Kaydırma
                    foreach (var fruit in columnOfFruit)
                    {
                        var pos = fruit.transform.position;
                        pos.y += addOrSubtract;

                        fruit.transform.DOMoveY(pos.y, 0.5f).OnComplete(delegate
                        {
                            if (fruit == columnOfFruit[^1])
                            {
                                CloneOverflowFruitForColumn(columnOfFruit, columnOfFruit.Count - 1);

                                fruit.gameObject.SetActive(false);
                            }
                        });
                    }

                    break;
                case < 0: // Aşağı Kaydırma
                    for (int i = columnOfFruit.Count - 1; i >= 0; i--)
                    {
                        var pos = columnOfFruit[i].transform.position;
                        pos.y += addOrSubtract;

                        columnOfFruit[i].transform.DOMoveY(pos.y, 0.5f);

                        if (columnOfFruit[i] == columnOfFruit[0])
                        {
                            newFruit = CloneOverflowFruitForColumn(columnOfFruit, 0);

                            columnOfFruit[i].gameObject.SetActive(false);
                        }
                    }

                    break;
            }

            if (newFruit != null)
            {
                columnOfFruit.RemoveAt(0);
                columnOfFruit.Add(newFruit);
            }
        }

        private GameObject CloneOverflowFruitForColumn(List<GameObject> columnList, int index)
        {
            var columnInFruitCount = columnList.Count;
            var fruit = columnList[index];
            var pos = fruit.transform.position;

            switch (index)
            {
                case > 0:
                    columnList.RemoveAt(index);
                    pos.y = -1;
                    var spawnPos = new Vector3(pos.x, pos.y, pos.z);
                    var newFruit = Instantiate(fruit, spawnPos, Quaternion.identity);
                    newFruit.name = "Fruit : " + pos.x + ", " + pos.y;
                    columnList.Insert(0, newFruit);
                    newFruit.transform.DOMoveY(0, .1f);
                    break;
                case 0:
                    pos.y = columnInFruitCount;
                    var spawnPos2 = new Vector3(pos.x, pos.y, pos.z);
                    var newFruit2 = Instantiate(fruit, spawnPos2, Quaternion.identity);
                    newFruit2.name = "Fruit : " + pos.x + ", " + pos.y;

                    newFruit2.transform.DOMoveY(columnInFruitCount - 1, .1f);
                    return newFruit2;
            }

            return null;
        }

        #endregion

        #region Row Scrolling Operations

        private void SlideRow(int addOrSubtract)
        {
            var fruitPos = _firstHitInformation.transform.position;
            var rowOfFruit = _gridSpawner.FruitRows[(int)fruitPos.y];

            GameObject newFruit = null;


            switch (addOrSubtract)
            {
                case > 0: // Sağa Kaydırma
                    foreach (var fruit in rowOfFruit)
                    {
                        var pos = fruit.transform.position;
                        pos.x += addOrSubtract;

                        fruit.transform.DOMoveX(pos.x, 0.5f).OnComplete(delegate
                        {
                            if (fruit == rowOfFruit[^1])
                            {
                                CloneOverflowFruitForRow(rowOfFruit, rowOfFruit.Count - 1);

                                fruit.gameObject.SetActive(false);
                            }
                        });
                    }

                    break;
                case < 0: // Sola Kaydırma
                    for (int i = rowOfFruit.Count - 1; i >= 0; i--)
                    {
                        var pos = rowOfFruit[i].transform.position;
                        pos.x += addOrSubtract;

                        rowOfFruit[i].transform.DOMoveX(pos.x, 0.5f);

                        if (rowOfFruit[i] == rowOfFruit[0])
                        {
                            newFruit = CloneOverflowFruitForRow(rowOfFruit, 0);

                            rowOfFruit[i].gameObject.SetActive(false);
                        }
                    }

                    break;
            }

            if (newFruit != null)
            {
                rowOfFruit.RemoveAt(0);
                rowOfFruit.Add(newFruit);
            }

            _gridSpawner.UpdateRow(rowOfFruit);
        }

        private GameObject CloneOverflowFruitForRow(List<GameObject> rowList, int index)
        {
            var rowInFruitCount = rowList.Count;
            var fruit = rowList[index];
            var pos = fruit.transform.position;

            switch (index)
            {
                case > 0: // Sağa Kaydırma
                    rowList.RemoveAt(index);
                    pos.x = -1;
                    var spawnPos = new Vector3(pos.x, pos.y, pos.z);
                    var newFruit = Instantiate(fruit, spawnPos, Quaternion.identity);
                    newFruit.name = "Fruit : " + pos.x + ", " + pos.y;
                    rowList.Insert(0, newFruit);
                    newFruit.transform.DOMoveX(0, 0.1f);
                    break;
                case 0: // Sola Kaydırma
                    pos.x = rowInFruitCount;
                    var spawnPos2 = new Vector3(pos.x, pos.y, pos.z);
                    var newFruit2 = Instantiate(fruit, spawnPos2, Quaternion.identity);
                    newFruit2.name = "Fruit : " + pos.x + ", " + pos.y;

                    newFruit2.transform.DOMoveX(rowInFruitCount - 1, .25f);
                    return newFruit2;
            }

            return null;
        }

        #endregion
    }
}