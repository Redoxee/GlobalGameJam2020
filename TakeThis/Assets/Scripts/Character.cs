using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Rigidbody2D Rigidbody;

    public Transform WorldTransform;
    public CameraScript Camera;
    public SpriteRenderer CharacterRenderer = null;
    public Sprite CharacterFront = null;
    public Sprite CharacterLeft = null;
    public Sprite CharacterBack = null;

    enum Facing
    {
        Front,
        Right,
        Back,
        Left,
    }

    private Facing currentlyFacing = Facing.Front;

    private float patCoolDown = -1;
    public float PatTimer = 1.0f;

    private List<GameObject> availableTackable = new List<GameObject>();
    private GameObject LastTackableMet = null;
    private GameObject CurrentHeld = null;

    private Vector3 WantedRelativePos = new Vector3(1, 0, 0);
    private float deadLift = .005f;
    private Vector3 wantedVelocity = Vector3.zero;

    private Baby LastBabyMet = null;
    private List<Baby> availableBaby = new List<Baby>();

    private void Update()
    {
        if (this.CurrentHeld != null)
        {
            Vector3 localPos = this.CurrentHeld.transform.localPosition;
            if ((localPos - this.WantedRelativePos).sqrMagnitude > this.deadLift)
            {
                Vector3 dampedPos = Vector3.SmoothDamp(localPos, this.WantedRelativePos, ref this.wantedVelocity, .25f, 20.0f, Time.deltaTime);
                this.CurrentHeld.transform.localPosition = dampedPos;
            }
            else
            {
                this.wantedVelocity = Vector3.zero;
            }
        }

        if (this.patCoolDown > 0.0f)
        {
            this.patCoolDown -= Time.deltaTime;
        }
    }

    private void LateUpdate()
    {
        Vector2 velocity = this.Rigidbody.velocity;
        Facing nextFace = this.currentlyFacing;

        if (velocity.sqrMagnitude > .1)
        {
            Vector2 direction = velocity.normalized;
            if (direction.y < -0.866)
            {
                nextFace = Facing.Front;
            }
            else if (direction.y > 0.866)
            {
                nextFace = Facing.Back;
            }
            else if (direction.x > 0)
            {
                nextFace = Facing.Right;
            }
            else
            {
                nextFace = Facing.Left;
            }
        }
        else
        {
            nextFace = Facing.Front;
        }

        if (nextFace != this.currentlyFacing)
        {
            this.currentlyFacing = nextFace;

            switch(nextFace)
            {
                case Facing.Front:
                    this.CharacterRenderer.sprite = this.CharacterFront;
                    this.CharacterRenderer.transform.localScale = new Vector3(1, 1, 1);
                    break;
                case Facing.Back:
                    this.CharacterRenderer.sprite = this.CharacterBack;
                    this.CharacterRenderer.transform.localScale = new Vector3(1, 1, 1);
                    break;
                case Facing.Right:
                    this.CharacterRenderer.sprite = this.CharacterLeft;
                    this.CharacterRenderer.transform.localScale = new Vector3(-1, 1, 1);
                    this.WantedRelativePos = new Vector3(.9f, 1, 0);
                    break;
                case Facing.Left:
                    this.CharacterRenderer.sprite = this.CharacterLeft;
                    this.CharacterRenderer.transform.localScale = new Vector3(1, 1, 1);
                    this.WantedRelativePos = new Vector3(-.9f, 1, 0);

                    break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject otherObject = other.gameObject;
        Tackable otherTackable = otherObject.GetComponent<Tackable>();
        if (otherTackable != null && !this.availableTackable.Contains(otherObject))
        {
            this.LastTackableMet = otherObject;
            this.availableTackable.Add(otherObject);
        }

        Baby baby = otherObject.GetComponent<Baby>();
        if (baby != null && !this.availableBaby.Contains(baby))
        {
            this.availableBaby.Add(baby);
            this.LastBabyMet = baby;
        }

        CameraTarget camTarget = otherObject.GetComponent<CameraTarget>();
        if (camTarget != null)
        {
            this.Camera.SetTarget(otherObject.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        GameObject otherObject = other.gameObject;
        Tackable otherTackable = otherObject.GetComponent<Tackable>();
        if (otherTackable != null)
        {
            if (this.LastTackableMet == otherObject)
            {
                this.LastTackableMet = null;
            }

            if (this.availableTackable.Contains(otherObject))
            {
                this.availableTackable.Remove(otherObject);
            }

            if (this.LastTackableMet == null && this.availableTackable.Count > 0)
            {
                this.LastTackableMet = this.availableTackable[0];
            }
        }

        Baby baby = otherObject.GetComponent<Baby>();
        if(baby != null)
        {
            if (this.LastBabyMet == baby)
            {
                this.LastBabyMet = null;
            }

            if (this.availableBaby.Contains(baby))
            {
                this.availableBaby.Remove(baby);
            }

            if (this.availableBaby.Count > 0)
            {
                this.LastBabyMet = this.availableBaby[0];
            }
        }
    }

    public void CallToActionTackable()
    {
        if (this.CurrentHeld == null)
        {
            if (this.LastTackableMet != null)
            {
                this.CurrentHeld = this.LastTackableMet;
                this.LastTackableMet.transform.SetParent(this.transform, true);
                this.wantedVelocity = Vector3.zero;
            }
        }
        else
        {
            this.CurrentHeld.transform.SetParent(this.WorldTransform, true);
            this.CurrentHeld = null;
            this.wantedVelocity = Vector3.zero;
        }
    }

    public void CallToActionPat()
    {
        if (this.LastBabyMet == null)
        {
            return;
        }

        if (this.patCoolDown > 0.0f)
        {
            return;
        }

        int numberOfbaby = this.availableBaby.Count;
        for (int index = 0; index < numberOfbaby; ++index)
        {
            if (this.availableBaby[index].PatPat())
            {
                break;
            }
        }

        this.patCoolDown = this.PatTimer;
    }

    public void CallToActionHug()
    {
        if (this.LastBabyMet == null)
        {
            return;
        }

        int numberOfbaby = this.availableBaby.Count;
        for (int index = 0; index < numberOfbaby; ++index)
        {
            if (this.availableBaby[index].Hug())
            {
                break;
            }
        }
    }
}
