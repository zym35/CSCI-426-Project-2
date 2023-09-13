using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    
    public enum SoundEffect
    {
        Roof,
        Wall,
        Glass,
        GenerateBlock,
        BuildBlock,
        BuildBuilding,
        Fail,
        Coin,
        Upgrade
    }

    public enum Music
    {
        BGM
    }
    
    public List<AudioClip> soundEffectClips, musicClips;
    public AudioSource soundEffectSource, musicSource;
    public bool startPlayingMusicLoop = true;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (FindObjectsOfType<AudioManager>().Length > 1)
        {
            foreach (var obj in FindObjectsOfType<AudioManager>())
            {
                if (obj == this)
                    Destroy(gameObject);
            }
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        if (startPlayingMusicLoop)
            PlayMusic(0);
    }

    public void PlaySoundEffect(SoundEffect effect, float volume = 1)
    {
        var id = (int)effect;
        if (id >= soundEffectClips.Count)
        {
            Debug.LogWarning("AudioManager: no corresponding sound effect clip!");
            return;
        }
        soundEffectSource.PlayOneShot(soundEffectClips[id], volume);
    }
    
    public void PlayMusic(Music music, float volume = 1, bool loop = true)
    {
        var id = (int)music;
        if (id >= musicClips.Count)
        {
            Debug.LogWarning("AudioManager: no corresponding music clip!");
            return;
        }

        musicSource.clip = musicClips[id];
        musicSource.volume = volume;
        musicSource.loop = loop;
        musicSource.Play();
    }

    public void PauseMusic()
    {
        musicSource.Pause();
    }
    
    public void UnPauseMusic()
    {
        musicSource.UnPause();
    }
    
    public void StopMusic()
    {
        musicSource.Stop();
    }
}
