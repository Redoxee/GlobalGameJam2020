using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public Text text;
    public bool isBlinking = true;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("BlinkingText");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SceneManager.LoadScene(1);
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
