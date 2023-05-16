using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacter : MonoBehaviour
{
    public InputMaster controls;

    public float maxHeight = 500;
    public float jumpSpeed = 15f;
    public float fallingConstant = 9.8f;
    private float verticalSpeed = 0f;
    public float rotationRate = 10f;

    private Sprite noFireVersion;
    private Sprite fireVersion;

    /// <summary>
    /// Sets up the player controls and the sprites.
    /// </summary>
    private void Awake()
    {
        SetupControls();
        SetupSprites();       
    }

    /// <summary>
    /// Sets up the input controls.
    /// </summary>
    private void SetupControls()
    {
        controls = new InputMaster();
        controls.Player.Jump.performed += context => OnJump();
    }

    /// <summary>
    /// Sets up the fire and no fire sprites.
    /// </summary>
    private void SetupSprites()
    {
        noFireVersion = Resources.Load<Sprite>("Sprites/Player/JetNoFire");
        fireVersion = Resources.Load<Sprite>("Sprites/Player/JetWithFire");

        GetComponent<SpriteRenderer>().sprite = noFireVersion;
    }

    /// <summary>
    /// Turn on controls when the player is enabled.
    /// </summary>
    private void OnEnable()
    {
        controls.Enable();
    }

    /// <summary>
    /// Turn off controls when the player is disabled.
    /// </summary>
    private void OnDisable()
    {
        controls.Disable();
    }

    /// <summary>
    /// Updates the player's y position and vertical speed each tick to simulate gravity.
    /// </summary>
    private void Update()
    {
        float y = transform.position.y + verticalSpeed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, y, 0);
        verticalSpeed -= fallingConstant * Time.deltaTime;

        SwitchSpritesIfNeeded();
        UpdateRotation();
    }

    /// <summary>
    /// Rotates the jet forward when it's moving down and backwards when it's moving up.
    /// </summary>
    private void UpdateRotation()
    {
        float internalMultiplier = 0.01f;
        float newZ = (verticalSpeed > 0) 
             ? rotationRate * internalMultiplier
             : -1 * rotationRate * internalMultiplier;
        if (transform.rotation.z + newZ < 0.5 && transform.rotation.z + newZ > -0.5)
        {
            Vector3 rotation = new(0, 0, newZ);
            transform.Rotate(rotation, Space.Self);
        }
        
    }

    /// <summary>
    /// Switches to the fire image if the vertical speed is positive, and to the no fire version
    /// if it is negative.
    /// </summary>
    private void SwitchSpritesIfNeeded()
    {
        Sprite currentSprite = GetComponent<SpriteRenderer>().sprite;
        if (verticalSpeed > 0 && currentSprite != fireVersion)
        {
            GetComponent<SpriteRenderer>().sprite = fireVersion;
        }
        else if (verticalSpeed <= 0 && currentSprite != noFireVersion)
        {
            GetComponent<SpriteRenderer>().sprite = noFireVersion;
        }
    }

    /// <summary>
    /// Causes the player to jump when the space bar is pressed.
    /// </summary>
    private void OnJump()
    {
        float y = transform.position.y;
        if (y < maxHeight && y > (-1 * maxHeight))
        {
            verticalSpeed = jumpSpeed;
        }
    }
}
