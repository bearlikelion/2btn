using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour {

    [SerializeField]
    private float spawnTime = 3.0f;

    [SerializeField]
    private GameObject[] obstacles;

    private int minP, maxP, spawnTick, spawnCount, totalSpawned;
    private bool hasSpawned = false, onPlayer = false;
    private float xPos, yPos;

    private Quaternion spawnRotation;

    private List<GameObject> _obstacles;
    private GameManager _gameManager;
    private PlayerController player;
    private System.Random rand;

    private List<string> spawnDirections = new List<string> {
        "TOP",
        "RIGHT",
        "BOTTOM",
        "LEFT"
    };

    // Use this for initialization
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _gameManager = GameObject.Find("GameManger").GetComponent<GameManager>();
        _obstacles = obstacles.ToList();
        rand = new System.Random();
        totalSpawned = 0;
        SetSpawnTick();
    }

    void SetSpawnTick() {
        spawnTick = Random.Range(1, 4);
        spawnCount = 0;
    }

    void ShuffleSpawnList() {
        _obstacles = obstacles.ToList();
        _obstacles = _obstacles.OrderBy(x => rand.Next()).ToList(); // randomize the order of the list
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
        spawnCount++;

        // Reset
        minP = -6;
        maxP = 7; // Random.Range is EXCLUSIVE for max with Integers

        if (_obstacles.Count == 0) {
            ShuffleSpawnList();
        }

        GameObject _obstacle = _obstacles.First(); // select first obstacle

        if (!player.wallClimb) {
            if (spawnCount == spawnTick) {
                _obstacles.Remove(_obstacles.First()); // remove it from the list
                SetSpawnTick();
            }
        } else {
            int randomEntry = Random.Range(0, _obstacles.Count);
            _obstacle = _obstacles[randomEntry]; // select random obstacle
            _obstacles.Remove(_obstacles[randomEntry]);
        }

        float xScale = _obstacle.transform.localScale.x;
        minP += (int)xScale / 2;
        maxP -= (int)xScale / 2;

        // Spawn obstacles on Player.SIDE (Ground) before they wallClimb
        switch (wall) {
            case "TOP":
                spawnRotation = Quaternion.Euler(0, 0, 0);
                xPos = Random.Range(minP, maxP);
                yPos = 6;
                break;
            case "RIGHT":
                spawnRotation = Quaternion.Euler(0, 0, 90f);
                yPos = Random.Range(minP, maxP);
                xPos = 6;
                break;
            case "BOTTOM":
                spawnRotation = Quaternion.Euler(0, 0, 0);
                xPos = Random.Range(minP, maxP);
                yPos = -6;
                break;
            case "LEFT":
                spawnRotation = Quaternion.Euler(0, 0, 90f);
                yPos = Random.Range(minP, maxP);
                xPos = -6;
                break;
        }

        // magic z:Pos 88 because my wife loves 8s
        Instantiate(_obstacle, new Vector3(xPos, yPos, 88.0f), spawnRotation);

        // Every 10 spawned obstacles reduce spawn time by 0.25s
        if (totalSpawned % 10 == 0 && spawnTime > 0.25f) {
            spawnTime -= 0.25f;
        }

        yield return new WaitForSeconds(spawnTime);
        hasSpawned = false;
    }
}
