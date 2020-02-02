using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressAToPlay : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButtonDown("PatPat") || Input.GetKeyDown(KeyCode.A))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
        }
    }
}
