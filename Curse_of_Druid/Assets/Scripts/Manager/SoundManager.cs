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
    private float SFXVolume = 1.0f;

    public void PlayEffectSound(SOUND_NAME soundName, bool isLooping = false, float volume = 1f, float pitch = 1f)
    {
        /*
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
        audioSourceToUse.loop = isLooping;

        audioSourceToUse.Play();
        usingIndexs.Remove(emptyAudioIndex);
        */
    }

    public void StopEffectSound(SOUND_NAME soundName)
    {
        AudioClip clip = clipList[(int)soundName];
        for (int i = 0; i < audioSources.Count; i++)
        {
            if (audioSources[i].isPlaying && audioSources[i].clip == clip)
            {
                audioSources[i].Stop();
            }
        }
    }

    public void PlayBGM(SOUND_NAME soundName, float volume = 1f, float pitch = 1f)
    {
        bgmAudioSource.Play();
    }

    public void SetBGMVolume(float volume = 1f)
    {
        bgmAudioSource.volume = volume;
    }

    public void SetSFXVolume(float volume = 1f)
    {
        SFXVolume = volume;
    }

    public void PauseBGM()
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

    public void StopBGM()
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

    private void Start()
    {
        PlayBGM(SOUND_NAME.BGM);
    }
}

public enum SOUND_NAME
{
    Cut,
    GateChoir,
    GetItem,
    LeverSwitch,
    Pickaxe,
    PickaxeHitRock,
    PlayerJump,
    PlayerHurt,
    PlayerRun,
    PlayerRunGrass,
    Swing,
    BGM
};