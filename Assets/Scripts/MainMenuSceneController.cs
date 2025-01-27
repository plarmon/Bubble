using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSceneController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Course Whitebox");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
