using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InfoPanel : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bar1, bar2, bar3, bar4;


    void Start()
    {
        initbar();
       
    }

    private void initbar()
    {
        bar1.transform.GetChild(2).transform.GetComponent<Slider>().value = 0f;
        bar2.transform.GetChild(2).transform.GetComponent<Slider>().value = 0f;
        bar3.transform.GetChild(2).transform.GetComponent<Slider>().value = 0f;
        bar4.transform.GetChild(2).transform.GetComponent<Slider>().value = 0f;
    }

    public void updateBarvalues(float data1, float data2, float data3, float data4)
    {
        bar1.transform.GetChild(2).transform.GetComponent<Slider>().DOValue(data1, 1);
        bar2.transform.GetChild(2).transform.GetComponent<Slider>().DOValue(data2, 1);
        bar3.transform.GetChild(2).transform.GetComponent<Slider>().DOValue(data3, 1);
        bar4.transform.GetChild(2).transform.GetComponent<Slider>().DOValue(data4, 1);

    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
