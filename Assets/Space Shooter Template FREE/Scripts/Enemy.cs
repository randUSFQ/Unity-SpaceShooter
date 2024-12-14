using UnityEngine;
using System.Collections;

/// <summary>
/// This script defines 'Enemy's' health and behavior.
/// </summary>
public class Enemy : MonoBehaviour
{
    [Tooltip("Health points in integer")]
    public int health = 5;

    [Tooltip("Enemy's projectile prefab")]
    public GameObject Projectile;

    [Tooltip("VFX prefab generating after destruction")]
    public GameObject destructionVFX;
    public GameObject hitEffect;

    [Tooltip("Chance of shooting (0-100%)")]
    public int shotChance = 50;

    [Tooltip("Minimum and maximum time before the enemy shoots")]
    public float shotTimeMin = 1f;
    public float shotTimeMax = 3f;

    private void Start()
    {
        StartCoroutine(ShootingRoutine());
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
                Instantiate(Projectile, transform.position, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning("Projectile prefab is not assigned to the Enemy script.");
            }
        }
    }

    public void GetDamage(int damage)
    {
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                player.GetDamage(1); // Aplica daño al jugador
            }

            // Destruir el enemigo tras colisión (opcional)
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
