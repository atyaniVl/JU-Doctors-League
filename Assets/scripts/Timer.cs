using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeRemaining;
    [SerializeField] TextMeshProUGUI timeText;
    bool isTimerRunning = false;
    [SerializeField] UI_manager UImanager;
    [SerializeField] Stage3UI stage3UI;
    [SerializeField] Color normalCol;
    [SerializeField] Button timerBTN;

    void Start()
    {
        timeText.color = normalCol;
    }
    public void startTimer(int sec)
    {
        isTimerRunning = true;
        timeRemaining = sec;
        StartCoroutine(timer());
        SoundManager.instance.timerSound();
        timerBTN.interactable = false;
    }
    public void stopTimer()
    {
        isTimerRunning = false;
        StopCoroutine(timer());
        timeText.text = "00:00";
        SoundManager.instance.timerSoundEnd();
        timerBTN.interactable = true;
    }

    IEnumerator timer()
    {
        while(isTimerRunning)
        {
            if (timeRemaining > 1)
            {
                timeRemaining -= 1;
                if (timeRemaining < 10)
                {
                    timeText.color = Color.red;
                }
                else 
                {
                    timeText.color = normalCol; 
                }
                displayTime(timeRemaining);
            }
            else
            {
                print("time out");
                stopTimer();
                SoundManager.instance.timeoutSound();
                if(UImanager!=null)
                    UImanager.NextLevel(4);
                else if (stage3UI!=null)
                    stage3UI.NextLevel(4);
            }
            yield return new WaitForSeconds(1);
        }
        yield return 0;
    }
    void displayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60), second = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, second);
    }
}