using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script defines the borders of the player's movement and enables movement via pointer or touch input.
/// </summary>
[System.Serializable]
public class Borders
{
    [Tooltip("Offsets from viewport borders for player's movement")]
    public float minXOffset = 1.5f, maxXOffset = 1.5f, minYOffset = 1.5f, maxYOffset = 1.5f;
    [HideInInspector] public float minX, maxX, minY, maxY;
}

public class PlayerMoving : MonoBehaviour
{
    [Tooltip("Offset from viewport borders for player's movement")]
    public Borders borders;

    Camera mainCamera;
    bool controlIsActive = true; // Determines if player movement is active

    public static PlayerMoving instance; // Singleton for easy access to the script

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        mainCamera = Camera.main;
        ResizeBorders(); // Adjust movement boundaries based on the viewport size
    }

    private void Update()
    {
        if (controlIsActive)
        {
#if UNITY_STANDALONE || UNITY_EDITOR // Desktop and editor platforms
            if (Input.GetMouseButton(0)) // If left mouse button is pressed
            {
                Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition); // Convert mouse position to world space
                mousePosition.z = transform.position.z; // Keep z-coordinate unchanged
                transform.position = Vector3.MoveTowards(transform.position, mousePosition, 30 * Time.deltaTime);
            }
#endif

#if UNITY_IOS || UNITY_ANDROID // Mobile platforms
            if (Input.touchCount > 0) // If there is a touch
            {
                Touch touch = Input.GetTouch(0); // Get the first touch
                Vector3 touchPosition = mainCamera.ScreenToWorldPoint(touch.position); // Convert touch position to world space
                touchPosition.z = transform.position.z; // Keep z-coordinate unchanged
                transform.position = Vector3.MoveTowards(transform.position, touchPosition, 30 * Time.deltaTime);
            }
#endif

            // Clamp player position within boundaries
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, borders.minX, borders.maxX),
                Mathf.Clamp(transform.position.y, borders.minY, borders.maxY),
                transform.position.z
            );
        }
    }

    // Adjust player movement boundaries based on viewport size and defined offsets
    void ResizeBorders()
    {
        borders.minX = mainCamera.ViewportToWorldPoint(Vector2.zero).x + borders.minXOffset;
        borders.maxX = mainCamera.ViewportToWorldPoint(Vector2.right).x - borders.maxXOffset;
        borders.minY = mainCamera.ViewportToWorldPoint(Vector2.zero).y + borders.minYOffset;
        borders.maxY = mainCamera.ViewportToWorldPoint(Vector2.up).y - borders.maxYOffset;
    }

    // Enable or disable player movement
    public void ToggleMovement(bool isActive)
    {
        controlIsActive = isActive;
    }
}
