using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tackable : MonoBehaviour
{
    public interface TackeWatcher
    {
        void OnTacken(Tackable tackable, object tacker);
        void OnDropped(Tackable tackable, object dropper);
    }

    [SerializeField]
    private GameObject[] watchers = new GameObject[0];

    public bool IsHeld
    {
        private set;
        get;
    }

    public void NotifyTaken(object tacker)
    {
        this.IsHeld = true;
        for (int index = 0; index < watchers.Length; ++index)
        {
            this.watchers[index].GetComponent<TackeWatcher>().OnTacken(this, tacker);
        }
    }

    public void NotifyDropped(object dropper)
    {
        this.IsHeld = false;
        for (int index = 0; index < this.watchers.Length; ++index)
        {
            this.watchers[index].GetComponent<TackeWatcher>().OnDropped(this, dropper);
        }
    }
}
