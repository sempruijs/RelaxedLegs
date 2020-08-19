using System;
using UnityEngine;
using UnityEngine.UI;

public class DisplayManager : MonoBehaviour
{
    public GameObject menu;
    public GameObject menuTv;
    public GameObject inGame;
    public GameObject playAgain;
    public GameObject credit;
    public GameObject settings;
    public GameObject pause;
    public GameObject specialThanks;
    public GameObject tutorial;

    [Space(10)]
    public Text coinsPickedUpText;
    public Text coinsPickedUpTextPlayAgain;
    public Text tapToPlayText;

    [Space(10)] 
    public Toggle musicToggle;
    public Toggle sfxToggle;

    [Space(10)] 
    public SelectButtonScript selectButtonScript;

    [Space(10)] 
    public GameObject[] inGameButtonsAppleTv;
    
    [Header("Buttons")]
    public Button playButtonTv;
    public Button playButton;
    [Space(10)] 
    public Button pauseButton;
    public Button creditButton;
    public Button specialThanksButton;
    public Button settingsButton;
    public Button playAgainButton;
    public Button tutorialButton;
    
    private static DisplayManager _instance;
    public static DisplayManager Instance {
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

    private void Start()
    {
        SetPlayText();
        SelectMenu();
    }

    void Update()
    {
        if (GameManager.Instance.state == GameManager.State.InGame)
        {
            coinsPickedUpText.text = GameManager.Instance.coinsCollected.ToString("F0");
        } else if (GameManager.Instance.state == GameManager.State.PlayAgain)
        {
            coinsPickedUpTextPlayAgain.text = GameManager.Instance.coinsCollected.ToString("F0");
        }
    }

    public void UpdateMenu()
    {
        playAgain.SetActive(GameManager.Instance.state == GameManager.State.PlayAgain);
        credit.SetActive(GameManager.Instance.state == GameManager.State.Credit);
        settings.SetActive(GameManager.Instance.state == GameManager.State.Settings);
        pause.SetActive(GameManager.Instance.state == GameManager.State.Pause);
#if UNITY_TVOS
        if (GameManager.Instance.state == GameManager.State.InGame)
        {
            foreach (var button in inGameButtonsAppleTv)
            {
                button.SetActive(false);
            }
        }
        menuTv.SetActive(GameManager.Instance.state == GameManager.State.Menu);
#else
    menu.SetActive(GameManager.Instance.state == GameManager.State.Menu);
#endif
        inGame.SetActive(GameManager.Instance.state == GameManager.State.InGame);
        specialThanks.SetActive(GameManager.Instance.state == GameManager.State.SpecialThanks);
        tutorial.SetActive(GameManager.Instance.state == GameManager.State.Tutorial);
    }

    public void UpdateSound()
    {
        AudioManager.Instance.music = musicToggle.isOn;
        AudioManager.Instance.sfx = sfxToggle.isOn;
        if (!musicToggle.isOn)
        {
            AudioManager.Instance.Stop();
        }
    }

    private void SetPlayText()
    {
        #if UNITY_IOS 
            tapToPlayText.text = "Tap to play";
        #endif
        #if !UNITY_IOS
            tapToPlayText.text = "Space to playq";        
        #endif
    }

    public void SelectMenu()
    {
#if UNITY_TVOS
        selectButtonScript.SelectButton(playButtonTv);
#else
        selectButtonScript.SelectButton(playButton);
#endif
    }

    public void SelectPause()
    {
        selectButtonScript.SelectButton(pauseButton);
    }

    public void SelectSettings()
    {
        selectButtonScript.SelectButton(settingsButton);
    }

    public void SelectCredit()
    {
        selectButtonScript.SelectButton(creditButton);
    }

    public void SelectSpecialThanks()
    {
        selectButtonScript.SelectButton(specialThanksButton);
    }

    public void SelectPlayAgain()
    {
        selectButtonScript.SelectButton(playAgainButton);
    }

    public void SelectTutorial()
    {
        selectButtonScript.SelectButton(tutorialButton);
    }
}
