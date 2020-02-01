using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Transform WorldTransform;

    private List<GameObject> availableTackable = new List<GameObject>();
    private GameObject LastTackableMet = null;
    private GameObject CurrentHeld = null;
    private Vector3 WantedRelativePos = new Vector3(0, 1,0);
    private float deadLift = .005f;
    private Vector3 wantedVelocity = Vector3.zero;

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
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        GameObject otherObject = other.gameObject;
        Tackable otherTackable = otherObject.GetComponent<Tackable>();
        if (otherTackable == null)
        {
            return;
        }

        if (this.LastTackableMet == otherTackable)
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
}
