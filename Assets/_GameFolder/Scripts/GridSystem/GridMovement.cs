using UnityEngine;

namespace _GameFolder.Scripts.GridSystem
{
    public enum Direction
    {
        None,
        Up,
        Down,
        Left,
        Right
    }


    public class GridMovement : MonoBehaviour
    {
        private Vector2 _firstTouchPos, _finalTouchPos;

        private bool _isTouch;
        private float _swipeAngle;

        private Camera _cam;

        private GridSpawner _gridSpawner;

        private Fruit _otherFruit;

        private void Awake()
        {
            _cam = Camera.main;

            _gridSpawner = FindObjectOfType<GridSpawner>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0)) OnInteraction();
            if (Input.GetMouseButtonUp(0)) NotInteraction();
        }

        private void OnInteraction()
        {
            _firstTouchPos = _cam.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log("first " + _firstTouchPos);
        }

        private void NotInteraction()
        {
            RaycastHit2D firstHitInformation = Physics2D.Raycast(_firstTouchPos, _cam.transform.forward);
            if (firstHitInformation.collider == null) return;

            _finalTouchPos = _cam.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log("final " + _finalTouchPos);

            /*RaycastHit2D finalHitInformation = Physics2D.Raycast(_finalTouchPos, _cam.transform.forward);
            if (finalHitInformation.collider == null) return;*/

            CalculateAngle();
        }

        private void CalculateAngle()
        {
            Debug.Log("test");

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
                    break;
                case > 45 and <= 135: // yukarı
                    Debug.Log("YUKARI");
                    break;
                case < -45 and >= -135: // aşağı
                    Debug.Log("AŞAĞI");
                    break;
                case > 135 or < -135: // sola
                    Debug.Log("SOLA");
                    break;
            }
        }
    }
}