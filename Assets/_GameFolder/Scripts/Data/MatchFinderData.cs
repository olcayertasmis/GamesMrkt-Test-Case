using UnityEngine;

namespace _GameFolder.Scripts.Data
{
    [CreateAssetMenu(fileName = "Match Finder Data", menuName = "New Match Finder Data")]
    public class MatchFinderData : ScriptableObject
    {
        [SerializeField] private GameObject blueFx, redFx, greenFx, yellowFx;

        #region Getters

        public GameObject BlueFx => blueFx;
        public GameObject RedFx => redFx;
        public GameObject GreenFx => greenFx;
        public GameObject YellowFx => yellowFx;

        #endregion
    } // END CLASS
}