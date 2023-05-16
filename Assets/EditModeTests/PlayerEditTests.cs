using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TestTools;

public class PlayerEditTests : InputTestFixture
{
    private readonly GameObject playerPrefab = 
        AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Player_PF.prefab");
    private GameObject player;

    // We override Setup() and TearDown() because this class inherits from InputTestFixture.
    public override void Setup()
    {
        player = Object.Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }

    public override void TearDown()
    {
        GameObject.DestroyImmediate(player);
    }

    [Test]
    public void Check_PlayerExists_WhenInstantiated()
    {
        Assert.NotNull(player);
    }

    [Test]
    public void Check_PlayerUsingNoFireImg_WhenFalling()
    {
        Sprite noFireVersion = Resources.Load<Sprite>("Sprites/Player/JetNoFire");
        Sprite currentImg = player.GetComponent<SpriteRenderer>().sprite;

        Assert.AreEqual(noFireVersion, currentImg);
    }
}
