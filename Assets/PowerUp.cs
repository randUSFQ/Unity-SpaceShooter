using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerUpType { Shield, Heal, WeaponUpgrade }
    public PowerUpType type;

    public int healAmount = 20; // Cantidad de curación

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            PlayerShooting playerShooting = collision.GetComponent<PlayerShooting>();

            if (player != null)
            {
                switch (type)
                {
                    case PowerUpType.Shield:
                        player.ActivateShield();
                        break;
                    case PowerUpType.Heal:
                        player.Heal(healAmount);
                        break;
                }
            }

            if (playerShooting != null && type == PowerUpType.WeaponUpgrade)
            {
                playerShooting.UpgradeWeapon();
            }

            Destroy(gameObject); // Destruir el Power-Up
        }
    }
}
