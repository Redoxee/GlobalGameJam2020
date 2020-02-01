using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyManager : MonoBehaviour
{
    private static BabyManager instance = null;

    public float TimeToFullySad = 60.0f;
    public float PatTimer = 1.0f;
    public float HugTimer = 20.0f;
    public Sprite[] Faces;


    public int PatBonus = 1;
    public int HugBonus = 10;

    public static BabyManager Instance
    {
        get
        {
            return BabyManager.instance;
        }
    }

    void Awake()
    {
        if (BabyManager.instance != null)
        {
            Destroy(this);
            return;
        }

        BabyManager.instance = this;
    }
}
