using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour {

    [SerializeField]
    private float spawnTime = 3.0f;

    [SerializeField]
    private GameObject obstaclePrefab;

    [SerializeField]
    private GameObject[] obstacles;    

    private bool hasSpawned = false;        
    private float xPos, yPos;

    private int minP, maxP, spawnTick, spawnCount;

    private List<GameObject> _obstacles;
    private PlayerController player;
    private Quaternion spawnRot;
    private System.Random rand;    

	// Use this for initialization
	void Start () {        
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        rand = new System.Random();
        CreateSpawnList();
        SetSpawnTick();
    }

    void SetSpawnTick() {        
        spawnTick = Random.Range(0, 6);
        spawnCount = 0;
    }
	
    void CreateSpawnList() {
        _obstacles = obstacles.ToList();

        // Save last entry in array as the 'longBlock'
        // GameObject longBlock = _obstacles.Last();
        // _obstacles.RemoveAt(_obstacles.Count - 1); // remove 'longBlock' from list
        // _obstacles = _obstacles.OrderBy(x => rand.Next()).ToList(); // randomize the order of the list        
        // _obstacles.Add(longBlock); // place 'longBlock' at the end        
    }

	// Update is called once per frame
	void Update () {
		if (!hasSpawned) {
            StartCoroutine(SpawnObstacle());
        }
	}

    IEnumerator SpawnObstacle() {
        hasSpawned = true;
        spawnCount++;

        // Reset min/max positions
        minP = -6;
        maxP = 7; // Random.Range is EXCLUSIVE for max with Integers (stupid unity)

        // TODO: obstacles larger than 1 unit        
        GameObject _obstacle = _obstacles.First(); // select first obstacle

        if (spawnCount == spawnTick) {
            _obstacles.Remove(_obstacles.First()); // remove it from the list
            SetSpawnTick();
        }        

        float xScale = _obstacle.transform.localScale.x;
        minP += (int) xScale/2;
        maxP -= (int) xScale/2;

        // Spawn obstacles on Player.SIDE
        switch (player.currentSide) {
            case PlayerController.SIDE.BOTTOM:
                spawnRot = Quaternion.Euler(0, 0, 0);
                xPos = Random.Range(minP, maxP);
                yPos = -6;
                break;
            case PlayerController.SIDE.LEFT:
                spawnRot = Quaternion.Euler(0, 0, 90f);
                yPos = Random.Range(minP, maxP);
                xPos = -6;                
                break;
            case PlayerController.SIDE.TOP:
                spawnRot = Quaternion.Euler(0, 0, 0);
                xPos = Random.Range(minP, maxP);
                yPos = 6;
                break;
            case PlayerController.SIDE.RIGHT:
                spawnRot = Quaternion.Euler(0, 0, 90f);
                yPos = Random.Range(minP, maxP);
                xPos = 6;                
                break;
        }                

        // magic number 88 because my wife loves 8s
        Instantiate(_obstacle, new Vector3(xPos, yPos, 88.0f), spawnRot);

        yield return new WaitForSeconds(spawnTime);

        hasSpawned = false;
    }
}
