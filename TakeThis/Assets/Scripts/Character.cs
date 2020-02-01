using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Transform WorldTransform;

    private List<GameObject> availableTackable = new List<GameObject>();
    private GameObject LastTackableMet = null;
    private GameObject CurrentHeld = null;

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
            }
        }
        else
        {
            this.CurrentHeld.transform.SetParent(this.WorldTransform, true);
            this.CurrentHeld = null;
        }
    }
}
