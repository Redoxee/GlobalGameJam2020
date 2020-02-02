using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class controlHandlerEnd : MonoBehaviour
{
    public Sprite A;
    public Sprite B;
    public Sprite X;
    public Sprite E;
    public Sprite R;
    public Sprite F;

    public Image CtrlWheel_A;
    public Image CtrlWheel_Ag;
    public Image CtrlWheel_ApH;
    public Image CtrlWheel_B;
    public Image CtrlWheel_X;
    public Image Baby_B_0;
    public Image Baby_B_0pH;
    public Image Baby_B_1;
    public Image Baby_B_1pH;
    public Image Baby_B_2;
    public Image Baby_B_2pH;
    public Image Baby_B_3;
    public Image Baby_B_3pH;
    public Image Baby_B_4;
    public Image Baby_B_4pH;
    public Image Baby_B_5;
    public Image Baby_B_5pH;
    public Image Baby_B_6;
    public Image Baby_B_6pH;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("joystick 1 button 0") || Input.GetKey("joystick 1 button 1") || Input.GetKey("joystick 1 button 3") || Input.GetKey("joystick 1 button 4") || Input.GetKey("joystick 1 button 5") || Input.GetKey("joystick 1 button 6") || Input.GetKey("joystick 1 button 7") || Input.GetKey("joystick 1 button 8") || Input.GetKey("joystick 1 button 9"))
        {
            CtrlWheel_A.GetComponent<Image>().sprite = A;
            CtrlWheel_Ag.GetComponent<Image>().sprite = A;
            CtrlWheel_ApH.GetComponent<Image>().sprite = A;
            CtrlWheel_B.GetComponent<Image>().sprite = B;
            CtrlWheel_X.GetComponent<Image>().sprite = X;
            Baby_B_0.GetComponent<Image>().sprite = B;
            Baby_B_0pH.GetComponent<Image>().sprite = B;
            Baby_B_1.GetComponent<Image>().sprite = B;
            Baby_B_1pH.GetComponent<Image>().sprite = B;
            Baby_B_2.GetComponent<Image>().sprite = B;
            Baby_B_2pH.GetComponent<Image>().sprite = B;
            Baby_B_3.GetComponent<Image>().sprite = B;
            Baby_B_3pH.GetComponent<Image>().sprite = B;
            Baby_B_4.GetComponent<Image>().sprite = B;
            Baby_B_4pH.GetComponent<Image>().sprite = B;
            Baby_B_5.GetComponent<Image>().sprite = B;
            Baby_B_5pH.GetComponent<Image>().sprite = B;
            Baby_B_6.GetComponent<Image>().sprite = B;
            Baby_B_6pH.GetComponent<Image>().sprite = B;
}
        else
        {
            CtrlWheel_A.GetComponent<Image>().sprite = E;
            CtrlWheel_Ag.GetComponent<Image>().sprite = E;
            CtrlWheel_ApH.GetComponent<Image>().sprite = E;
            CtrlWheel_B.GetComponent<Image>().sprite = R;
            CtrlWheel_X.GetComponent<Image>().sprite = F;
            Baby_B_0.GetComponent<Image>().sprite = R;
            Baby_B_0pH.GetComponent<Image>().sprite = R;
            Baby_B_1.GetComponent<Image>().sprite = R;
            Baby_B_1pH.GetComponent<Image>().sprite = R;
            Baby_B_2.GetComponent<Image>().sprite = R;
            Baby_B_2pH.GetComponent<Image>().sprite = R;
            Baby_B_3.GetComponent<Image>().sprite = R;
            Baby_B_3pH.GetComponent<Image>().sprite = R;
            Baby_B_4.GetComponent<Image>().sprite = R;
            Baby_B_4pH.GetComponent<Image>().sprite = R;
            Baby_B_5.GetComponent<Image>().sprite = R;
            Baby_B_5pH.GetComponent<Image>().sprite = R;
            Baby_B_6.GetComponent<Image>().sprite = R;
            Baby_B_6pH.GetComponent<Image>().sprite = R;
        }
    }

}
