using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D PlayerRigidBody = null;

    public float Speed = 2.0f;

    private void LateUpdate()
    {
        Vector2 direction = Vector2.zero;
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            direction.x -= 1;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            direction.x += 1;
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            direction.y -= 1;
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            direction.y += 1;
        }

        if (direction.sqrMagnitude > 0)
        {
            this.PlayerRigidBody.velocity += direction * this.Speed;
        }
    }
}
