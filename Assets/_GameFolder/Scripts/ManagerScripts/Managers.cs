using System;
using _GameFolder.Scripts.Data.LevelSystem;
using _GameFolder.Scripts.SingletonSystem;
using UnityEngine;

namespace _GameFolder.Scripts.ManagerScripts
{
    public class Managers : Singleton<Managers>
    {
        [Header("Identifiers")]
        [SerializeField] private DataManager dataManager;
        [SerializeField] private GameManager gameManager;
        [SerializeField] private LevelManager levelManager;
        [SerializeField] private UIManager uiManager;


        #region Manager Getters

        public DataManager DataManager => dataManager;
        public GameManager GameManager => gameManager;
        public LevelManager LevelManager => levelManager;
        public UIManager UIManager => uiManager;

        #endregion
    } // END CLASS
}