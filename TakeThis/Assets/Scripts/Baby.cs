﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Baby : MonoBehaviour
{
    BabyManager manager;

    internal float Mood;
    private float hugCoolDown = -1;

    public Image bGreyedOut;
    public Image bFill;

    [SerializeField]
    private SpriteRenderer faceRenderer = null;
    private int currentFaceIndex = -1;
    public int MoodLevel = -1;

    private const float MaxMood = 100f;

    private void Start()
    {
        this.manager = BabyManager.Instance;
        this.manager.Register(this);
        this.Mood = 50 + Random.Range(-20, 20);
    }

    private void Update()
    {
        float timeToFullySad = this.manager.TimeToFullySad;
        timeToFullySad = Mathf.Max(timeToFullySad, 1.0f);
        this.Mood -= (Time.deltaTime/timeToFullySad) * Baby.MaxMood;
        this.UpdateFace();

        if (this.hugCoolDown > 0.0f)
        {
            this.hugCoolDown -= Time.deltaTime;
            this.bGreyedOut.enabled = true;
            this.bFill.fillAmount += 1.0f/21.0f *Time.deltaTime;
        } else
        {
            this.bGreyedOut.enabled = false;
            this.bFill.fillAmount = 0.0f;
        }

        if (this.Mood < 90 && !this.GetComponent<Tackable>().IsHeld)
        {
            LayerMask carrotMask = LayerMask.GetMask("Carrot");
            Collider2D[] results = Physics2D.OverlapCircleAll(this.transform.position, this.manager.GrabRadius);
            for (int index = 0; index < results.Length; ++index)
            {
                Carrot carrot = results[index].gameObject.GetComponent<Carrot>();
                if (carrot != null)
                {
                    Tackable tackable = carrot.GetComponent<Tackable>();
                    if (tackable != null && !tackable.IsHeld)
                    {
                        tackable.NotifyTaken(this);
                        this.Mood += this.manager.CarrotBonus;
                        break;
                    }
                }
            }
        }

        this.Mood = Mathf.Clamp(this.Mood, 0f, Baby.MaxMood);
    }

    public bool PatPat()
    {
        if (this.Mood >= Baby.MaxMood)
        {
            return false;
        }

        this.Mood += this.manager.PatBonus;
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
        this.MoodLevel = faceIndex;

        if (this.currentFaceIndex != faceIndex)
        {
            this.currentFaceIndex = faceIndex;
            this.faceRenderer.sprite = this.manager.Faces[faceIndex];
        }
    }
}
