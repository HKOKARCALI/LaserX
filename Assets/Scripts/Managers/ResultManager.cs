using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

using System;

public class ResultManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bar1, bar2, bar3, bar4, bar5,bar6,bar7;

    public GameObject txt1, txt2, txt3, txt4, txt5, txt6, txt7;

    public Button mainMenuBTN, restartBTN, exitBTN;
    private string session_timeTPT;
    private int paus = 0;
    private int elapsed;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    void Start()
    {

        //todo stil bug datas
        //buray yetıstıremedım sadece tasarımda kaldı bazı datalar tam gelmıyor
        updateBarvalues(1, 0.5f, 0.6f, 0.7f, 0.8f, 0.9f, 0.2f);
        mainMenuBTN.onClick.AddListener(backToMainMenu);
        restartBTN.onClick.AddListener(restartGame);
        exitBTN.onClick.AddListener(exitGame);
        Cursor.lockState = CursorLockMode.None;

       //session_timeTPT = timeFormat((elapsedTime) / 1000);
       // Debug.Log(session_timeTPT);
          
     
    }

    private string timeFormat(object p)
    {
        throw new NotImplementedException();
    }

    public static string timeFormat(double timeValue)
    {
        return format(Convert.ToInt32((timeValue / 3600) % 60), 2) + ":" + format(Convert.ToInt32((timeValue / 60) % 60), 2) + ":" + format(Convert.ToInt32((timeValue) % 60), 2);
    }

    public static string format(int num, int frmt)
    {
        int len = Convert.ToString(num).Length;
        string txt = "";

        for (var tempj = 0; tempj < frmt - len; tempj++)
        {
            txt += "0";
        }
        txt += num;
        return txt;
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    private void initbar()
    {
        bar1.transform.GetChild(2).transform.GetComponent<Slider>().value = 0f;
        bar2.transform.GetChild(2).transform.GetComponent<Slider>().value = 0f;
        bar3.transform.GetChild(2).transform.GetComponent<Slider>().value = 0f;
        bar4.transform.GetChild(2).transform.GetComponent<Slider>().value = 0f;
        bar5.transform.GetChild(2).transform.GetComponent<Slider>().value = 0f;
        bar6.transform.GetChild(2).transform.GetComponent<Slider>().value = 0f;
        bar7.transform.GetChild(2).transform.GetComponent<Slider>().value = 0f;


    }

    public void updateBarvalues(float data1, float data2, float data3, float data4, float data5, float data6, float data7)
    {

   

        bar1.transform.GetChild(2).transform.GetComponent<Slider>().DOValue(data1, 1);
        bar2.transform.GetChild(2).transform.GetComponent<Slider>().DOValue(data2, 1).SetDelay(0.1f);
        bar3.transform.GetChild(2).transform.GetComponent<Slider>().DOValue(data3, 1).SetDelay(0.2f);
        bar4.transform.GetChild(2).transform.GetComponent<Slider>().DOValue(data4, 1).SetDelay(0.3f);
        bar5.transform.GetChild(2).transform.GetComponent<Slider>().DOValue(data5, 1).SetDelay(1);
        bar6.transform.GetChild(2).transform.GetComponent<Slider>().DOValue(data6, 1).SetDelay(1.1f);
        bar7.transform.GetChild(2).transform.GetComponent<Slider>().DOValue(data7, 1).SetDelay(2f);


        var startTime = PlayerPrefs.GetInt("StartTime");
        var elapsedTime = Time.time - startTime;

        var score = PlayerPrefs.GetInt("Score");
        var distance = PlayerPrefs.GetFloat("Distance");
        var health = PlayerPrefs.GetFloat("Health");
        var hit = PlayerPrefs.GetFloat("Hit");
        var die = PlayerPrefs.GetFloat("Die");


        txt1.GetComponent<TextMeshProUGUI>().text = "Health"+": "+ health.ToString() ;
        txt2.GetComponent<TextMeshProUGUI>().text = "Score" + ": " + score.ToString();
        txt3.GetComponent<TextMeshProUGUI>().text = "Hit" + ": " + hit.ToString();
        txt4.GetComponent<TextMeshProUGUI>().text = "kill" + ": " + die.ToString();
        txt5.GetComponent<TextMeshProUGUI>().text = "total damage" + ": " + (score*2).ToString();
        txt6.GetComponent<TextMeshProUGUI>().text = "distance" + ": " + distance.ToString();
        txt7.GetComponent<TextMeshProUGUI>().text = "time" + ": " + elapsedTime.ToString()+" "+"Seconds";


    }

    void restartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    void exitGame()
    {
        Application.Quit();
    }

    void backToMainMenu()
    {
        SceneManager.LoadScene("StartScene");
    }
}
