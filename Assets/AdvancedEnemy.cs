using UnityEngine;
using System.Collections;

/// <summary>
/// This script defines an advanced enemy's health, behavior, and unique actions.
/// </summary>
public class AdvancedEnemy : MonoBehaviour
{
    [Header("Health Settings")]
    [Tooltip("Health points in integer")]
    public int health = 10;

    [Tooltip("VFX prefab generating after destruction")]
    public GameObject destructionVFX;
    public GameObject hitEffect;

    [Header("Shooting Settings")]
    [Tooltip("Enemy's projectile prefab")]
    public GameObject Projectile;

    [Tooltip("Chance of shooting (0-100%)")]
    public int shotChance = 70;

    [Tooltip("Minimum and maximum time before the enemy shoots")]
    public float shotTimeMin = 0.5f;
    public float shotTimeMax = 2f;

    [Tooltip("Additional projectile angles for multi-directional shooting")]
    public float[] additionalAngles = { 30f, -30f };

    [Header("Movement Settings")]
    [Tooltip("Speed of vertical movement")]
    public float verticalSpeed = 1f;

    [Tooltip("Amplitude of zigzag movement")]
    public float zigzagAmplitude = 2f;

    [Tooltip("Frequency of zigzag movement")]
    public float zigzagFrequency = 2f;

    [Header("Shield Settings")]
    [Tooltip("Is the enemy shielded initially?")]
    public bool isShielded = true;

    [Tooltip("Duration of the shield in seconds")]
    public float shieldDuration = 3f;

    private void Start()
    {
        // Start the shield cooldown and shooting routine
        StartCoroutine(ShieldCooldown());
        StartCoroutine(ShootingRoutine());
    }

    private void Update()
    {
        // Handle zigzag movement
        float xOffset = Mathf.Sin(Time.time * zigzagFrequency) * zigzagAmplitude * Time.deltaTime;
        transform.position += new Vector3(xOffset, -verticalSpeed * Time.deltaTime, 0);
    }

    private IEnumerator ShootingRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(shotTimeMin, shotTimeMax));
            ActivateShooting();
        }
    }

    private void ActivateShooting()
    {
        if (Random.value < (float)shotChance / 100)
        {
            if (Projectile != null)
            {
                // Shoot the main projectile
                Instantiate(Projectile, transform.position, Quaternion.identity);

                // Shoot additional projectiles at specified angles
                foreach (float angle in additionalAngles)
                {
                    Instantiate(Projectile, transform.position, Quaternion.Euler(0, 0, angle));
                }
            }
            else
            {
                Debug.LogWarning("Projectile prefab is not assigned to the AdvancedEnemy script.");
            }
        }
    }

    public void GetDamage(int damage)
    {
        if (isShielded)
        {
            Debug.Log("Shield absorbed the damage!");
            return;
        }

        health -= damage;

        if (health <= 0)
        {
            Destruction();
        }
        else if (hitEffect != null)
        {
            Instantiate(hitEffect, transform.position, Quaternion.identity);
        }
    }

    private IEnumerator ShieldCooldown()
    {
        yield return new WaitForSeconds(shieldDuration);
        isShielded = false; // Disable the shield after the duration
        Debug.Log("Shield deactivated.");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                player.GetDamage(1); // Apply damage to the player
            }

            // Destroy the enemy upon collision (optional)
            Destruction();
        }
    }

    private void Destruction()
    {
        if (destructionVFX != null)
        {
            Instantiate(destructionVFX, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
