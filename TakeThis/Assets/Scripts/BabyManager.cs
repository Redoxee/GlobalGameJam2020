using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BabyManager : MonoBehaviour
{
    private static BabyManager instance = null;

    public float GrabRadius = 2;
    public float TimeToFullySad = 100.0f;
    public float HugTimer = 25.0f;
    public Sprite[] Faces;

    public int PatBonus = 5;
    public int HugBonus = 15;
    public int CarrotBonus = 40;
    
    public float TimeToEnd = 15;
    private float timerEnd;
    private bool isCountingDown = false;

    private List<Baby> babies = new List<Baby>();

    [SerializeField]
    public TextMeshProUGUI NearEndText = null;

    public float TimeToSuperCry = 7;
    public float SuperCryRadius = 3;

    public static BabyManager Instance
    {
        get
        {
            return BabyManager.instance;
        }
    }

    public void Register(Baby baby)
    {
        if (!this.babies.Contains(baby))
        {
            this.babies.Add(baby);
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

    private void Update()
    {
        if (this.babies.Count <= 0)
        {
            return;
        }

        int mood = this.babies[0].MoodLevel;
        bool allTheSame = true;
        for (int index = 0; index < this.babies.Count; ++index)
        {
            if (this.babies[index].MoodLevel != mood)
            {
                allTheSame = false;
                break;
            }
        }
        if (mood != 0 && mood != this.Faces.Length - 1)
        {
            allTheSame = false;
        }

        if (allTheSame && !this.isCountingDown)
        {
            this.isCountingDown = true;
            this.timerEnd = this.TimeToEnd;
            Debug.Log("StartingCountDown");
        }
        else if (allTheSame)
        {
            this.NearEndText.gameObject.SetActive(true);
            if (mood == 0)
            {
                this.NearEndText.color = Color.red;

                this.NearEndText.text = $"Everybody is crying.\nAct FAST !\n{this.timerEnd:00}";
            }
            else
            {
                this.NearEndText.color = Color.white;
                this.NearEndText.text = $"Everybody is HAPPY.\nKeep it that way !\n{this.timerEnd:00}";
            }

            this.timerEnd -= Time.deltaTime;

            if (timerEnd <= 0)
            {
                if (mood == 0)
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene("LooseScene");
                }
                else
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene("WinScene");
                }
            }
        }

        if (!allTheSame)
        {
            this.isCountingDown = false;
            this.NearEndText.gameObject.SetActive(false);
        }
    }
}
