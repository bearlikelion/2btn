using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private bool gameOver = false;

    [SerializeField]
    private GameObject gameOverScreen;    

    public bool GameOver {
        get { return gameOver; }
    }

    // Use this for initialization
    void Start() {        
        if (!gameOver && gameOverScreen.activeSelf) {
            gameOverScreen.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            RestartGame();
        }        
    }

    public void RestartGame() {        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);        
    }

    public void EndGame() {
        gameOver = true;
        gameOverScreen.SetActive(true);

        Time.timeScale = 0.5f; // Slowdown time to half        
        Debug.Log("Game Over!");
    }
}
