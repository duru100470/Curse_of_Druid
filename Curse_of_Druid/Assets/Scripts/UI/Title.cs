using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    [SerializeField]
    private string SceneToLoad;

    public void LoadGame()
    {
        SceneManager.LoadScene(SceneToLoad);
    }
}
