using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private NewBehaviourScript player;

    [Header("Component")]
    public TextMeshProUGUI timerText;

    [Header("Timer Settings")]
    public float currentTime;
    public bool countDown;

    [Header("Format Settings")]
    public bool hasFormat;
    public TimerFormats format;
    private Dictionary<TimerFormats, string> timeFormats = new Dictionary<TimerFormats, string>();

    void Start()
    {
        player = FindObjectOfType<NewBehaviourScript>();
        timeFormats.Add(TimerFormats.Whole, "0");
        timeFormats.Add(TimerFormats.TenthDecimal, "0.0");
        timeFormats.Add(TimerFormats.HundrethsDecimal, "0.00");
    }

    void Update()
    {
        currentTime = countDown ? currentTime -= Time.deltaTime : currentTime += Time.deltaTime;
        SetTimerText();
    }

    private void SetTimerText()
    {
        timerText.text = hasFormat ? currentTime.ToString(timeFormats[format]) : currentTime.ToString();
        
        /*if (player.PlayerDeath == true)
        {
            stopTimer = currentTime;
            timerText.text = hasFormat ? stopTimer.ToString(timeFormats[format]) : currentTime.ToString(); 
            //gameObject.SetActive(false);
            //timerText.gameObject.SetActive(false);
        } */ 
    }

    public enum TimerFormats
    {
        Whole, 
        TenthDecimal,
        HundrethsDecimal
    }
}
