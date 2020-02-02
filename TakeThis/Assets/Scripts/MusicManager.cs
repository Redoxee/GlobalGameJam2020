using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{

    public AudioSource musicMenuSource;
    public AudioSource musicGameSource;
    public AudioSource musicEndSource;

    public float musicTransitionSpeed;

    public string isScene = "game"; // "menu", "game", "end"

    public string isGlobalState = "calm"; // "laugh", "happy", "calm", "sad", "cry"

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

       /*// temp / change music manually
        if (Input.GetKey(KeyCode.Keypad1)) { isScene = "menu"; }
        if (Input.GetKey(KeyCode.Keypad2)) { isScene = "game"; }
        if (Input.GetKey(KeyCode.Keypad3)) { isScene = "end"; }*/

        // change music playing
        switch (isScene)
        {

            case "menu":
                if (musicMenuSource.volume < 0.8) { musicMenuSource.volume += musicTransitionSpeed; }
                if (musicGameSource.volume > 0) { musicGameSource.volume -= musicTransitionSpeed; }
                if (musicEndSource.volume > 0) { musicEndSource.volume -= musicTransitionSpeed; }
                break;

            case "game":
                if (musicMenuSource.volume > 0) { musicMenuSource.volume -= musicTransitionSpeed; }
                if (musicGameSource.volume < 0.65) { musicGameSource.volume += musicTransitionSpeed; }
                if (musicEndSource.volume > 0) { musicEndSource.volume -= musicTransitionSpeed; }
                break;

            case "end":
                if (musicMenuSource.volume > 0) { musicMenuSource.volume -= musicTransitionSpeed; }
                if (musicGameSource.volume > 0) { musicGameSource.volume -= musicTransitionSpeed; }
                if (musicEndSource.volume < 1) { musicEndSource.volume += musicTransitionSpeed; }
                break;

        }

        // temp / change global state
        if (Input.GetKey(KeyCode.Keypad8)) { isGlobalState = "laugh"; }
        if (Input.GetKey(KeyCode.Keypad7)) { isGlobalState = "happy"; }
        if (Input.GetKey(KeyCode.Keypad6)) { isGlobalState = "calm"; }
        if (Input.GetKey(KeyCode.Keypad5)) { isGlobalState = "sad"; }
        if (Input.GetKey(KeyCode.Keypad4)) { isGlobalState = "cry"; }

        // change game music pitch
        switch (isGlobalState)
        {

            case "laugh":
                musicGameSource.pitch = 0.95f;
                break;

            case "happy":
                musicGameSource.pitch = 1;
                break;

            case "calm":
                musicGameSource.pitch = 1.05f;
                break;

            case "sad":
                musicGameSource.pitch = 1.12f;
                break;

            case "cry":
                musicGameSource.pitch = 1.22f;
                break;

        }

    }

}
