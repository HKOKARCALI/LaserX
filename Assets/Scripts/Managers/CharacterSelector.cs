using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelector : MonoBehaviour
{
    // Start is called before the first frame update

    // The  marker out of visible scence
    public Transform markerChar;
    public Button prevBTN;
    public Button nextBTN;


    // The characters prefabs to pick
    public Transform[] charsPrefabs;

    // The game objects created to be showed on screen
    private GameObject[] chars;

    // The index of the current character
    public int currentChar = 0;

    

    void Start()
    {
        // We initialize the chars array
        chars = new GameObject[charsPrefabs.Length];

        // We create game objects based on characters prefabs
        int index = 0;
        foreach (Transform t in charsPrefabs)
        {
            chars[index++] = GameObject.Instantiate(t.gameObject, markerChar.position, markerChar.rotation, markerChar) as GameObject;
           
        }

        for (int i = 0; i < charsPrefabs.Length-1; i++)
        {
            markerChar.GetChild(i+1).gameObject.SetActive(false);
        }

        prevBTN.onClick.AddListener(PrevChar);
        nextBTN.onClick.AddListener(NextChar);
   

    }

    

    // Update is called once per frame
    void Update()
    {
      
        
    }

    void updateInfoBar()
    {
        if (FindObjectOfType<InfoPanel>())
        {
            switch (currentChar)
            {
                case 0:
                    FindObjectOfType<InfoPanel>().updateBarvalues(0.1f, 0.3f, 0.5f, 0.3f);
                    break;
                case 1:
                    FindObjectOfType<InfoPanel>().updateBarvalues(0.15f, 0.35f, 0.75f, 0.4f);
                    break;
                case 2:
                    FindObjectOfType<InfoPanel>().updateBarvalues(0.20f, 0.6f, 1, 0.5f);
                    break;

            }
        }
    }

	
     public void NextChar()
            {
                currentChar++;
        

        if (currentChar >= chars.Length)
                {
                    currentChar = chars.Length - 1;
                    
                }
        markerChar.GetChild(currentChar - 1).gameObject.SetActive(false);
        markerChar.GetChild(currentChar).gameObject.SetActive(true);
        updateInfoBar();
        

    }

    public void PrevChar()
        {
            currentChar--;
        

        if (currentChar < 0)
            {
                currentChar = 0;

            }

        markerChar.GetChild(currentChar + 1).gameObject.SetActive(false);
        markerChar.GetChild(currentChar).gameObject.SetActive(true);
        updateInfoBar();
       ;
    }


}
