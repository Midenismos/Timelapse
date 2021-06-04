using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindableLight : Rewindable
{



    [SerializeField] private FlickeringLight flickeringLight = null;
    public override void StartRewind()
    {
        base.StartRewind();
    }

    public override void Rewind(float deltaGameTime, float totalTime)
    {
        base.Rewind(deltaGameTime, totalTime);

        // Script presque identique à FlickeringLight mais les timers avancent en fonction du deltaGameTime au lieu de reculer en fonction du Time.deltaTime
        if (flickeringLight.timerLightEnabled >= 0 && flickeringLight.timerLightEnabled <= flickeringLight.timerLightEnabledMax)
        {
            flickeringLight.timerLightEnabled += deltaGameTime;
        }
        else
        {
            flickeringLight.lightEnabled = false;
        }

        if (flickeringLight.timerLightDisabled >= 0 && flickeringLight.timerLightDisabled <= flickeringLight.timerLightDisabledMax)
        {
            flickeringLight.timerLightDisabled += deltaGameTime;
        }
        else
        {
            flickeringLight.lightEnabled = true;
        }

        if (flickeringLight.lightEnabled == false)
        {
            flickeringLight.timerLightEnabled = 0;
        }
        else
        {
            flickeringLight.timerLightDisabled = 0;
        }
    }
}

