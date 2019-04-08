using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private bool gameOver = false;

    public bool GameOver {
        get { return gameOver; }
    }

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void EndGame() {
        Debug.Log("Game Over!");
    }
}
