using UnityEngine;

namespace EMDR.Saving
{
    public class PlayerPrefsController : MonoBehaviour
    {
        // Keys
        const string MASTER_VOLUME_KEY = "masterVolume";

        // Parameters
        const float MIN_VOLUME = 0f;
        const float MAX_VOLUME = 1f;

        public static void ClearPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
        }

        public static void SaveToDisk()
        {
            PlayerPrefs.Save();
        }

        public static void SetMasterVolume(float volume)
        {
            PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, Mathf.Clamp(volume, MIN_VOLUME, MAX_VOLUME));
        }

        public static float GetMasterVolume()
        {
            return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);
        }

        public static bool MasterVolumeKeyExists()
        {
            return PlayerPrefs.HasKey(MASTER_VOLUME_KEY);
        }
    }
}
