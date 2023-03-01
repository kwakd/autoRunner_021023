using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class countDownController : MonoBehaviour
{
    public int countDownTime;
    public TMP_Text countDownText;

    private int countDownTimeBase;
    private testPlayerController myPlayerController;

    private void Start()
    {
        myPlayerController = FindObjectOfType<testPlayerController>();
        countDownTimeBase = countDownTime;
        StartCoroutine(countDownToStart());
    }

    public void startCountDownRespawn()
    {
        Debug.Log("RESPAWN COUNTDOWN HIT");
        countDownTime = 1;
        countDownText.gameObject.SetActive(true);
        myPlayerController.countDownDone = false;
        StartCoroutine(countDownToStart());
    }

    IEnumerator countDownToStart()
    {
        while(countDownTime > 0)
        {
            countDownText.text = countDownTime.ToString();
            
            yield return new WaitForSeconds(1f);

            countDownTime--;
        }

    countDownText.text = "GO!";

    myPlayerController.countDownDone = true;

    yield return new WaitForSeconds(0.5f);

    countDownText.gameObject.SetActive(false);

    }


}
