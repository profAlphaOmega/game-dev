using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
    {   
        public string _scene;
        public LevelManager(){}

        void Start() 
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnLoaded;
        }

        public void LoadScene(string _scene)
        {
            // SceneManager.LoadScene(_scene);
        }

        public void LoadSceneFromFile()
        {
            // SceneManager.LoadScene(SETTINGS.currentScene);
        }
        
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            
        }
        void OnSceneUnLoaded(Scene scene)
        {

        }
    }
