using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class GameOverScript : MonoBehaviour
{
    public TextMeshProUGUI pointsText;

    public void Setup(float score)
    {
        gameObject.SetActive(true);
        pointsText.text = Mathf.Round(score) + " POINTS";
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("testScene2");
    }

    public void ExitButton()
    {
        Debug.Log("TODO: MAIN MENU");
    }

}
