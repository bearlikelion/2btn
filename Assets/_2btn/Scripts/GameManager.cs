using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private bool gameOver = false;

    [SerializeField]
    private GameObject gameOverScreen;

    private Camera mainCamera;

    private float tickDifficulty;
    private float zoomSpeed;

    public bool GameOver {
        get { return gameOver; }
    }

    public float Difficulty {
        get { return tickDifficulty;  }
    }

    // Use this for initialization
    void Start() {
        mainCamera = Camera.main;
        tickDifficulty = 0;

        if (!gameOver && gameOverScreen.activeSelf) {
            gameOverScreen.SetActive(false);
        }

        StartCoroutine("CameraZoom");
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            RestartGame();
        }

        if(Input.GetButtonDown(KeyCode.Q))
    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void EndGame() {
        gameOver = true;
        gameOverScreen.SetActive(true);

        Time.timeScale = 0.5f; // Slowdown time to half
        Time.fixedDeltaTime = 0.02f * Time.timeScale; // smooth slow motion
        Debug.Log("Game Over!");
    }

    // Increase difficulty
    public void Tick() {
        float difficultyIncrease = 1.0f;

        tickDifficulty += difficultyIncrease;
        ScrollTexture[] walls = FindObjectsOfType(typeof(ScrollTexture)) as ScrollTexture[];
        foreach (ScrollTexture wall in walls) {
            wall.scrollSpeed += difficultyIncrease;
        }
    }

    IEnumerator CameraZoom()
    {
        while(mainCamera.fieldOfView > 60f)
        {
            zoomSpeed -= Time.deltaTime * 1.0f;
            mainCamera.fieldOfView += Time.deltaTime * zoomSpeed;
            yield return null;
        }
        mainCamera.fieldOfView = 60f;
    }
}
