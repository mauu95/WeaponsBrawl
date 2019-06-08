using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerAfterAttackScript : MonoBehaviour {

    public TextMeshProUGUI timerUI;
    public float TimeToDisappear = 3f;

    private float timeLeft;
    
    public void SetTimer(float sec)
    {
        timeLeft = sec;
        timerUI.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (timerUI.gameObject.activeSelf)
        {
            if(timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                timerUI.text = timeLeft.ToString("0");
            }
            else
                StartCoroutine(DisableTimerAfterSec());
        }


    }

    IEnumerator DisableTimerAfterSec()
    {
        yield return new WaitForSeconds(TimeToDisappear);
        timerUI.gameObject.SetActive(false);
    }

    

}
