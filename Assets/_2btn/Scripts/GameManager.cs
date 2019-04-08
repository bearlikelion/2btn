using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private bool gameOver = false;

    [SerializeField]
    private GameObject gameOverPrefab;

    private GameObject _canvas;

    public bool GameOver {
        get { return gameOver; }
    }

    // Use this for initialization
    void Start() {
        _canvas = GameObject.Find("UICanvas");
    }

    // Update is called once per frame
    void Update() {

    }

    public void EndGame() {
        gameOver = true;
        Time.timeScale = 0.5f; // Slowdown time to half
        Instantiate(gameOverPrefab, _canvas.transform);
        Debug.Log("Game Over!");
    }
}
