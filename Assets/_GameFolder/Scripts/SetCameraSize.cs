using _GameFolder.Scripts.Data.LevelSystem;
using _GameFolder.Scripts.ManagerScripts;
using UnityEngine;

namespace _GameFolder.Scripts
{
    public class SetCameraSize : MonoBehaviour
    {
        private AllLevels _allLevels;

        private void Start()
        {
            _allLevels = Managers.Instance.DataManager.AllLevels;
            SetCameraSetting();
        }

        private void SetCameraSetting()
        {
            int halfGridSizeX = _allLevels.LevelList[_allLevels.activeLevel].RowCount / 2;
            int halfGridSizeY = _allLevels.LevelList[_allLevels.activeLevel].ColumnCount / 2;

            Vector3 centerPosition = new Vector3(halfGridSizeX, halfGridSizeY, -10);
            transform.position = centerPosition;
        }
    }
}