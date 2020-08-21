using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public State state;
    public GameObject player;
    public GameObject startEmptyChunks;
    public int coinsCollected;
    public ScoreBoardManager scoreBoardManager;
    public SelectButtonScript selectButtonScript;
    
    [Space(10)]
    public Button retry; //Play Again
    public Button resume; //Pause
    public Button tapToPlay; //Menu
    public Button backSettings; //Settings
    public Button backCredit; //Credit
    

    private bool _isPaused = false;
    
    private static GameManager _instance;
         public static GameManager Instance {
         get {
             return _instance;
         }
    }
    private void Awake()
    {
         if (_instance != null && _instance != this)
         {
            Destroy(this.gameObject);
         } else {
            _instance = this;
         } 
    }

    public enum State
    {
        InGame,
        Menu,
        PlayAgain,
        Credit,
        Settings,
        Pause,
        SpecialThanks,
        Tutorial
    };

    void Start()
    {
        Menu();
        Application.targetFrameRate = 300;
        ShowTutorial();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadCurrentScene();
        }

        if (Input.GetKeyDown(KeyCode.Space) && state == State.Menu)
        {
            Reset();
        }
        
        if (Input.GetButtonDown("Menu"))
        {
            if (state == State.InGame)
            {
                if (!_isPaused)
                {
                    Pause();
                    _isPaused = true;
                }
            } else if (state == State.Menu)
            {
#if UNITY_TVOS
                UnityEngine.tvOS.Remote.allowExitToHome = true;
#endif
            } else if (state == State.Credit)
            {
                Settings();
            } else if (state == State.SpecialThanks)
            {
                Credit();
            } else if (state == State.Settings)
            {
                Menu();
            } else if (state == State.Tutorial)
            {
                Menu();
            } else if (state == State.Pause)
            {
                Menu();
            }
        } 
    }

    public void Menu()
    {
        _isPaused = false;
        state = State.Menu;
        AudioManager.Instance.MenuMusic();
        DisplayManager.Instance.UpdateMenu();
        DisplayManager.Instance.SelectMenu();
        Time.timeScale = 1f;
    }

    public void InGame()
    {
            #if UNITY_TVOS
                UnityEngine.tvOS.Remote.allowExitToHome = false;
            #endif
        _isPaused = false;
        state = State.InGame;
        // AudioManager.Instance.Stop();
        player.GetComponent<Rigidbody2D>().gravityScale = 6;
        AudioManager.Instance.InGameMusic();
        // Advertisement.Initialize(_appStoreId, useAds);
        DisplayManager.Instance.UpdateMenu();
        Time.timeScale = 1f;
    }

    public void Credit()
    {
#if UNITY_TVOS
                UnityEngine.tvOS.Remote.allowExitToHome = false;
#endif
        state = State.Credit;
        DisplayManager.Instance.UpdateMenu();
        DisplayManager.Instance.SelectCredit();
    }

    public void Settings()
    {
#if UNITY_TVOS
                UnityEngine.tvOS.Remote.allowExitToHome = false;
#endif
        state = State.Settings;
        DisplayManager.Instance.UpdateMenu();
        DisplayManager.Instance.SelectSettings();
    }

    public void PlayAgain()
    {
        state = State.PlayAgain;
        DisplayManager.Instance.UpdateMenu();
        scoreBoardManager.PostScoreOnLeaderBoard(coinsCollected);
        DisplayManager.Instance.SelectPlayAgain();
    }

    public void Pause()
    {
#if UNITY_TVOS
                UnityEngine.tvOS.Remote.allowExitToHome = false;
#endif
        state = State.Pause;
        player.GetComponent<Rigidbody2D>().gravityScale = 0;
        DisplayManager.Instance.UpdateMenu();
        Time.timeScale = 0f;
        DisplayManager.Instance.SelectPause();
    }

    public void SpecialThanks()
    {
#if UNITY_TVOS
                UnityEngine.tvOS.Remote.allowExitToHome = false;
#endif
        state = State.SpecialThanks;
        DisplayManager.Instance.UpdateMenu();
        DisplayManager.Instance.SelectSpecialThanks();
    }

    public void Tutorial()
    {
#if UNITY_TVOS
                UnityEngine.tvOS.Remote.allowExitToHome = false;
#endif
        state = State.Tutorial;
        DisplayManager.Instance.UpdateMenu();
        DisplayManager.Instance.SelectTutorial();
    }
    
    public void ReloadCurrentScene() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void RemoveAllGameObjectsWithTag(string tag)
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag (tag);

        foreach (var g in gameObjects)
        {
            Destroy(g);
        }
    }

    public void Reset()
    {
        RemoveAllGameObjectsWithTag("Chunk");
        // RemoveAllGameObjectsWithTag("Spike");
        player.transform.position = new Vector3(0f, -1.5f, 0f);
        player.SetActive(true);
        Instantiate(startEmptyChunks, new Vector3(0, 0, 0), Quaternion.identity);
        coinsCollected = 0;
        // AudioManager.Instance.InGameMusic();
        InGame();
    }

    public void ShowTutorial()
    {
        if (PlayerPrefs.GetInt("FirstTime", 0) == 0)
        {
            Tutorial();
            PlayerPrefs.SetInt("FirstTime", 1);
        }
    }
}
