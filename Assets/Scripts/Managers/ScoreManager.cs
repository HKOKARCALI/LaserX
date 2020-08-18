using UnityEngine;
using System.Collections;
using TMPro;
public class ScoreManager : MonoBehaviour
{

    
    public int Score;
    public float BestDistance;
    private float lastestDistance;
    private float timeTemp = 0;
    private bool showdistance = false;
    private int scorePlus = 0;
    private string hitType;
    public GameObject scoreText;
    public GameObject bestText;
  
    void Start()
    {
        Score = 0;
        BestDistance = 0;
       

    }

    void Update()
    {
        if (showdistance)
        {
            if (Time.time >= timeTemp + 3)
            {
                showdistance = false;
            }
        }
    }

    public void AddScore(int score, float distance, int suffix)
    {
        int bonus = 1;

        if (distance > 500)
            bonus = 2;

        if (distance > 1000)
            bonus = 5;

        if (distance > 1500)
            bonus = 10;

        switch (suffix)
        {
            case 0:
                hitType = "Body shot";
                break;
            case 1:
                hitType = "Head shot";
                break;
            case 2:
                hitType = "Arm shot";
                break;
            case 3:
                hitType = "Leg shot";
                break;
            case 4:
                hitType = "Ass shot";
                break;
        }

        scorePlus = score * (((int)(distance * 0.1) + 1)) * bonus;
        Score += scorePlus;
        lastestDistance = distance;
        if (distance > BestDistance)
        {
            BestDistance = distance;
        }

        showdistance = true;
        timeTemp = Time.time;
        scoreGuiUpdate();
    }

    void scoreGuiUpdate()
    {
        if (showdistance)
        {
            scoreText.GetComponent<TextMeshProUGUI>().text = "SCORE " + Score.ToString();
            bestText.GetComponent<TextMeshProUGUI>().text = "BEST " + Mathf.Floor(BestDistance) + " M.";

            PlayerPrefs.SetInt("Score", Score);
            PlayerPrefs.SetFloat("Distance", Mathf.Floor(BestDistance));


        }
        /*if (showdistance)
        {
            scoreText.GetComponent<TextMeshProUGUI>().text = hitType + " " + Mathf.Floor(lastestDistance) + " M.\n+" + scorePlus;
        }*/
  
    }
}
