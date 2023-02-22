using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Pause : MonoBehaviour
{
    [SerializeField]
    private GameObject basePanel;
    [SerializeField]
    private GameObject pauseButton;
    [SerializeField]
    private GameObject keySettingPanel;
    [SerializeField]
    private GameObject inventory;
    [SerializeField]
    private GameObject life;
    [SerializeField]
    private TextMeshProUGUI lifeText;
    [SerializeField]
    private TextMeshProUGUI BGMVolume;
    [SerializeField]
    private TextMeshProUGUI SFXVolume;
    private Entity player;
    private Vector3 playerPos;
    private bool isGamePaused;
    private bool isKeySettingOpen;
    private Scene currentScene;
    private float maxBGMVolume;
    private float maxSFXVolume;
    private float curBGMVolume;
    private float curSFXVolume;

    void Awake()
    {
        basePanel.SetActive(false);
        keySettingPanel.SetActive(false);
        maxBGMVolume = 7f;
        maxSFXVolume = 7f;
    }

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        lifeText.text = player.Health.ToString();
        playerPos = player.transform.position;
        currentScene = SceneManager.GetActiveScene();
        BGMVolume.text = maxBGMVolume.ToString();
        SFXVolume.text = maxSFXVolume.ToString();
        curBGMVolume = maxBGMVolume;
        curSFXVolume = maxSFXVolume;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            //Debug.Log("pause : " + isGamePaused);
            //Debug.Log("key : " + isKeySettingOpen);
            if(isGamePaused && !isKeySettingOpen)
            {
                Resume();
            }
            else if(isGamePaused && isKeySettingOpen)
            {
                Menu();
            }
            else
            {
                Menu();
            }
        }
    }

    public void Resume()
    {
        basePanel.SetActive(false);
        pauseButton.SetActive(true);
        inventory.SetActive(true);
        life.SetActive(true);
        isGamePaused = false;
        isKeySettingOpen = false;
    }

    public void Menu()
    {
        basePanel.SetActive(true);
        pauseButton.SetActive(false);
        keySettingPanel.SetActive(false);
        inventory.SetActive(false);
        life.SetActive(false);
        isGamePaused = true;
        isKeySettingOpen = false;
    }

    public void Home()
    {
        basePanel.SetActive(false);
        pauseButton.SetActive(true);
        inventory.SetActive(false);
        life.SetActive(false);
        isGamePaused = false;
        isKeySettingOpen = false;
        SceneManager.LoadScene("TitleScene");
    }

    public void Restart()
    {
        basePanel.SetActive(false);
        pauseButton.SetActive(true);
        inventory.SetActive(true);
        life.SetActive(true);
        isGamePaused = false;
        isKeySettingOpen = false;
        //재시작 (인벤토리 비우기, 씬 바꾸기?)
        /*inventory.GetComponent<Inventory>().FreshInventory();
        player.Health = 100;
        lifeText.text = player.Health.ToString();
        player.transform.position = playerPos;*/
        if(currentScene.name is "Tutorial")
        {
            SceneManager.LoadScene("Tutorial");
        }
        else
        {
            SceneManager.LoadScene("Map1");
        }
    }

    public void KeySetting()
    {
        basePanel.SetActive(false);
        keySettingPanel.SetActive(true);
        pauseButton.SetActive(false);
        inventory.SetActive(false);
        life.SetActive(false);
        isGamePaused = true;
        isKeySettingOpen = true;
    }

    public void ReduceBGMVolume()
    {
        if (curBGMVolume > 0f)
        {
            curBGMVolume = curBGMVolume - 1f;
            BGMVolume.text = curBGMVolume.ToString();
            SoundManager.Inst.SetBGMVolume(curBGMVolume / 7f);
        }
    }

    public void IncreaseBGMVolume()
    {
        if (curBGMVolume < 7f)
        {
            curBGMVolume = curBGMVolume + 1f;
            BGMVolume.text = curBGMVolume.ToString();
            SoundManager.Inst.SetBGMVolume(curBGMVolume / 7f);
        }
    }

    public void ReduceSFXVolume()
    {
        if (curSFXVolume > 0f)
        {
            curSFXVolume = curSFXVolume - 1f;
            SFXVolume.text = curSFXVolume.ToString();
            SoundManager.Inst.SetSFXVolume(curSFXVolume / 7f);
        }
    }

    public void IncreaseSFXVolume()
    {
        if (curSFXVolume < 7f)
        {
            curSFXVolume = curSFXVolume + 1f;
            SFXVolume.text = curSFXVolume.ToString();
            SoundManager.Inst.SetSFXVolume(curSFXVolume / 7f);
        }
    }
}
