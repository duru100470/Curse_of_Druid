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
    private bool isGamePaused;
    private bool isKeySettingOpen;

    void Start()
    {
        basePanel.SetActive(false);
        keySettingPanel.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
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
        isGamePaused = false;
    }

    public void Menu()
    {
        basePanel.SetActive(true);
        pauseButton.SetActive(false);
        keySettingPanel.SetActive(false);
        isGamePaused = true;
        isKeySettingOpen = false;
    }

    public void Home()
    {
        basePanel.SetActive(false);
        pauseButton.SetActive(true);
        //메인 화면으로 나가기
    }

    public void Restart()
    {
        basePanel.SetActive(false);
        pauseButton.SetActive(true);
        //재시작
    }

    public void KeySetting()
    {
        basePanel.SetActive(false);
        keySettingPanel.SetActive(true);
        pauseButton.SetActive(false);
        isKeySettingOpen = true;
    }
}
