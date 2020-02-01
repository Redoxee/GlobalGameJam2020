using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baby : MonoBehaviour
{
    BabyManager manager;

    internal float Mood;
    float patCoolDown = -1;
    float hugCoolDown = -1;

    void Start()
    {
        this.manager = BabyManager.Instance;    
    }

    private void Update()
    {
        float timeToFullySad = this.manager.TimeToFullySad;
        timeToFullySad = Mathf.Max(timeToFullySad, 1.0f);
        this.Mood -= Mathf.Max(60.0f * Time.deltaTime / timeToFullySad, 0.0f, 0.0f);

        if (this.patCoolDown > 0.0f)
        {
            this.patCoolDown -= Time.deltaTime;
        }

        if (this.hugCoolDown > 0.0f)
        {
            this.hugCoolDown -= Time.deltaTime;
        }
    }

    public bool PatPat()
    {
        if (this.patCoolDown > 0.0f)
        {
            return false;
        }

        if (this.Mood >= 100)
        {
            return false;
        }

        this.Mood += this.manager.PatBonus;
        this.patCoolDown = this.manager.PatTimer;
        this.Mood = Mathf.Clamp(this.Mood, 0, 100);

        return true;
    }

    public bool Hug()
    {
        if (this.hugCoolDown > 0.0f)
        {
            return false;
        }

        if (this.Mood >= 100)
        {
            return false;
        }

        this.Mood += this.manager.HugBonus;
        this.hugCoolDown = this.manager.HugTimer;

        this.Mood = Mathf.Clamp(this.Mood, 0, 100);

        return true;
    }
}
