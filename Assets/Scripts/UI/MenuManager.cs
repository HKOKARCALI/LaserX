using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using UnityEngine.Events;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    GameObject spawnCharactersGO, spawnWeaponsGO, spawnItemsGO, rotateRootItems,rotateRootCH, rotateRootWP, startPanel, infoPanel, 
    headerInfoText, characterInfoText, weaponInfoText,nextBTN,prevBTN,modSelectPanel;

    public GameObject logo;
    public Button newGameBTN;
    public Button resumeGameBTN;
    public Button selectBTN;
    public Button playBTN;
    public Button backBTN;
    public Image BG;
    public Image Header;
    private bool characterSelectBO=false;
    private bool weaponSelectBO=false;
    private bool itemSelectBO=false;
    private int currentScene = 0;
    public GameObject soldierLOGO;
    public GameObject selectedPanel;

    public int currentObject;



    public Dictionary<string, LaserWeapon> laserWeapons = new Dictionary<string, LaserWeapon>();
    private  int totalScenes=3;

    void Start()
    {
        newGameBTN.onClick.AddListener(newGameFCN);
        resumeGameBTN.onClick.AddListener(resumeGameFCN);

        if (PlayerPrefs.GetInt("GamePlay") > 0)
        {
            resumeGameBTN.interactable = true;
        }

        PlayerPrefs.DeleteKey("Health");
        PlayerPrefs.DeleteKey("StartTime");
        PlayerPrefs.DeleteKey("Score");
        PlayerPrefs.DeleteKey("Distance");
        PlayerPrefs.DeleteKey("Health");
        PlayerPrefs.DeleteKey("Die");


        modSelectPanel.transform.GetChild(0).transform.DOScale(0, 0.1f);
        modSelectPanel.transform.GetChild(1).transform.DOScale(0, 0.1f);
        modSelectPanel.transform.GetChild(2).transform.DOScale(0, 0.1f);

        modSelectPanel.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => { gameModeSelect(0); });
        modSelectPanel.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => { gameModeSelect(1); });
        modSelectPanel.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => { gameModeSelect(2); });
        playBTN.interactable = false;

        //init weapons Data for info panel
        Debug.Log("cascascascascasc");
        LaserWeapon wp1 = new LaserWeapon("Weapon1", 10, 10, 5000, 3);
        LaserWeapon wp2 = new LaserWeapon("Weapon2", 15, 15, 10000, 4);
        LaserWeapon wp3 = new LaserWeapon("Weapon3", 20, 20, 15000, 5);


        laserWeapons.Add("0",wp1);
        laserWeapons.Add("1", wp2);
        laserWeapons.Add("2", wp3);
        addListeners();
        

    }

    private void changeScene(int v)
    {
        switch (v)
        {
            case 0:
                selectScreen0();
                break;
            case 1:
                selectScreen1();
                break;
            case 2:
                selectScreen2();
                break;
          


        }
    }

   

    void updateSelectedPanel()
    {

        switch (currentScene)
        {
            case 0:


                for (int i = 0; i < spawnCharactersGO.transform.childCount; i++)
                {
                    if (spawnCharactersGO.transform.GetChild(i).gameObject.activeInHierarchy)
                    {
                        selectedPanel.transform.GetChild(0).GetChild(i+1).gameObject.SetActive(true);
                        
                        
                    }
                }

          

                break;
            case 1:
                for (int i = 0; i < spawnWeaponsGO.transform.childCount; i++)
                {
                    if (spawnWeaponsGO.transform.GetChild(i).gameObject.activeInHierarchy)
                    {
                        selectedPanel.transform.GetChild(1).GetChild(i+1).gameObject.SetActive(true);
                        PlayerPrefs.SetInt("Weapon", i);
                    }
                }
                break;
            case 2:

                for (int i = 0; i < spawnItemsGO.transform.childCount; i++)
                {
                    if (spawnItemsGO.transform.GetChild(i).gameObject.activeInHierarchy)
                    {
                        selectedPanel.transform.GetChild(2).GetChild(i+1).gameObject.SetActive(true);
                    }
                }

                break;

            default:
                break;
        }

    }




    private void loadGame()
    {
        PlayerPrefs.SetInt("GamePlay", 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

 

    private static void updateInfoPanelBar()
    {
        switch (FindObjectOfType<CharacterSelector>().currentChar)
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

    private void selectScreen0()
    {
        soldierLOGO.SetActive(false);
        Header.DOFade(1, 0.5f);
        spawnCharactersGO.SetActive(true);
        selectedPanel.SetActive(true);
        rotateRootCH.SetActive(true);
        selectBTN.gameObject.SetActive(true);
        playBTN.gameObject.SetActive(true);
        backBTN.gameObject.SetActive(true);
        backBTN.interactable=false;
        rotateRootWP.SetActive(false);
        spawnWeaponsGO.SetActive(false);
        infoPanel.SetActive(false);
        headerInfoText.GetComponent<TextMeshProUGUI>().text = "Select Characters";
        nextBTN.SetActive(true);
        prevBTN.SetActive(true);
        currentScene = 0;
        clearSelectPanel();

        
    }

    private void selectScreen2()
    {

        rotateRootItems.SetActive(true);
        spawnItemsGO.SetActive(true);
        spawnCharactersGO.SetActive(false);
        rotateRootCH.SetActive(false);
        rotateRootWP.SetActive(false);
        spawnWeaponsGO.SetActive(false);
        headerInfoText.GetComponent<TextMeshProUGUI>().text = "Select Item";

        selectBTN.gameObject.SetActive(false);
        playBTN.interactable = true;
        playBTN.onClick.AddListener(() => { loadGame(); });
        currentScene = 2;
        

    }


    private void selectScreen1()
    {

        rotateRootItems.SetActive(false);
        spawnItemsGO.SetActive(false);
        spawnCharactersGO.SetActive(false);
        rotateRootCH.SetActive(false);
        rotateRootWP.SetActive(true);
        spawnWeaponsGO.SetActive(true);
        headerInfoText.GetComponent<TextMeshProUGUI>().text = "Select Weapons";
        infoPanel.SetActive(true);
        playBTN.interactable = false;
        selectBTN.gameObject.SetActive(true);
        backBTN.interactable = true;
        updateInfoPanelBar();

        currentScene = 1;
        
        
    }

    void clearSelectPanel()
    {
        selectedPanel.transform.GetChild(currentScene).GetChild(0).gameObject.SetActive(true);
        selectedPanel.transform.GetChild(currentScene).GetChild(1).gameObject.SetActive(false);
        selectedPanel.transform.GetChild(currentScene).GetChild(2).gameObject.SetActive(false);
        selectedPanel.transform.GetChild(currentScene).GetChild(3).gameObject.SetActive(false);
    }



    private void addListeners()
    {

        selectBTN.onClick.AddListener(() =>
        {
            
            if (currentScene+1 < totalScenes)
            {
                updateSelectedPanel();
                changeScene(currentScene+1);
               
               
                

            }
            
        });

        backBTN.onClick.AddListener(() =>
        {
            if (currentScene > 0)
            {
                changeScene(currentScene-1);
            }
            
        });


    }



    private void gameModeSelect(int v)
    {
        modSelectPanel.transform.GetChild(0).transform.DOScale(0, 0.1f);
        modSelectPanel.transform.GetChild(1).transform.DOScale(0, 0.1f);
        modSelectPanel.transform.GetChild(2).transform.DOScale(0, 0.1f);
        BG.DOFade(0, 0.5f);
        selectScreen0();

        switch (v)
        {
            case 0:
                PlayerPrefs.SetInt("GameMode", 0);
                break;
           case 1:
                PlayerPrefs.SetInt("GameMode", 1);
                break;
            case 2:
                PlayerPrefs.SetInt("GameMode", 2);
                break;
            default:
                break;
        }

    }

    void newGameFCN()
    {
        modSelectPanel.SetActive(true);
        //image.DOFade(1, 0.5f)
        modSelectPanel.transform.GetChild(0).transform.DOScale(1.6f, 0.5f);
        modSelectPanel.transform.GetChild(1).transform.DOScale(1.6f, 0.5f).SetDelay(0.1f); 
        modSelectPanel.transform.GetChild(2).transform.DOScale(1.6f, 0.5f).SetDelay(0.1f); 
        logoMenuDisappear();
    }

    void logoMenuDisappear()
    {
        logo.SetActive(false);
        newGameBTN.gameObject.SetActive(false);
        resumeGameBTN.gameObject.SetActive(false);
    }



    void resumeGameFCN()
    {
        loadGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
