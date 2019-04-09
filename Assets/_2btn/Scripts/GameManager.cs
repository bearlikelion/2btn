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
    private float zoomSpeed;

    public bool GameOver {
        get { return gameOver; }
    }

    // Use this for initialization
    void Start() {
        mainCamera = Camera.main;
        if (!gameOver && gameOverScreen.activeSelf) {
            gameOverScreen.SetActive(false);
        }
        StartCoroutine("CameraZoom");
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
        Time.fixedDeltaTime = 0.02f * Time.timeScale; // smooth slow motion
        Debug.Log("Game Over!");
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
