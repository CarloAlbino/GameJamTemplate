using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Timer
{
    public string name;
    public float remainingTime;

    public void DecrementTime()
    {
        remainingTime -= Time.deltaTime;
    }

    public void SetTime(float t)
    {
        Debug.Log("Decreasing time " + remainingTime);
        remainingTime = t;
    }
}

public class TimerController : Singleton<TimerController>{
    private List<Timer> m_timers = new List<Timer>();

    void Update()
    {
        foreach (Timer t in m_timers)
        {
            t.DecrementTime();
        }
    }

    public void SetNewTimer(string name, float time)
    {
        foreach(Timer timer in m_timers)
        {
            if (timer.name == name)
            {
                Debug.LogError("Timer already exists");
                return;
            }
        }

        Timer t = new Timer();
        t.name = name;
        t.remainingTime = time;

        m_timers.Add(t);
    }

    public void ResetTimer(string name, float time)
    {
        foreach (Timer timer in m_timers)
        {
            if (timer.name == name)
            {
                timer.SetTime(time);
                return;
            }
        }

        Debug.LogError("Timer does not exists");
    }

    public void DeleteTimer(string name)
    {
        foreach (Timer timer in m_timers)
        {
            if (timer.name == name)
            {
                m_timers.Remove(timer);
                return;
            }
        }

        Debug.LogError("Timer does not exists");
    }

    public float GetRemainingTime(string name)
    {
        int i = 0;
        foreach (Timer timer in m_timers)
        {
            if (timer.name == name)
            {
                return m_timers[i].remainingTime;
            }
            i++;
        }

        Debug.LogError("Timer does not exists");
        return 0.0f;
    }
}
