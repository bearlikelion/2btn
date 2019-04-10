using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private bool gameOver = false;

    [SerializeField]
    private GameObject gameOverScreen;

    [SerializeField]
    private float zoomSpeed;

    private Camera mainCamera;
    private PlayerController player;
    private HighScores highScores;
    private PlayerGUID playerGuid;

    private int obstaclesAvoided;
    private float startTime, endTime;
    private float tickDifficulty;

    public bool GameOver {
        get { return gameOver; }
    }

    public float Difficulty {
        get { return tickDifficulty;  }
    }

    public int ObstaclesAvoided {
        get { return obstaclesAvoided; }
    }

    public float TimeAlive {
        get { return CalculateTimeAlive(); }
    }

    // Use this for initialization
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerGuid = GameObject.Find("PlayerGUID").GetComponent<PlayerGUID>();
        highScores = GetComponent<HighScores>();
        mainCamera = Camera.main;
        startTime = Time.time;
        obstaclesAvoided = 0;
        tickDifficulty = 0;
        endTime = 0;

        Time.timeScale = 1f;

        // if Game not over but GameOver screen is showing - hide it
        if (!gameOver && gameOverScreen.activeSelf) {
            gameOverScreen.SetActive(false);
        }
        StartCoroutine("CameraZoom");
    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ViewHighScores() {
        SceneManager.LoadScene("HighScores");
    }

    public void EndGame() {
        gameOver = true;
        endTime = Time.time;
        gameOverScreen.SetActive(true);

        GameObject tip = gameOverScreen.transform.Find("Tip").gameObject;
        if (!player.wallClimb) {
            tip.SetActive(true);
        } else {
            tip.SetActive(false);
        }

        highScores.Submit(playerGuid.Guid, obstaclesAvoided, CalculateTimeAlive()); // Submit score to leaderboard

        Time.timeScale = 0.5f; // Slowdown time to half
        Time.fixedDeltaTime = 0.02f * Time.timeScale; // smooth slow motion
        Debug.Log("Game Over!");
    }

    private float CalculateTimeAlive() {
        float timeAlive = 0;
        if (endTime > 0) {
            timeAlive = endTime - startTime;
        } else {
            timeAlive = Time.time - startTime;
        }

        return Mathf.Round(timeAlive);
    }

    public void ObstacleAvoided() {
        obstaclesAvoided++;
    }

    // Increase difficulty
    public void Tick() {
        float difficultyIncrease = 1.0f;

        tickDifficulty += difficultyIncrease;

        ScrollTexture[] walls = FindObjectsOfType(typeof(ScrollTexture)) as ScrollTexture[];
        foreach (ScrollTexture wall in walls) {
            if (wall.gameObject.name != "End") {
                wall.scrollSpeed += difficultyIncrease;
            }
        }
    }

    IEnumerator CameraZoom()
    {
        while(mainCamera.fieldOfView > 60f)
        {
            zoomSpeed -= Time.deltaTime * 1.0f;
            mainCamera.fieldOfView -= Time.deltaTime * zoomSpeed;
            yield return null;
        }
        mainCamera.fieldOfView = 60f;
    }
}
