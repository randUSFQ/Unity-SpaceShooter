using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Tooltip("Damage which a projectile deals to another object")]
    public int damage;

    [Tooltip("Whether the projectile belongs to the 'Enemy' or to the 'Player'")]
    public bool enemyBullet;

    [Tooltip("Whether the projectile is destroyed in the collision or not")]
    public bool destroyedByCollision;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (enemyBullet && collision.CompareTag("Player")) // Si el proyectil es del enemigo y golpea al jugador
        {
            Debug.Log($"Projectile collided with: {collision.name}");
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                player.GetDamage(damage); // Aplica daño al jugador
            }

            if (destroyedByCollision)
            {
                Destroy(gameObject); // Destruye el proyectil tras la colisión
            }
        }
        else if (!enemyBullet && collision.CompareTag("Enemy")) // Si el proyectil es del jugador y golpea a un enemigo
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.GetDamage(damage); // Aplica daño al enemigo
            }

            if (destroyedByCollision)
            {
                Destroy(gameObject); // Destruye el proyectil tras la colisión
            }
        }
    }
}


