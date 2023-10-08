using System.Collections.Generic;
using System.Linq;
using _GameFolder.Scripts.Enums;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _GameFolder.Scripts.ManagerScripts
{
    public class GameManager : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private GameObject nextLevelPanel;
        [SerializeField] private GameObject fruitImages;
        [SerializeField] private GameObject inGamePanel;

        public List<FruitColor> requiredColor;
        public List<int> requiredValue;

        public List<int> activeTrueMatchCount = new List<int>();
        private readonly List<bool> _trueMatchControl = new List<bool>();

        private readonly string[] _imagePaths = { "blue", "red", "green", "yellow" };

        private void Start()
        {
            nextLevelPanel.SetActive(false);

            var level = Managers.Instance.DataManager.AllLevels.LevelList[Managers.Instance.DataManager.AllLevels.activeLevel];
            var required = level.RequiredValuesList;
            foreach (var req in required)
            {
                requiredColor.Add(req.requiredColor[0]);
                requiredValue.Add(req.requiredColorNumber[0]);
                activeTrueMatchCount.Add(0);
                _trueMatchControl.Add(false);
            }

            UpdateUI();
        }

        private void UpdateUI()
        {
            for (int i = 0; i < requiredColor.Count; i++)
            {
                var image = fruitImages.transform.GetChild(i);
                image.gameObject.SetActive(true);
                Image imageComponent = image.GetComponent<Image>();
                TextMeshProUGUI tmpComponent = image.GetComponentInChildren<TextMeshProUGUI>();
                switch (requiredColor[i])
                {
                    case FruitColor.Blue:
                        imageComponent.sprite = Resources.Load<Sprite>(_imagePaths[0]); // Blue resmi yükle
                        tmpComponent.text = activeTrueMatchCount[i].ToString();
                        break;
                    case FruitColor.Red:
                        imageComponent.sprite = Resources.Load<Sprite>(_imagePaths[1]); // Red resmi yükle
                        tmpComponent.text = activeTrueMatchCount[i].ToString();
                        break;
                    case FruitColor.Green:
                        imageComponent.sprite = Resources.Load<Sprite>(_imagePaths[2]); // Green resmi yükle
                        tmpComponent.text = activeTrueMatchCount[i].ToString();
                        break;
                    case FruitColor.Yellow:
                        imageComponent.sprite = Resources.Load<Sprite>(_imagePaths[3]); // Yellow resmi yükle
                        tmpComponent.text = activeTrueMatchCount[i].ToString();
                        break;
                }
            }
        }

        private void Update()
        {
            Control();
            OpenNextLevelPanel();
        }

        private void OpenNextLevelPanel()
        {
            bool allTrue = AreAllTrue(_trueMatchControl);

            if (allTrue)
            {
                nextLevelPanel.SetActive(true);
                inGamePanel.SetActive(false);
            }
        }

        private void Control()
        {
            for (int i = 0; i < requiredColor.Count; i++)
            {
                if (requiredValue[i] <= activeTrueMatchCount[i])
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
                    UpdateUI();
                }
            }
        }

        public void NextLevel()
        {
            Managers.Instance.DataManager.AllLevels.activeLevel++;

            var scene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(scene);
        }
    } // END CLASS
}