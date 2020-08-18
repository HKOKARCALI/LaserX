using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlowManager : MonoBehaviour
{
    [Header("Parameters")]
    [Tooltip("Duration of the fade-to-black at the end of the game")]
    public float endSceneLoadDelay = 3f;
    [Tooltip("The canvas group of the fade-to-black screen")]
    public CanvasGroup endGameFadeCanvasGroup;

    [Header("Win")]
    [Tooltip("This string has to be the name of the scene you want to load when winning")]
    public string winSceneName = "WinScene";
    [Tooltip("Duration of delay before the fade-to-black, if winning")]
    public float delayBeforeFadeToBlack = 4f;
    [Tooltip("Duration of delay before the win message")]
    public float delayBeforeWinMessage = 2f;
    [Tooltip("Sound played on win")]
    public AudioClip victorySound;
    [Tooltip("Prefab for the win game message")]
    public GameObject WinGameMessagePrefab;

    [Header("Lose")]
    [Tooltip("This string has to be the name of the scene you want to load when losing")]
    public string loseSceneName = "LoseScene";


    public bool gameIsEnding { get; private set; }

    PlayerCharacterController m_Player;
    NotificationHUDManager m_NotificationHUDManager;
    ObjectiveManager m_ObjectiveManager;
    float m_TimeLoadEndGameScene;
    string m_SceneToLoad;

    public GameObject EasyMode;
    public GameObject MediumMode;
    public GameObject HardMode;

    public WeaponController Weapon1;
    public WeaponController Weapon2;
    public WeaponController Weapon3;

    public float startTime;

    private void Awake()
    {
        PlayerPrefs.DeleteKey("Health");
        PlayerPrefs.DeleteKey("StartTime");
        PlayerPrefs.DeleteKey("Score");
        PlayerPrefs.DeleteKey("Distance");
        PlayerPrefs.DeleteKey("Health");
        PlayerPrefs.DeleteKey("Die");
    }

    void Start()
    {
        m_Player = FindObjectOfType<PlayerCharacterController>();
        DebugUtility.HandleErrorIfNullFindObject<PlayerCharacterController, GameFlowManager>(m_Player, this);

        m_ObjectiveManager = FindObjectOfType<ObjectiveManager>();
		DebugUtility.HandleErrorIfNullFindObject<ObjectiveManager, GameFlowManager>(m_ObjectiveManager, this);

        AudioUtility.SetMasterVolume(1);


        


        startTime = Time.time;
        
        PlayerPrefs.SetFloat("StartTime", startTime);

        //PlayerPREFS Selectin Logic

        if (PlayerPrefs.GetInt("GameMode") == 0)
        {
            
            for (int i = 0; i < EasyMode.transform.childCount; i++)
            {
                EasyMode.transform.GetChild(i).gameObject.SetActive(true);
            }


        }
         if (PlayerPrefs.GetInt("GameMode") == 1)
        {
           

            for (int i = 0; i < MediumMode.transform.childCount; i++)
            {
                MediumMode.transform.GetChild(i).gameObject.SetActive(true);
            }


        }
         if(PlayerPrefs.GetInt("GameMode") == 2)
        {
          
            for (int i = 0; i < HardMode.transform.childCount; i++)
            {
                HardMode.transform.GetChild(i).gameObject.SetActive(true);
            }


        }

        
         if(PlayerPrefs.GetInt("Weapon") == 0)
        {

            try
            {
                FindObjectOfType<PlayerWeaponsManager>().startingWeapons[0] = Weapon1;
                FindObjectOfType<PlayerWeaponsManager>().AddWeapon(Weapon1);
                FindObjectOfType<PlayerWeaponsManager>().SwitchWeapon(true);
                FindObjectOfType<PlayerWeaponsManager>().m_WeaponSwitchState = PlayerWeaponsManager.WeaponSwitchState.PutDownPrevious;
            }
            catch (System.Exception)
            {

                
            }

            



        }

        if (PlayerPrefs.GetInt("Weapon") == 1)
        {

            try
            {
                FindObjectOfType<PlayerWeaponsManager>().startingWeapons[0] = Weapon2;
                FindObjectOfType<PlayerWeaponsManager>().AddWeapon(Weapon2);
                FindObjectOfType<PlayerWeaponsManager>().GetActiveWeapon();
                FindObjectOfType<PlayerWeaponsManager>().m_WeaponSwitchState = PlayerWeaponsManager.WeaponSwitchState.PutDownPrevious;
            }
            catch (System.Exception)
            {


            }

           
        }

        if (PlayerPrefs.GetInt("Weapon") == 2)
        {

            try
            {
                FindObjectOfType<PlayerWeaponsManager>().startingWeapons[0] = Weapon3;
                FindObjectOfType<PlayerWeaponsManager>().AddWeapon(Weapon3);
                FindObjectOfType<PlayerWeaponsManager>().GetActiveWeapon();
                FindObjectOfType<PlayerWeaponsManager>().m_WeaponSwitchState = PlayerWeaponsManager.WeaponSwitchState.PutDownPrevious;
            }
            catch (System.Exception)
            {


            }

            
        }






    }

    void Update()
    {
        if (gameIsEnding)
        {
            float timeRatio = 1 - (m_TimeLoadEndGameScene - Time.time) / endSceneLoadDelay;
            endGameFadeCanvasGroup.alpha = timeRatio;

            AudioUtility.SetMasterVolume(1 - timeRatio);

            // See if it's time to load the end scene (after the delay)
            if (Time.time >= m_TimeLoadEndGameScene)
            {
                SceneManager.LoadScene(m_SceneToLoad);
                gameIsEnding = false;
            }
        }
        else
        {
            if (m_ObjectiveManager.AreAllObjectivesCompleted())
                EndGame(true);

            // Test if player died
            if (m_Player.isDead)
                EndGame(false);
        }
    }

    void EndGame(bool win)
    {
        // unlocks the cursor before leaving the scene, to be able to click buttons
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Remember that we need to load the appropriate end scene after a delay
        gameIsEnding = true;
        endGameFadeCanvasGroup.gameObject.SetActive(true);
        if (win)
        {
            m_SceneToLoad = winSceneName;
            m_TimeLoadEndGameScene = Time.time + endSceneLoadDelay + delayBeforeFadeToBlack;

            // play a sound on win
            var audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = victorySound;
            audioSource.playOnAwake = false;
            audioSource.outputAudioMixerGroup = AudioUtility.GetAudioGroup(AudioUtility.AudioGroups.HUDVictory);
            audioSource.PlayScheduled(AudioSettings.dspTime + delayBeforeWinMessage);

            // create a game message
            var message = Instantiate(WinGameMessagePrefab).GetComponent<DisplayMessage>();
            if (message)
            {
                message.delayBeforeShowing = delayBeforeWinMessage;
                message.GetComponent<Transform>().SetAsLastSibling();
            }
        }
        else
        {
            m_SceneToLoad = loseSceneName;
            m_TimeLoadEndGameScene = Time.time + endSceneLoadDelay;
        }
    }
}
