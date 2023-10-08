using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _GameFolder.Scripts.ManagerScripts
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject nextLevelPanel;

        private void Awake()
        {
            nextLevelPanel.SetActive(false);
        }

        public void NextLevel()
        {
            Managers.Instance.DataManager.AllLevels.activeLevel++;

            /*activeLevelIndex++;

            Managers.Instance.LevelManager.SetActiveLevel(activeLevelIndex);*/

            var scene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(scene);
        }
    }
}