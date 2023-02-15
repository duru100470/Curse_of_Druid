using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private bool isGamePaused;
    private bool isKeySettingOpen;
    private Entity player;

    void Awake()
    {
        basePanel.SetActive(false);
        keySettingPanel.SetActive(false);
    }

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("pause : " + isGamePaused);
            Debug.Log("key : " + isKeySettingOpen);
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
        isGamePaused = false;
        isKeySettingOpen = false;
    }

    public void Menu()
    {
        basePanel.SetActive(true);
        pauseButton.SetActive(false);
        keySettingPanel.SetActive(false);
        inventory.SetActive(false);
        isGamePaused = true;
        isKeySettingOpen = false;
    }

    public void Home()
    {
        basePanel.SetActive(false);
        pauseButton.SetActive(true);
        inventory.SetActive(false);
        isGamePaused = false;
        isKeySettingOpen = false;
        //메인 화면으로 전환
    }

    public void Restart()
    {
        basePanel.SetActive(false);
        pauseButton.SetActive(true);
        inventory.SetActive(true);
        isGamePaused = false;
        isKeySettingOpen = false;
        //재시작
        inventory.GetComponent<Inventory>().FreshInventory();
        player.Health = 100;
        UIManager.Inst.SetPlayerLife(player.Health);
    }

    public void KeySetting()
    {
        basePanel.SetActive(false);
        keySettingPanel.SetActive(true);
        pauseButton.SetActive(false);
        inventory.SetActive(false);
        isGamePaused = true;
        isKeySettingOpen = true;
    }
}
