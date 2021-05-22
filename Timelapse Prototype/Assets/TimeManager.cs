using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private RewindManager rewindManager = null;
    
    //Ne rien changer là dedans

    public float multiplier;
    public float baseMultiplier = 1f;
    private float newMultiplier = 0f;
    public float timer = 0f;

    private float timerLerpMin = 0f;
    private float timerLerpMax = 1f;
    private float timerLerp = 1f;
    private bool goBackTimeChange = true;

    // Start is called before the first frame update
    void Start()
    {
        multiplier = baseMultiplier;
        rewindManager.OnRewindStopped += RewindStopped;
    }

    // Update is called once per frame
    void Update()
    {
        //Keyboard controlls for testing
        if (Input.GetKeyDown(KeyCode.R))
        {
            //StartRewind();
        }

        //Décrémente le timer
        if (timer >= 0)
        {
            timer -= Time.deltaTime;
        }

        //Entamme la réinitialisation du multiplier une second avant la fin du timer fixé par Duration dans le TimeChanger
        if (timer <=1)
        {
            if (goBackTimeChange == false)
            {
                EndTimeChange();
            }
        }

        //Gère la transition smooth entre deux changements de temps 
        if (timerLerp <= timerLerpMax)
        {
            timerLerp += Time.deltaTime;

            if (goBackTimeChange == false)
            {
                multiplier = Mathf.Lerp(baseMultiplier, newMultiplier, timerLerp);
            }
            else
            {
                multiplier = Mathf.Lerp(newMultiplier, baseMultiplier, timerLerp);
            }
        }
    }

    //Commence le changement de temps (appellé par un TimeChanger)
    public void StartTimeChange(float NewMultiplier, float timeChangeDuration)
    {
        if(newMultiplier >= 0)
        {
            timerLerp = timerLerpMin;
            newMultiplier = NewMultiplier;
            goBackTimeChange = false;
            timer = timeChangeDuration;
        } else
        {
            Debug.Log("started Rewind");
            StartRewind(-NewMultiplier, timeChangeDuration);
        }
        
    }

    //Termine le changement de temps
    public void EndTimeChange()
    {
        timerLerp = timerLerpMin;
        goBackTimeChange = true;
    }

    public void StartRewind(float speed, float duration)
    {
        multiplier = 0;
        timerLerp = timerLerpMax + 1;
        rewindManager.StartRewind(speed, duration);
    }

    private void RewindStopped()
    {
        multiplier = 1;
        timerLerp = timerLerpMax + 1;
    }
}
