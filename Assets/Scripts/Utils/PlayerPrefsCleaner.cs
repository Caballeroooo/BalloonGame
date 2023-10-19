#if UNITY_EDITOR
    using UnityEditor;
    using UnityEngine;

    public class PlayerPrefsCleaner : MonoBehaviour
    {
        [MenuItem("Caballeroooo/Clear PlayerPrefs")]
        private static void ClearPlayerPrefs()
        {
            var leaderBoardSettings = SettingsProvider.Get<LeaderBoardSettings>();
            leaderBoardSettings.Remove(PlayerPrefsProvider.GetPlayerGuid());
            PlayerPrefs.DeleteAll();
            Debug.Log("Player Prefs Cleared");
        }
    }
#endif