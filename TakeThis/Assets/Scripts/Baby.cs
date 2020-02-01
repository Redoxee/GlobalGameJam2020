using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baby : MonoBehaviour
{
    BabyManager manager;

    internal float Mood;
    private float patCoolDown = -1;
    private float hugCoolDown = -1;

    [SerializeField]
    private SpriteRenderer faceRenderer = null;
    private int currentFaceIndex = -1;

    private const float MaxMood = 100f;

    private void Start()
    {
        this.manager = BabyManager.Instance;
        this.Mood = 50 + Random.Range(-20, 20);
    }

    private void Update()
    {
        float timeToFullySad = this.manager.TimeToFullySad;
        timeToFullySad = Mathf.Max(timeToFullySad, 1.0f);
        this.Mood -= (Time.deltaTime/timeToFullySad) * Baby.MaxMood;
        this.Mood = Mathf.Clamp(this.Mood, 0f, Baby.MaxMood);
        this.UpdateFace();
        Debug.Log(this.Mood);

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

        if (this.Mood >= Baby.MaxMood)
        {
            return false;
        }

        this.Mood += this.manager.PatBonus;
        this.patCoolDown = this.manager.PatTimer;
        this.Mood = Mathf.Clamp(this.Mood, 0, Baby.MaxMood);
        this.UpdateFace();
        Debug.Log("PAT PAT!");

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

        this.Mood = Mathf.Clamp(this.Mood, 0, Baby.MaxMood);
        this.UpdateFace();
        Debug.Log("HUG!");

        return true;
    }

    private void UpdateFace()
    {
        int numberOfFaces = this.manager.Faces.Length;
        int faceIndex = Mathf.FloorToInt((this.Mood / Baby.MaxMood) * numberOfFaces);
        faceIndex = Mathf.Clamp(faceIndex, 0, numberOfFaces);

        if (this.currentFaceIndex != faceIndex)
        {
            this.currentFaceIndex = faceIndex;
            this.faceRenderer.sprite = this.manager.Faces[faceIndex];
        }
    }
}
