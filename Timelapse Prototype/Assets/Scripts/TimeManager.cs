using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public enum TimeChangeType
{
    REWINDNORMAL,
    REWINDSPEED,
    REWINDSLOW,
    STOP,
    SLOW,
    SPEED,
    NORMAL
};

[Serializable]
public struct TimeChange
{
    public TimeChangeType type;
    public float duration;
    public float speed;
}

public class TimeManager : MonoBehaviour
{
    [Header("Loop Settings")]
    [SerializeField] private float loopDuration = 10;

    [Header("Timescale Settings")]
    [SerializeField] private float startingMultiplier = 1;
    [SerializeField] private float timeLerpDuration = 1;
    [SerializeField] private int maximumActiveTimeChangers = 2;

    [Header("Time Manipulation Settings")]
    [SerializeField] private float stopTimeMultiplier = 0;
    [SerializeField] private float slowTimeMultiplier = 0.5f;
    [SerializeField] private float normalTimeMultiplier = 1;
    [SerializeField] private float speedTimeMultiplier = 2;
    [SerializeField] private float rewindSlowTimeMultiplier = 0.5f;
    [SerializeField] private float rewindTimeMultiplier = 1;
    [SerializeField] private float rewindSpeedTimeMultiplier = 2;
    [SerializeField] private float timeManipulationDuration = 120;

    [Header("References")]
    [SerializeField] private RewindManager rewindManager = null;

    public event Action<TimeChangeType> OnTimeChange = null;
    
    public float multiplier = 1;
    public float timeChangeTimer= 0f;

    public float currentLoopTime = 0;
    public float currentTolerance = 0;

    private bool hasTimeManipulation = false;
    private bool hasTimeChange = false;
    private TimeChange currentTimeChange;

    private bool hasStandartTimeChange = false;

    private bool mustResumeCurrentTimeChange = false;

    private int currentlyActiveTimechangers = 0;

    private Coroutine TimeLerpCoroutine = null;

    private List<ITimeStoppable> timeStoppables = new List<ITimeStoppable>();

    public bool IsTimeStopped { get => multiplier == 0;}

    private float timeManipulationRemaining = 0;

    private TimeChangeType currentTimeChangeType = TimeChangeType.NORMAL;


    // Start is called before the first frame update
    void Start()
    {
        multiplier = startingMultiplier;
        //rewindManager.OnRewindStopped += RewindStopped;

        //Find other way to gather timestoppables
        //timeStoppables = FindObjectsOfType<UnityEngine.Object>().OfType<ITimeStoppable>().ToList();
    }

    // Update is called once per frame
    void Update()
    {

        if (!rewindManager.isRewinding)
        {
            if(!IsTimeStopped)
            currentLoopTime += Time.deltaTime;

            if(currentLoopTime >= loopDuration)
            {
                RestartLoop();
            }
        }
        if(hasStandartTimeChange)
        {
            //timeChangeTimer -= Time.unscaledDeltaTime;

            //if(timeChangeTimer <= 0)
            //{
            //    EndStandartTimeChange();
            //}
        }

        if(hasTimeManipulation)
        {
            timeManipulationRemaining -= Time.unscaledDeltaTime;

            if(timeManipulationRemaining <= 0)
            {
                EndTimeManipulation();
            }
        }
    }

    public void RegisterTimeStoppable(ITimeStoppable timeStoppable)
    {
        timeStoppables.Add(timeStoppable);
    }

    public void UnRegisterTimeStoppable(ITimeStoppable timeStoppable)
    {
        timeStoppables.Remove(timeStoppable);
    }

    public void StartTimeManipulation()
    {
        timeManipulationRemaining = timeManipulationDuration;
        hasTimeManipulation = true;
    }

    private void EndTimeManipulation()
    {
        hasTimeManipulation = false;

        if(hasStandartTimeChange)
        {
            EndStandartTimeChange();
        } else if (rewindManager.isRewinding)
        {
            rewindManager.EndRewind();
        }

        ChangeTimeScale(normalTimeMultiplier);

        currentTimeChangeType = TimeChangeType.NORMAL;
        OnTimeChange?.Invoke(currentTimeChangeType);
    }

    //Commence le changement de temps (appellé par un TimeChanger)
    //public void StartTimeChange(TimeChange timeChange, float toleranceCost = 0)
    //{
    //    PayToleranceCost(toleranceCost);

    //    if (!hasTimeChange)
    //    {
    //        if (timeChange.type == TimeChangeType.REWIND)
    //        {
    //            StartRewind(timeChange.speed, timeChange.duration);
    //        }
    //        else
    //        {
    //            StartStandartTimeChange(timeChange.speed, timeChange.duration);
    //        }
    //        currentTimeChange = timeChange;
    //    }
    //    else
    //    {
    //        if (currentTimeChange.type == TimeChangeType.SLOW)
    //        {
    //            if (timeChange.type == TimeChangeType.REWIND)
    //            {
    //                EndStandartTimeChange(false);
    //                StartRewind(timeChange.speed, timeChangeTimer + timeChange.duration);
    //            }
    //            else if (timeChange.type == TimeChangeType.STOP)
    //            {
    //                PauseCurrentTimeChange();
    //                StartStandartTimeChange(timeChange.speed, timeChange.duration);
    //            }
    //            else if (timeChange.type == TimeChangeType.ACCELERATE)
    //            {
    //                EndStandartTimeChange();
    //            }
    //            else if (timeChange.type == TimeChangeType.SLOW)
    //            {
    //                timeChangeTimer += timeChange.duration;
    //            }
    //        }
    //        else if (currentTimeChange.type == TimeChangeType.ACCELERATE)
    //        {
    //            if (timeChange.type == TimeChangeType.REWIND)
    //            {
    //                EndStandartTimeChange(false);
    //                StartRewind(timeChange.speed, timeChangeTimer + timeChange.duration);
    //            }
    //            else if (timeChange.type == TimeChangeType.STOP)
    //            {
    //                PauseCurrentTimeChange();
    //                StartStandartTimeChange(timeChange.speed, timeChange.duration);
    //            }
    //            else if (timeChange.type == TimeChangeType.SLOW)
    //            {
    //                EndStandartTimeChange();
    //            }
    //            else if (timeChange.type == TimeChangeType.ACCELERATE)
    //            {
    //                timeChangeTimer += timeChange.duration;
    //            }
    //        } else if(currentTimeChange.type == TimeChangeType.REWIND)
    //        {
    //            if(timeChange.type == TimeChangeType.REWIND)
    //            {
    //                rewindManager.AddDuration(timeChange.duration);
    //            } else if(timeChange.type == TimeChangeType.STOP)
    //            {
    //                PauseCurrentTimeChange();
    //                StartStandartTimeChange(timeChange.speed, timeChange.duration);
    //            } else if(timeChange.type == TimeChangeType.SLOW || timeChange.type == TimeChangeType.ACCELERATE)
    //            {
    //                rewindManager.AddDuration(timeChange.duration);
    //                rewindManager.ChangeSpeed(timeChange.speed);
    //            }
    //        } else if(currentTimeChange.type == TimeChangeType.STOP)
    //        {
    //            if (timeChange.type == TimeChangeType.REWIND)
    //            {
    //                currentTimeChange = timeChange;
    //                mustResumeCurrentTimeChange = true;
    //            } else if(timeChange.type == TimeChangeType.STOP)
    //            {
    //                timeChangeTimer += timeChange.duration;
    //            } else if(timeChange.type == TimeChangeType.SLOW || timeChange.type == TimeChangeType.ACCELERATE)
    //            {
    //                currentTimeChange = timeChange;
    //                mustResumeCurrentTimeChange = true;
    //            }
    //        }
    //    }

    //    IncrementCurrentlyActiveTimeChangers();
    //}

    public void StopTimePressed()
    {
        if (hasTimeManipulation)
        {
            StartStandartTimeChange(stopTimeMultiplier);
            currentTimeChangeType = TimeChangeType.STOP;
            OnTimeChange?.Invoke(currentTimeChangeType);
        }
    }

    public void SlowTimePressed()
    {
        if (hasTimeManipulation)
        {
            StartStandartTimeChange(slowTimeMultiplier);
            currentTimeChangeType = TimeChangeType.SLOW;
            OnTimeChange?.Invoke(currentTimeChangeType);
        }
    }

    public void NormalTimePressed()
    {
        if (hasTimeManipulation)
        {
            StartStandartTimeChange(normalTimeMultiplier);
            currentTimeChangeType = TimeChangeType.NORMAL;
            OnTimeChange?.Invoke(currentTimeChangeType);
        }
    }

    public void SpeedTimePressed()
    {
        if (hasTimeManipulation)
        {
            StartStandartTimeChange(speedTimeMultiplier);
            currentTimeChangeType = TimeChangeType.SPEED;
            OnTimeChange?.Invoke(currentTimeChangeType);
        }
    }

    public void RewindTimeSlowPressed()
    {
        if (hasTimeManipulation)
        {
            StartRewind(rewindSlowTimeMultiplier);
            currentTimeChangeType = TimeChangeType.REWINDSLOW;
            OnTimeChange?.Invoke(currentTimeChangeType);
        }
    }

    public void RewindTimeNormalPressed()
    {
        if (hasTimeManipulation)
        {
            StartRewind(rewindTimeMultiplier);
            currentTimeChangeType = TimeChangeType.REWINDNORMAL;
            OnTimeChange?.Invoke(currentTimeChangeType);
        }
    }

    public void RewindTimeSpeedPressed()
    {
        if (hasTimeManipulation)
        {
            StartRewind(rewindSpeedTimeMultiplier);
            currentTimeChangeType = TimeChangeType.REWINDSPEED;
            OnTimeChange?.Invoke(currentTimeChangeType);
        }
    }

    private void StartStandartTimeChange(float speed)
    {
        if(rewindManager.isRewinding)
        {
            rewindManager.EndRewind();
        }

        hasStandartTimeChange = true;

        StartTimeLerp(speed);
    }
    public void EndStandartTimeChange(bool executeTimeLerp = true)
    {
        hasStandartTimeChange = false;

        //if(mustResumeCurrentTimeChange)
        //{
        //    if(currentTimeChange.type == TimeChangeType.SLOW || currentTimeChange.type == TimeChangeType.ACCELERATE)
        //    {
        //        StartStandartTimeChange(currentTimeChange.speed, currentTimeChange.duration);
        //    } else
        //    {
        //        StartRewind(currentTimeChange.speed, currentTimeChange.duration);
        //    }

        //    mustResumeCurrentTimeChange = false;
        //    currentlyActiveTimechangers--;
        //    return;
        //}

        //currentlyActiveTimechangers = 0;

        if(executeTimeLerp)
        {
            StartTimeLerp(normalTimeMultiplier);
        }
    }

    private void StartRewind(float speed)
    {
        if(!rewindManager.isRewinding)
        {
            ChangeTimeScale(0);
            hasStandartTimeChange = false;
        }
        
        rewindManager.StartRewind(speed);
    }

    private void RewindStopped()
    {
        ChangeTimeScale(1);

    }

    private void StartTimeLerp(float speed)
    {
        if (TimeLerpCoroutine != null)
        {
            StopCoroutine(TimeLerpCoroutine);
        }
        TimeLerpCoroutine = StartCoroutine(TimeLerp(multiplier, speed, timeLerpDuration));
    }

    private IEnumerator TimeLerp(float oldMultiplier, float newMultiplier, float duration)
    {
        float timeCounter = 0;

        while (timeCounter < duration)
        {
            timeCounter += Time.unscaledDeltaTime;
            
            ChangeTimeScale(Mathf.Lerp(oldMultiplier, newMultiplier, timeCounter / duration));
            yield return null;
        }
        
        ChangeTimeScale(newMultiplier);
    }

    private void PauseCurrentTimeChange()
    {
        if(currentTimeChange.type == TimeChangeType.SLOW  || currentTimeChange.type == TimeChangeType.SPEED)
        {
            EndStandartTimeChange(false);
            currentTimeChange.duration = timeChangeTimer;
        } else if (currentTimeChange.type == TimeChangeType.REWINDNORMAL)
        {
            currentTimeChange.duration = rewindManager.EndRewind();
        }
        mustResumeCurrentTimeChange = true;

    }

    private void IncrementCurrentlyActiveTimeChangers()
    {
        currentlyActiveTimechangers++;

        if(currentlyActiveTimechangers > maximumActiveTimeChangers)
        {
            RestartLoop();
        }
    }

    public void RestartLoop()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void PayToleranceCost (float toleranceCost)
    {
        currentTolerance += toleranceCost * loopDuration;

        if (currentTolerance >= loopDuration)
        {
            RestartLoop();
        }
    }

    private void ChangeTimeScale (float newTimeScale)
    {
        float oldMultiplier = multiplier;
        multiplier = newTimeScale;
        if(multiplier != 0)
        {
            Time.timeScale = newTimeScale;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;

            if(oldMultiplier == 0)
            {
                ResumeTime();
            }
        } else {
            Time.timeScale = 1;
            Time.fixedDeltaTime = 0.02f;

            if(oldMultiplier != 0)
            {
                StopTime();
            }
        }
    }

    private void StopTime()
    {
        for (int i = 0; i < timeStoppables.Count; i++)
        {
            timeStoppables[i].StartTimeStop();
        }
    }

    private void ResumeTime()
    {
        for (int i = 0; i < timeStoppables.Count; i++)
        {
            timeStoppables[i].EndTimeStop();
        }
    }
}
