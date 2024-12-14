using UnityEngine;

public class Bonus : MonoBehaviour
{
    [Tooltip("Type of bonus: WeaponUpgrade, Shield, Heal")]
    public enum BonusType { WeaponUpgrade, Shield, Heal }
    public BonusType type;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerShooting playerShooting = collision.GetComponent<PlayerShooting>();
            if (playerShooting != null)
            {
                switch (type)
                {
                    case BonusType.WeaponUpgrade:
                        playerShooting.UpgradeWeapon();
                        break;
                        // Otros tipos de bonus
                }
            }

            Destroy(gameObject); // Destruye el bonus tras recogerlo
        }
    }
}
