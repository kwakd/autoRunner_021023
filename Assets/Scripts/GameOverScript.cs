using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class GameOverScript : MonoBehaviour
{
    public TextMeshProUGUI pointsText;

    [SerializeField] private AudioSource   gameOverSoundEffect;

    public void Setup(float score)
    {
        gameOverSoundEffect.Play();
        gameObject.SetActive(true);
        pointsText.text = Mathf.Round(score) + " POINTS";
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("testScene2");
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("testMenu");
    }

}
