using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public State state;
    public GameObject player;
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
        Settings
    };

    void Start()
    {
        Menu();
        Application.targetFrameRate = 300;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadCurrentScene();
        }
    }

    public void Menu()
    {
        state = State.Menu;
    }

    public void InGame()
    {
        state = State.InGame;
    }

    public void Credit()
    {
        state = State.Credit;
    }

    public void Settings()
    {
        state = State.Settings;
    }

    public void PlayAgain()
    {
        state = State.PlayAgain;
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
        player.transform.position = new Vector3(0f, -1.5f, 0f);
        player.SetActive(true);
        InGame();
    }
}
