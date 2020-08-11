using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayManager : MonoBehaviour
{
    public GameObject menu;
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

    [Space(10)] 
    public Toggle musicToggle;
    public Toggle sfxToggle;
    
    
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
        menu.SetActive(GameManager.Instance.state == GameManager.State.Menu);
        inGame.SetActive(GameManager.Instance.state == GameManager.State.InGame);
        playAgain.SetActive(GameManager.Instance.state == GameManager.State.PlayAgain);
        credit.SetActive(GameManager.Instance.state == GameManager.State.Credit);
        settings.SetActive(GameManager.Instance.state == GameManager.State.Settings);
        pause.SetActive(GameManager.Instance.state == GameManager.State.Pause);
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
}
