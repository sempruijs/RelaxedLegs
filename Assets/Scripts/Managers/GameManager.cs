﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public State state;
    public GameObject player;
    public GameObject startEmptyChunks;
    public float time;
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
        Pause
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

        if (state == State.InGame)
        {
            time += Time.deltaTime;    
        }
    }

    public void Menu()
    {
        state = State.Menu;
    }

    public void InGame()
    {
        state = State.InGame;
        AudioManager.Instance.InGameMusic();
        player.GetComponent<Rigidbody2D>().gravityScale = 6;
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

    public void Pause()
    {
        state = State.Pause;
        player.GetComponent<Rigidbody2D>().gravityScale = 0;
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
        RemoveAllGameObjectsWithTag("Spike");
        player.transform.position = new Vector3(0f, -1.5f, 0f);
        player.SetActive(true);
        Instantiate(startEmptyChunks, new Vector3(0, 0, 0), Quaternion.identity);
        time = 0f;
        InGame();
    }
}
