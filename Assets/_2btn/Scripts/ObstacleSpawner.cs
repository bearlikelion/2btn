using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour {

    [SerializeField]
    private float spawnTime = 3.0f;

    [SerializeField]
    private GameObject[] obstacles;

    private bool hasSpawned = false, onPlayer = false;
    private float xPos, yPos;
    private int totalSpawned;

    private Quaternion spawnRotation;

    private List<GameObject> _obstacles;
    private GameManager _gameManager;
    private PlayerController player;

    private List<string> spawnDirections = new List<string> {
        "TOP",
        "RIGHT",
        "BOTTOM",
        "LEFT"
    };

    // Use this for initialization
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _obstacles = obstacles.ToList();
        totalSpawned = 0;
    }

    // Update is called once per frame
    void Update() {
        if (!hasSpawned && !_gameManager.GameOver) {
            if (player.wallClimb) {
                if (onPlayer) {
                    string randomWall = SelectRandomWall();
                    StartCoroutine(SpawnObstacle(randomWall));
                    onPlayer = false;
                } else {
                    StartCoroutine(SpawnObstacle(player.currentSide.ToString()));
                    onPlayer = true;
                }
            } else {
                StartCoroutine(SpawnObstacle("BOTTOM"));
            }
        }
    }

    string SelectRandomWall() {
        string randomWall;
        var randomIndex = Random.Range(0, spawnDirections.Count);

        randomWall = spawnDirections[randomIndex];
        if (spawnDirections[randomIndex] == player.currentSide.ToString()) {
            randomWall = SelectRandomWall(); // Recusive if random == currentSide
        }
        return randomWall;
    }

    IEnumerator SpawnObstacle(string wall) {
        hasSpawned = true;
        totalSpawned++;

        int minPosition = -6;
        int maxPosition = 7; // Random.Range is EXCLUSIVE for max with Integers

        int diceRoll = Random.Range(0, 6);
        GameObject _obstacle; // select first obstacle

        if (diceRoll > 4) {
            _obstacle = _obstacles[Random.Range(6, _obstacles.Count)];
        } else {
            _obstacle = _obstacles[Random.Range(0, 6)];
        }

        float xScale = _obstacle.transform.localScale.x;
        minPosition += (int)xScale / 2;
        maxPosition -= (int)xScale / 2;

        switch (wall) {
            case "TOP":
                spawnRotation = Quaternion.Euler(0, 0, 0);
                xPos = Random.Range(minPosition, maxPosition);
                yPos = 6;
                break;
            case "RIGHT":
                spawnRotation = Quaternion.Euler(0, 0, 90f);
                yPos = Random.Range(minPosition, maxPosition);
                xPos = 6;
                break;
            case "BOTTOM":
                spawnRotation = Quaternion.Euler(0, 0, 0);
                xPos = Random.Range(minPosition, maxPosition);
                yPos = -6;
                break;
            case "LEFT":
                spawnRotation = Quaternion.Euler(0, 0, 90f);
                yPos = Random.Range(minPosition, maxPosition);
                xPos = -6;
                break;
        }

        //Quick fix for obstacles matching the grid
        if (_obstacle.transform.localScale.x % 2 == 0) {
            if (wall == "TOP" || wall == "BOTTOM") {
                if (xPos >= 0)  xPos += 0.5f;
                else xPos -= 0.5f;
            } else {
                if (yPos >= 0) yPos += 0.5f;
                else yPos -= 0.5f;
            }
        }
        // magic z:Pos 88 because my wife loves 8s
        Instantiate(_obstacle, new Vector3(xPos, yPos, 88.0f), spawnRotation);

        // Every 5 spawned obstacles increased diffculty
        if (totalSpawned % 5 == 0) {
            _gameManager.Tick();
            if (spawnTime > 0.25f) {
                spawnTime -= 0.25f;
            }
        }

        yield return new WaitForSeconds(spawnTime);
        hasSpawned = false;
    }
}
