using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void QuitButton()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void RestartButton()
    {
        Debug.Log("Restart");
        SceneManager.LoadScene(0);
    }
}
