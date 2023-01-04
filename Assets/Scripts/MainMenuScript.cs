using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuScript : MonoBehaviour
{
    public void PlayButton()
    {
        SceneManager.LoadScene("testScene2");
    }

    public void ExitButton()
    {
        Debug.Log("EXIT PRESSED");
        //Application.Quit();
    }
}
