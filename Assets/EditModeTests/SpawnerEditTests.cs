using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class SpawnerEditTests
{
    private readonly GameObject spawnerPrefab =
        AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Spawner_PF.prefab");
    private GameObject spawner;

    [SetUp]
    public void SetUp()
    {
        spawner = Object.Instantiate(spawnerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }

    [TearDown]
    public void TearDown()
    {
        GameObject.DestroyImmediate(spawner);
    }

    [Test]
    public void Spawner_ShouldNotSpawnObstacles_BeforeCalled()
    {
        Spawner spawnerScript = spawner.GetComponent<Spawner>();
        // Disable automatic spawning
        spawnerScript.spawnTime = 0;
        Obstacle[] obstacles = Object.FindObjectsByType<Obstacle>(FindObjectsSortMode.None);
        Assert.IsEmpty(obstacles);
    }

    [Test]
    public void Spawner_ShouldSpawnObstacle_WhenCalled()
    {
        Spawner spawnerScript = spawner.GetComponent<Spawner>();

        // Disable automatic spawning
        spawnerScript.spawnTime = 0;
        spawnerScript.SpawnObstacle();
        Obstacle[] obstacles = Object.FindObjectsByType<Obstacle>(FindObjectsSortMode.None);
        Assert.IsNotEmpty(obstacles);
    }

    [Test]
    public void Spawner_ShouldSpawnObstacle_ToRightOfPlayer()
    {
        Spawner spawnerScript = spawner.GetComponent<Spawner>();
        // Disable automatic spawning
        spawnerScript.spawnTime = 0;
        spawnerScript.SpawnObstacle();
        // Assume the player will always be at x = 0.
        Obstacle obstacle = Object.FindObjectsByType<Obstacle>(FindObjectsSortMode.None)[0];
        Assert.Greater(obstacle.transform.position.x, 0);
    }
}
