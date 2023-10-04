using UnityEngine;

namespace _GameFolder.Scripts.ManagerScripts
{
    public class LevelManager : MonoBehaviour
    {
        private int _activeLevel = 0;

        private void Start()
        {
            InitializeLevel();
        }

        public int GetActiveLevel()
        {
            return _activeLevel;
        }

        public void SetActiveLevel(int level)
        {
            _activeLevel = level;
        }


        private void InitializeLevel()
        {
        }
    }
}