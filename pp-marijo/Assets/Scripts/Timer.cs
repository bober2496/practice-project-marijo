using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    //Ido sebesseg manipulalashoz
    public float SpeedOfTimeInPercent = 100;

    //Time szamitashoz
    public int Seconds, Minutes;
    [SerializeField] private double _UnityFixedTimer;
    private double _deltaSecondSum;

    //Time kijelzeshez
    private Text _timerText;
    private string _timerMinutes, _timerSeconds;
    
    private void Awake()
    {
        _timerText = GetComponent<Text>();
    }

    void Start()
    {
        Seconds = Minutes = 0;
        _deltaSecondSum = Time.fixedTime;
        _timerSeconds = _timerMinutes = "00";
    }

    private void Update()
    {
        //Ido sebesseg allitas
        if (SpeedOfTimeInPercent > 10000.0f)
        {
            SpeedOfTimeInPercent = 10000.0f;
            Debug.LogWarning("Scale maximum limit reached.\n" + "Value set to 10000.");
        }
        else if (SpeedOfTimeInPercent < 0.0f)
        {
            SpeedOfTimeInPercent = 0.0f;
            Debug.LogWarning("Scale minimum limit reached.\n" + "Value set to 0.");
        }
        else
            Time.timeScale = SpeedOfTimeInPercent / 100;
    }

    void FixedUpdate()
    {
        _UnityFixedTimer = Time.fixedTime;
        _deltaSecondSum += Time.fixedDeltaTime;

        if (Seconds == 60)
        {
            _deltaSecondSum -= 60.0f;
            Seconds = 0;
            Minutes++;
        }
        else Seconds = Convert.ToInt32(_deltaSecondSum - _deltaSecondSum % 1);

        if(Minutes == 60)
        {
            Minutes = 0;
        }

        _timerSeconds = (Seconds < 10 ? "0" : null) + Seconds.ToString();
        _timerMinutes = (Minutes < 10 ? "0" : null) + Minutes.ToString();
        _timerText.text = _timerMinutes + ":" + _timerSeconds;        
    }
}
