using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManipulationFeedback : MonoBehaviour
{
    [SerializeField] private Image image = null;

    [SerializeField] private Color slowColor = Color.yellow;
    [SerializeField] private Color stopColor = Color.grey;
    [SerializeField] private Color speedColor = Color.red;
    [SerializeField] private Color rewindNormalColor = Color.blue;
    [SerializeField] private Color rewindSlowColor = Color.green;
    [SerializeField] private Color rewindSpeedColor = Color.magenta;
    [SerializeField] private Color normalColor = Color.white;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<TimeManager>().OnTimeChange += TimeChanged;
    }

    private void TimeChanged(TimeChangeType timeChangeType)
    {
        Color color = Color.white;

        switch (timeChangeType)
        {
            case TimeChangeType.SLOW:
                color = slowColor;
                break;

            case TimeChangeType.STOP:
                color = stopColor;
                break;

            case TimeChangeType.SPEED:
                color = speedColor;
                break;

            case TimeChangeType.REWINDNORMAL:
                color = rewindNormalColor;
                break;

            case TimeChangeType.REWINDSLOW:
                color = rewindSlowColor;
                break;

            case TimeChangeType.REWINDSPEED:
                color = rewindSpeedColor;
                break;

            case TimeChangeType.NORMAL:
                color = normalColor;
                break;
        }

        image.color = color;
    }
}
