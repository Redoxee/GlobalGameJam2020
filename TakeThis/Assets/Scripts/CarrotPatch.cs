using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotPatch : MonoBehaviour, Tackable.TackeWatcher
{
    enum States
    {
        Growing = 0,
        ReadyToBePickedUp = 1,
        WaitingForConsumption = 2,
    }

    public float TimeToGrow = 10;
    public Sprite CarrotGrowing = null;
    public Sprite CarrotReady = null;
    public Sprite CarrotPicked = null;

    [SerializeField]
    private SpriteRenderer PlantRenderer = null;
    States currentState = States.Growing;
    private delegate void StateDelegate();
    StateDelegate[] statesUpdate = new StateDelegate[3];

    [SerializeField]
    private Transform carrotSpawn = null;

    [SerializeField]
    private Tackable Carrote = null;

    float growthTimer = 0;

    private void Awake()
    {
        this.statesUpdate[(int)States.Growing] = this.Update_Growing;
        this.statesUpdate[(int)States.ReadyToBePickedUp] = this.Update_ReadyToBePickedUp;
        this.statesUpdate[(int)States.WaitingForConsumption] = this.Update_WaitingForConsumption;

        this.ResetCarrot();

        this.currentState = States.Growing;
        this.growthTimer = Random.Range(0, this.TimeToGrow);
        this.PlantRenderer.sprite = this.CarrotGrowing;
    }

    private void ResetCarrot()
    {
        this.currentState = States.Growing;
        this.Carrote.gameObject.SetActive(false);
        this.Carrote.transform.SetParent(this.carrotSpawn);
        this.Carrote.transform.localPosition = Vector3.zero;
        this.Carrote.GetComponent<Collider2D>().enabled = false;
    }

    private void Update()
    {
        this.statesUpdate[(int)this.currentState]();
    }

    private void Update_Growing()
    {
        this.growthTimer += Time.deltaTime;
        if (this.growthTimer >= this.TimeToGrow)
        {
            this.growthTimer = 0;
            this.currentState = States.ReadyToBePickedUp;
            this.Carrote.gameObject.SetActive(true);
            this.Carrote.GetComponent<SpriteRenderer>().enabled = false;
            this.PlantRenderer.sprite = this.CarrotReady;
            this.Carrote.GetComponent<Collider2D>().enabled = true;
        }
    }

    private void Update_ReadyToBePickedUp()
    {
    }

    private void Update_WaitingForConsumption()
    {
    }

    public void OnTacken(Tackable tackable, object tacker)
    {
        if (this.currentState == States.ReadyToBePickedUp)
        {
            this.currentState = States.WaitingForConsumption;
            this.Carrote.GetComponent<SpriteRenderer>().enabled = true;
            this.PlantRenderer.sprite = this.CarrotPicked;
        }
        else if (this.currentState == States.WaitingForConsumption)
        {
            if (tacker is Baby)
            {
                this.currentState = States.Growing;
                this.growthTimer = 0;
                this.PlantRenderer.sprite = this.CarrotGrowing;

                this.ResetCarrot();
            }
        }
    }

    public void OnDropped(Tackable tackable, object dropper)
    {
    }
}
