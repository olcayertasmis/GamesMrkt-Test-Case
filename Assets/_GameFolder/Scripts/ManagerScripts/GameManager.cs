using System;
using System.Collections.Generic;
using System.Linq;
using _GameFolder.Scripts.Enums;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _GameFolder.Scripts.ManagerScripts
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject nextLevelPanel;

        public List<FruitColor> requiredColor;
        public List<int> requiredValue;

        public List<int> activeTrueMatchCount = new List<int>();
        private List<bool> _trueMatchControl = new List<bool>();

        private void Start()
        {
            nextLevelPanel.SetActive(false);

            var level = Managers.Instance.DataManager.AllLevels.LevelList[Managers.Instance.DataManager.AllLevels.activeLevel];
            var req = level.requiredValues;
            foreach (var x in req)
            {
                requiredColor.Add(x.requiredColor[0]);
                requiredValue.Add(x.requiredColorNumber[0]);
                activeTrueMatchCount.Add(0);
                _trueMatchControl.Add(false);
            }
        }

        private void Update()
        {
            OpenNextLevelPanel();
        }

        private void OpenNextLevelPanel()
        {
            bool allTrue = AreAllTrue(_trueMatchControl);

            if (allTrue) nextLevelPanel.SetActive(true);
        }

        private void Control()
        {
            for (int i = 0; i < requiredColor.Count; i++)
            {
                if (requiredValue[i] == activeTrueMatchCount[i])
                {
                    _trueMatchControl[i] = true;
                }
            }
        }

        private bool AreAllTrue(List<bool> boolList)
        {
            return boolList.All(item => item); // Tüm değerler "true" ise true döner, aksi halde false döner
        }

        public void MatchValueControl(FruitColor fruitColor)
        {
            for (int i = 0; i < requiredColor.Count; i++)
            {
                if (fruitColor == requiredColor[i] && requiredValue[i] >= activeTrueMatchCount[i])
                {
                    activeTrueMatchCount[i]++;
                }
            }
        }

        public void NextLevel()
        {
            Managers.Instance.DataManager.AllLevels.activeLevel++;

            var scene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(scene);
        }
    }
}