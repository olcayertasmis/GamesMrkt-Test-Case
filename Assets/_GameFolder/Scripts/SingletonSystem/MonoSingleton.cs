using UnityEngine;

namespace _GameFolder.Scripts.SingletonSystem
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static volatile T _instance = null;

        public static T Instance => _instance;

        [SerializeField] private bool dontDestroyOnLoad = false;

        protected virtual void Awake()
        {
            MakeSingleton();
        }

        private void MakeSingleton()
        {
            if (dontDestroyOnLoad)
            {
                if (_instance == null)
                {
                    _instance = this as T;
                    DontDestroyOnLoad(gameObject);
                }
                else
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                if (_instance == null)
                {
                    _instance = this as T;
                }
            }
        }
    } // END CLASS
}