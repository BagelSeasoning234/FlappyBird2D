using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float spawnTime = 5.0f;
    private bool gameRunning = true;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("ObstacleSpawnRoutine");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// A coroutine that fires the SpawnObstacle() event every spawnTime number of seconds.
    /// If spawnTime is 0, no obstacles will be spawned.
    /// </summary>
    private IEnumerator ObstacleSpawnRoutine()
    {
        // wait first, then spawn
        return null;
    }

    /// <summary>
    /// Spawns an obstacle.
    /// </summary>
    public void SpawnObstacle()
    {
        // check if it's currently running first and 
        //while(gameRunning)
        //{
        //    
        //}
    }
}
