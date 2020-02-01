using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPadController : MonoBehaviour
{
    public Character chatacter = null;

    public Rigidbody2D PlayerRigidBody = null;
    public float Speed;
    public void LateUpdate()
    {
        float ix = Input.GetAxis("Horizontal");
        float iy = Input.GetAxis("Vertical");

        this.PlayerRigidBody.velocity += new Vector2(ix, iy) * Speed;

        if (Input.GetButtonDown("Take"))
        {
            this.chatacter.CallToActionTackable();
        }

        if (Input.GetButtonDown("PatPat"))
        {
            this.chatacter.CallToActionPat();
        }

        if (Input.GetButtonDown("Hug"))
        {
            this.chatacter.CallToActionHug();
        }
    }
}
