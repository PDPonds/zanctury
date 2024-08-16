using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DaySystem : MonoBehaviour
{
    CharacterMovement player;
    [SerializeField] private float timeMultiplier;
    [SerializeField] private float startHour;
    [SerializeField] private float endHour;
    [SerializeField] private TextMeshProUGUI timeText;

    private DateTime currentTime;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<CharacterMovement>();
        currentTime = DateTime.Now.Date + TimeSpan.FromHours(startHour);
    }
    private void Update()
    {
        UpdateTimeOfDay();
        if (currentTime.Hour >= endHour)
        {
            player.Die();
        }
    }
    private void UpdateTimeOfDay()
    {
        currentTime = currentTime.AddSeconds(Time.deltaTime * timeMultiplier);
        if (timeText != null)
        {
            timeText.text = currentTime.ToString("HH:mm");
        }
    }
}
