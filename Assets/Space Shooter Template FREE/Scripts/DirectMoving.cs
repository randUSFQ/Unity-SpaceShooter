using UnityEngine;

/// <summary>
/// Moves the attached object (projectile or enemy) along the Y-axis
/// and destroys it if it moves out of the screen bounds.
/// </summary>
public class DirectMoving : MonoBehaviour
{
    [Tooltip("Moving speed along the Y-axis")]
    public float speed;

    [Tooltip("Defines if the object moves upward (true) or downward (false)")]
    public bool movesUpward = true;

    [Tooltip("Destroy the object if it leaves the screen bounds")]
    public bool destroyOutOfBounds = true;

    [Tooltip("Bounds for destruction (if destroyOutOfBounds is enabled)")]
    public float upperBound = 10f;
    public float lowerBound = -10f;

    private void Update()
    {
        // Determine the direction of movement
        float direction = movesUpward ? 1f : -1f;

        // Move the object along the Y-axis
        transform.Translate(Vector3.up * direction * speed * Time.deltaTime);

        // Check for out-of-bounds and destroy if enabled
        if (destroyOutOfBounds && (transform.position.y > upperBound || transform.position.y < lowerBound))
        {
            Destroy(gameObject);
        }
    }
}

