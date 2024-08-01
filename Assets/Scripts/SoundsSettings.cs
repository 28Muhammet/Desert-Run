using UnityEngine;

public class SoundsSettings : MonoBehaviour
{
    [Header("Sounds Settings")]
    public GameObject[] sounds;
    private AudioSource[] audioSources;

    private void Start()
    {
        audioSources = new AudioSource[sounds.Length];
        for (int i = 0; i < sounds.Length; i++)
        {
            audioSources[i] = sounds[i].GetComponent<AudioSource>();
        }
    }

    private void Update()
    {
        AdjustSoundsVolume();

        if (PlayerController.fallTrue)
        {
            StopAllSounds();
        }

        if (SettingsCoin.isCoin)
        {
            PlayCoinSound();
            SettingsCoin.isCoin = false;
        }

        if (SettingsMenu.isPauseMenu)
        {
            PauseAllSounds();
        }
        else
        {
            UnPauseAllSounds();
        }
    }

    private void StopAllSounds()
    {
        foreach (AudioSource audio in audioSources)
        {
            audio.Stop();
        }
    }

    private void PlayCoinSound()
    {
        if (!audioSources[0].isPlaying)
        {
            audioSources[0].Play();
        }
    }

    private void PauseAllSounds()
    {
        foreach (AudioSource audio in audioSources)
        {
            audio.Pause();
        }
    }

    private void UnPauseAllSounds()
    {
        foreach (AudioSource audio in audioSources)
        {
            audio.UnPause();
        }
    }

    private void AdjustSoundsVolume()
    {
        foreach (AudioSource audio in audioSources)
        {
            audio.volume = SettingsMenu.isSounds ? 1f : 0f;
        }
    }
}
