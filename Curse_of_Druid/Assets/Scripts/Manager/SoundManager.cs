using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


////SoundManager.Instance.메소드 호출 ㅇㅋ? 하 씨발 힘들다
public class SoundManager : SingletonBehavior<SoundManager>
{
    public static SoundManager instance = null;
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


    public void PlayEffectSound(SOUND_NAME soundName)
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
        audioSourceToUse.volume = SFXVolume;
        audioSourceToUse.loop = false;

        audioSourceToUse.Play();
        usingIndexs.Remove(emptyAudioIndex);
    }

    public void PlayBGM()
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
        PlayBGM();
    }

        void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public static SoundManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
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
    Swing
};