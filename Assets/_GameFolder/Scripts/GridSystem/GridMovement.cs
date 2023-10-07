using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace _GameFolder.Scripts.GridSystem
{
    public class GridMovement : MonoBehaviour
    {
        public enum MovementDirection
        {
            Vertical,
            Horizontal
        }

        private MovementDirection _movementDirection;

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

        private MatchFinder _matchFinder;

        private void Awake()
        {
            _cam = Camera.main;

            _gridSpawner = GetComponent<GridSpawner>();

            _matchFinder = FindObjectOfType<MatchFinder>();
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
        }

        private void NotInteraction()
        {
            _canInteract2 = false;
            _canInteract = true;

            if (_firstHitInformation.collider == null) return;

            _finalTouchPos = _cam.ScreenToWorldPoint(Input.mousePosition);

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

            _matchFinder.FindAllMatches();
        }

        #region Column Scrolling Operations

        private void SlideColumn(int addOrSubtract)
        {
            _movementDirection = MovementDirection.Vertical;

            var fruitPos = _firstHitInformation.transform.position;
            var columnOfFruits = _gridSpawner.FruitColumns[(int)fruitPos.x];

            GameObject cloneFruitOfTop = null;
            GameObject cloneFruitOfBot = null;

            switch (addOrSubtract)
            {
                case > 0: // Yukarı Kaydırma
                    foreach (var fruit in columnOfFruits)
                    {
                        var pos = fruit.transform.position;
                        pos.y += addOrSubtract;

                        fruit.transform.DOMoveY(pos.y, 0.5f);

                        if (fruit == columnOfFruits[^1])
                        {
                            cloneFruitOfBot = CloneOverflowFruitForColumn(columnOfFruits, columnOfFruits.Count - 1);

                            fruit.gameObject.SetActive(false);

                            _gridSpawner.Cells[(int)pos.x, 0].UpdateFruits(cloneFruitOfBot.GetComponent<Fruit>());
                        }
                        else
                        {
                            _gridSpawner.Cells[(int)pos.x, (int)pos.y].UpdateFruits(fruit.GetComponent<Fruit>());
                        }
                    }

                    break;
                case < 0: // Aşağı Kaydırma
                    for (int i = columnOfFruits.Count - 1; i >= 0; i--)
                    {
                        var pos = columnOfFruits[i].transform.position;
                        pos.y += addOrSubtract;

                        columnOfFruits[i].transform.DOMoveY(pos.y, 0.5f);

                        if (columnOfFruits[i] == columnOfFruits[0])
                        {
                            cloneFruitOfTop = CloneOverflowFruitForColumn(columnOfFruits, 0);

                            columnOfFruits[i].gameObject.SetActive(false);

                            _gridSpawner.Cells[(int)pos.x, columnOfFruits.Count - 1].UpdateFruits(cloneFruitOfTop.GetComponent<Fruit>());
                        }
                        else _gridSpawner.Cells[(int)pos.x, (int)pos.y].UpdateFruits(columnOfFruits[i].GetComponent<Fruit>());
                    }

                    break;
            }

            if (cloneFruitOfTop != null)
            {
                columnOfFruits.RemoveAt(0);
                columnOfFruits.Add(cloneFruitOfTop);
            }

            if (cloneFruitOfBot != null)
            {
                columnOfFruits.RemoveAt(columnOfFruits.Count - 1);
                columnOfFruits.Insert(0, cloneFruitOfBot);
            }
        }

        private static GameObject CloneOverflowFruitForColumn(List<GameObject> columnList, int index)
        {
            var columnInFruitCount = columnList.Count;
            var fruit = columnList[index];
            var pos = fruit.transform.position;

            switch (index)
            {
                case > 0:
                    pos.y = -1;
                    var spawnPos = new Vector3(pos.x, pos.y, pos.z);
                    var newFruit = Instantiate(fruit, spawnPos, Quaternion.identity);
                    newFruit.name = "Fruit : " + pos.x + ", " + pos.y + 1;
                    newFruit.transform.DOMoveY(0, .1f);
                    return newFruit;
                    break;
                case 0:
                    pos.y = columnInFruitCount;
                    var spawnPos2 = new Vector3(pos.x, pos.y, pos.z);
                    var newFruit2 = Instantiate(fruit, spawnPos2, Quaternion.identity);
                    newFruit2.name = "Fruit : " + pos.x + ", " + pos.y + -1;
                    newFruit2.transform.DOMoveY(columnInFruitCount - 1, .1f);
                    return newFruit2;
            }

            return null;
        }

        #endregion

        #region Row Scrolling Operations

        private void SlideRow(int addOrSubtract)
        {
            _movementDirection = MovementDirection.Horizontal;

            var fruitPos = _firstHitInformation.transform.position;
            var rowOfFruit = _gridSpawner.FruitRows[(int)fruitPos.y];

            GameObject cloneFruitOfLeft = null;
            GameObject cloneFruitOfRight = null;

            switch (addOrSubtract)
            {
                case > 0: // Sağa Kaydırma
                    foreach (var fruit in rowOfFruit)
                    {
                        var pos = fruit.transform.position;
                        pos.x += addOrSubtract;

                        fruit.transform.DOMoveX(pos.x, 0.5f);


                        if (fruit == rowOfFruit[^1])
                        {
                            cloneFruitOfLeft = CloneOverflowFruitForRow(rowOfFruit, rowOfFruit.Count - 1);

                            fruit.gameObject.SetActive(false);

                            _gridSpawner.Cells[0, (int)pos.y].UpdateFruits(cloneFruitOfLeft.GetComponent<Fruit>());
                        }
                        else _gridSpawner.Cells[(int)pos.x, (int)pos.y].UpdateFruits(fruit.GetComponent<Fruit>());
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
                            cloneFruitOfRight = CloneOverflowFruitForRow(rowOfFruit, 0);

                            rowOfFruit[i].gameObject.SetActive(false);

                            _gridSpawner.Cells[rowOfFruit.Count - 1, (int)pos.y].UpdateFruits(cloneFruitOfRight.GetComponent<Fruit>());
                        }
                        else _gridSpawner.Cells[(int)pos.x, (int)pos.y].UpdateFruits(rowOfFruit[i].GetComponent<Fruit>());
                    }

                    break;
            }


            if (cloneFruitOfRight != null)
            {
                rowOfFruit.RemoveAt(0);
                rowOfFruit.Add(cloneFruitOfRight);
            }

            if (cloneFruitOfLeft != null)
            {
                rowOfFruit.RemoveAt(rowOfFruit.Count - 1);
                rowOfFruit.Insert(0, cloneFruitOfLeft);
            }
        }

        private GameObject CloneOverflowFruitForRow(List<GameObject> rowList, int index)
        {
            var rowInFruitCount = rowList.Count;
            var fruit = rowList[index];
            var pos = fruit.transform.position;

            switch (index)
            {
                case > 0: // Sağa Kaydırma
                    pos.x = -1;
                    var spawnPos = new Vector3(pos.x, pos.y, pos.z);
                    var newFruit = Instantiate(fruit, spawnPos, Quaternion.identity);
                    newFruit.name = "Fruit : " + pos.x + 1 + ", " + pos.y;
                    newFruit.transform.DOMoveX(0, 0.1f);
                    break;
                case 0: // Sola Kaydırma
                    pos.x = rowInFruitCount;
                    var spawnPos2 = new Vector3(pos.x, pos.y, pos.z);
                    var newFruit2 = Instantiate(fruit, spawnPos2, Quaternion.identity);
                    newFruit2.name = "Fruit : " + pos.x + -1 + ", " + pos.y;
                    newFruit2.transform.DOMoveX(rowInFruitCount - 1, .25f);
                    return newFruit2;
            }

            return null;
        }

        #endregion

        #region Helpers

        public MovementDirection GetCurrentMovementDirection() => _movementDirection;

        #endregion
    }
}