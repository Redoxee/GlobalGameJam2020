using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float Smoothness = 1.0f;

    [SerializeField]
    private Transform CurrentTarget = null;
    private Vector2 Target;

    private Vector2 velocity = Vector2.zero;

    private void Start()
    {
        if (this.CurrentTarget != null)
        {
            Vector2 p = this.CurrentTarget.position;
            this.Target = new Vector2(p.x, p.y);
        }
    }

    void LateUpdate()
    {
        if (this.CurrentTarget == null)
        {
            return;
        }

        Vector3 pos = transform.position;
        Vector2 current = new Vector2(pos.x, pos.y);
        Vector2 next = Vector2.SmoothDamp(current, this.Target, ref this.velocity, this.Smoothness,200f,Time.deltaTime);
        this.transform.position = new Vector3(next.x, next.y, pos.z);
    }

    public void SetTarget(Transform nextTarget)
    {
        this.CurrentTarget = nextTarget;

        if (this.CurrentTarget != null)
        {
            Vector2 p = this.CurrentTarget.position;
            this.Target = new Vector2(p.x, p.y);
        }
    }
}
