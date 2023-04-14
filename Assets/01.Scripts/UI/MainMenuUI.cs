using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    private void Awake()
    {
        GetButton<Button>("PlayButton").onClick.AddListener(() => { GameSceneManager.Load(GameSceneManager.Scene.GameScene); });
        GetButton<Button>("QuitButton").onClick.AddListener(() => { Application.Quit(); });
    }

    private T GetButton<T>(string name) where T : Object
    {
        return transform.Find(name).GetComponent<T>();
    }
}
