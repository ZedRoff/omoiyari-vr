using System.Collections;
using TMPro;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
  
    public float delay;
    public TextMeshProUGUI timer;

    private Coroutine currentTimer;
    public GameScript gameScript;
    void Start()
    {
        timer.gameObject.SetActive(false);
        gameScript = GameObject.Find("Game Manager").GetComponent<GameScript>();
    }

    IEnumerator Timer()
    {
        float remainTime = delay;
        while (remainTime > 0)
        {
          
           
            int minutes = Mathf.FloorToInt(remainTime / 60);
            int seconds = Mathf.FloorToInt(remainTime % 60);

            timer.text = $"{minutes:00}:{seconds:00}";

            remainTime -= Time.deltaTime;
            yield return null;
        }


        timer.text = "00:00";
      
    }

    public void StopTimer()
    {
        if (currentTimer != null)
        {
            StopCoroutine(currentTimer);
            currentTimer = null;
        }

        timer.text = "00:00";
        timer.gameObject.SetActive(false);
    
    }

    public void StartTimer(float pDelay)
    {
        delay = pDelay;
        if (currentTimer != null)
        {
            timer.gameObject.SetActive(false);
            StopCoroutine(currentTimer);
        }
        timer.gameObject.SetActive(true);
        currentTimer = StartCoroutine(Timer());
    }
   
}
