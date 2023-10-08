using System;
using UnityEngine;

namespace _GameFolder.Scripts.ManagerScripts
{
    public class LevelManager : MonoBehaviour
    {
        /*public int GetActiveLevel()
        {
            return _activeLevel;
        }*/

        public void SetActiveLevel(int level)
        {
            Managers.Instance.DataManager.AllLevels.activeLevel = level;
        }
    }
}