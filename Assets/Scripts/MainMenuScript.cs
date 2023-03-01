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

    public void InstructionButton()
    {
        //Debug.Log("TEST SCENE NOT YET MADE");
        SceneManager.LoadScene("testInstScene");
    }

    public void InstructionButton2()
    {
        //Debug.Log("TEST SCENE NOT YET MADE");
        SceneManager.LoadScene("testInstScene2");
    }

    public void MainMenuButton()
    {
        //Debug.Log("MAIN MENU SCENE NOT YET MADE");
        SceneManager.LoadScene("testMenu");
    }

    public void ExitButton()
    {
        Debug.Log("EXIT PRESSED");
        //Application.Quit();
    }
}
