using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private bool gameOver = false;

    [SerializeField]
    private GameObject gameOverScreen;

    private PlayerController playerController;
    
    private Rigidbody playerRb;
    private GameObject canvas;
    private GameObject player;
    private GameObject cam;    

    private Vector3 playerStartPosition;

    public bool GameOver {
        get { return gameOver; }
    }

    // Use this for initialization
    void Start() {        
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        playerRb = player.GetComponent<Rigidbody>();        

        canvas = GameObject.Find("UICanvas");
        cam = Camera.main.gameObject;

        playerStartPosition = player.transform.position;        
        
        if (!gameOver && gameOverScreen.activeSelf) {
            gameOverScreen.SetActive(false);
        }

        Debug.Log(playerRb.velocity);
        Debug.Log(playerRb.angularVelocity);
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
