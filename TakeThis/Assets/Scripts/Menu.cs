using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public Text text;
    public bool isBlinking = true;

    void Start()
    {
        StartCoroutine("BlinkingText");
    }

    void Update()
    {
        if (Input.GetButtonDown("PatPat") || Input.GetKeyDown(KeyCode.A))
        {
            SceneManager.LoadScene("GameScene");
        }
    }

    public IEnumerator BlinkingText()
    {
        while (isBlinking)
        {
            text.enabled = true;
            yield return new WaitForSeconds(1);
            text.enabled = false;
            yield return new WaitForSeconds(.5f);
        }
    }
}
