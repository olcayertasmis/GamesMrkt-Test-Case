using _GameFolder.Scripts.Data.LevelSystem;
using _GameFolder.Scripts.ManagerScripts;
using UnityEngine;

namespace _GameFolder.Scripts.Helpers
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
            float halfGridSizeX = (float)(_allLevels.LevelList[_allLevels.activeLevel].RowCount - 1) / 2;
            float halfGridSizeY = (float)(_allLevels.LevelList[_allLevels.activeLevel].ColumnCount - 1) / 2;

            Vector3 centerPosition = new Vector3(halfGridSizeX, halfGridSizeY, -10);
            transform.position = centerPosition;
        }
    } // END CLASS
}