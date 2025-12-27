using UnityEngine;
using EMDR.Saving;

namespace EMDR.Sound
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundEffects : MonoBehaviour
    {
        // Tunables
        [SerializeField] AudioClip[] audioClips;

        // State
        float volume = 0.3f;
        private AudioSource audioSource;
        bool destroyAfterPlay;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            InitializeVolume();
        }

        private void Update()
        {
            if (destroyAfterPlay && !audioSource.isPlaying)
            {
                Destroy(gameObject);
            }
        }

        public void Setup(float defaultVolume, bool setDestroyAfterPlay)
        {
            volume = defaultVolume;
            InitializeVolume();
            destroyAfterPlay = setDestroyAfterPlay;
        }

        private void InitializeVolume()
        {
            if (PlayerPrefsController.MasterVolumeKeyExists())
            {
                volume = PlayerPrefsController.GetMasterVolume();
            }
            audioSource.volume = volume;
        }

        private void GeneratePersistentSoundEffect(AudioClip audioClip, float defaultVolume)
        {
            if (audioClip == null) { return; }
            SoundEffects newSoundEffects = Instantiate(this, null, true);
            newSoundEffects.Setup(defaultVolume, true);
            DontDestroyOnLoad(newSoundEffects);
            newSoundEffects.PlayClip(audioClip);
        }

        public void PlayClip(AudioClip audioClip)
        {
            if (audioClip == null) { return; }
            InitializeVolume();
            audioSource.clip = audioClip;
            if (!audioSource.isPlaying) { audioSource.Play(); }
        }

        public void PlayClip()
        {
            if (audioClips == null) { return; }
            AudioClip audioClip = audioClips[Random.Range(0, audioClips.Length - 1)];
            PlayClip(audioClip);
        }

        public void PlayClipAfterDestroy(AudioClip audioClip)
        {
            if (audioClip == null) { return; }
            GeneratePersistentSoundEffect(audioClip, audioSource.volume);
        }

        public void PlayClipAfterDestroy(int clipIndex)
        {
            if (audioClips == null) { return; }
            PlayClipAfterDestroy(audioClips[clipIndex]);
        }

        public void PlayClipAfterDestroy()
        {
            if (audioClips == null) { return; }
            AudioClip currentClip = audioClips[Random.Range(0, audioClips.Length - 1)];
            PlayClipAfterDestroy(currentClip);
        }
    }
}
