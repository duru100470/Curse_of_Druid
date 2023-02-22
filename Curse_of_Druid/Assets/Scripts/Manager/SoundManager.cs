using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SoundManager : SingletonBehavior<SoundManager>
{
    /// <summary>
    /// fixme
    /// </summary>
    private readonly List<AudioSource> audioSources = new List<AudioSource>();
    [SerializeField]
    private AudioSource bgmAudioSource;
    private readonly HashSet<int> usingIndexs = new HashSet<int>();
    [SerializeField]
    private List<AudioClip> clipList;

    public void PlayEffectSound(SOUND_NAME soundName, float volume = 1f, float pitch = 1f)
    {
        int emptyAudioIndex = -1;
        for (int i = 0; i < audioSources.Count; ++i)
        {
            if (!usingIndexs.Contains(i) && !audioSources[i].isPlaying)
            {
                emptyAudioIndex = i;
                usingIndexs.Add(emptyAudioIndex);
                break;
            }
        }
        // 만일 모든 AudioSource가 사용중일때
        if (emptyAudioIndex < 0)
        {
            audioSources.Add(gameObject.AddComponent<AudioSource>());
            emptyAudioIndex = audioSources.Count - 1;
        }

        var audioSourceToUse = audioSources[emptyAudioIndex];

        audioSourceToUse.clip = clipList[(int)soundName];
        audioSourceToUse.volume = volume;
        audioSourceToUse.pitch = pitch;
        audioSourceToUse.loop = false;

        audioSourceToUse.Play();
        usingIndexs.Remove(emptyAudioIndex);
    }

    public void PlayBGM(SOUND_NAME soundName, float volume = 1f, float pitch = 1f)
    {
        bgmAudioSource.clip = clipList[(int)soundName];
        bgmAudioSource.volume = volume;
        bgmAudioSource.pitch = pitch;
        bgmAudioSource.loop = false;

        bgmAudioSource.Play();
    }

    public void PauseBGM(SOUND_NAME soundName)
    {
        bgmAudioSource.Pause();
    }

    public void PauseAll()
    {
        for (int i = 0; i < audioSources.Count; i++)
        {
            if (audioSources[i].isPlaying)
            {
                audioSources[i].Pause();
            }
        }
    }

    public void StopBGM(SOUND_NAME soundName)
    {
        bgmAudioSource.Stop();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        int Scenenumber = SceneManager.GetActiveScene().buildIndex;
        switch (Scenenumber) {
            default:
                PauseAll();
                break;
        }
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}

public enum SOUND_NAME
{
    BUTTON_CLICK_SOUND,

    MAIN_BGM,
    LEVEL_BGM
}