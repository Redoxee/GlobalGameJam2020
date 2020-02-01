using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPadController : MonoBehaviour
{
    public Rigidbody2D PlayerRigidBody = null;
    public float Speed;
    public void LateUpdate()
    {
        float ix = Input.GetAxis("Horizontal");
        float iy = Input.GetAxis("Vertical");

        this.PlayerRigidBody.velocity += new Vector2(ix, iy) * Speed;
    }
}
