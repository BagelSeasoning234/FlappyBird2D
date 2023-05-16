using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.InputSystem;

public class PlayerPlayTests : InputTestFixture
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

    [UnityTest]
    public IEnumerator Check_GravityShouldAffectPlayerY_WhenTicked()
    {
        float startingY = player.transform.position.y;
        yield return new WaitForSeconds(Time.deltaTime);

        Assert.Less(player.transform.position.y, startingY);
    }

    [UnityTest]
    public IEnumerator Check_PlayerMovesUp_WhenJumpButtonPressed()
    {
        Keyboard keyboard = InputSystem.AddDevice<Keyboard>();
        Mouse mouse = InputSystem.AddDevice<Mouse>();

        float startingY = player.transform.position.y;
        Press(keyboard.spaceKey);
        yield return new WaitForSeconds(Time.deltaTime);

        Assert.Greater(player.transform.position.y, startingY);
    }

    [UnityTest]
    public IEnumerator Check_PlayerDoesNotMoveUp_WhenAboveScreen_IfJumpButtonPressed()
    {
        Keyboard keyboard = InputSystem.AddDevice<Keyboard>();
        Mouse mouse = InputSystem.AddDevice<Mouse>();

        float maxHeight = player.GetComponent<PlayerCharacter>().maxHeight;
        player.transform.position = new Vector3(0, maxHeight, 0);

        float startingY = player.transform.position.y;
        Press(keyboard.spaceKey);
        yield return new WaitForSeconds(0.33f);

        Assert.LessOrEqual(player.transform.position.y, startingY);
    }

    [UnityTest]
    public IEnumerator Check_PlayerDoesNotMoveUp_WhenBelowScreen_IfJumpButtonPressed()
    {
        Keyboard keyboard = InputSystem.AddDevice<Keyboard>();
        Mouse mouse = InputSystem.AddDevice<Mouse>();

        float minHeight = -1 * player.GetComponent<PlayerCharacter>().maxHeight;
        player.transform.position = new Vector3(0, minHeight, 0);

        float startingY = player.transform.position.y;
        Press(keyboard.spaceKey);
        yield return new WaitForSeconds(0.33f);

        Assert.LessOrEqual(player.transform.position.y, startingY);
    }

    [UnityTest]
    public IEnumerator Check_PlayerUsingFireImg_WhenJumping()
    {
        Keyboard keyboard = InputSystem.AddDevice<Keyboard>();
        Mouse mouse = InputSystem.AddDevice<Mouse>();
        Sprite fireVersion = Resources.Load<Sprite>("Sprites/Player/JetWithFire");

        Press(keyboard.spaceKey);
        yield return new WaitForSeconds(Time.deltaTime);

        Sprite currentImg = player.GetComponent<SpriteRenderer>().sprite;
        Assert.AreEqual(fireVersion, currentImg);
    }

    [UnityTest]
    public IEnumerator Check_PlayerIsRotatingForward_WhenFalling()
    {
        float currentZ = player.transform.rotation.z;

        yield return new WaitForSeconds(Time.deltaTime);

        Assert.Less(player.transform.rotation.z, currentZ);
    }

    [UnityTest]
    public IEnumerator Check_PlayerIsRotatingBack_WhenJumping()
    {
        Keyboard keyboard = InputSystem.AddDevice<Keyboard>();
        Mouse mouse = InputSystem.AddDevice<Mouse>();
        float currentZ = player.transform.rotation.z;

        Press(keyboard.spaceKey);
        yield return new WaitForSeconds(Time.deltaTime);

        Assert.Greater(player.transform.rotation.z, currentZ);
    }
}
