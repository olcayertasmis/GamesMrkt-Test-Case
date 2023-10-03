using _GameFolder.Scripts.SingletonSystem;
using UnityEngine;

namespace _GameFolder.Scripts.ManagerScripts
{
    public class Managers : Singleton<Managers>
    {
        public DataManager DataManager { get; private set; }
        public GameManager GameManager { get; private set; }
        public UIManager UiManager { get; private set; }
        public LevelManager LevelManager { get; private set; }
    } // END CLASS
}