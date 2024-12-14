using System.Collections;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public GameObject destructionFX;

    public int maxHealth = 100; // Salud máxima
    private int currentHealth;

    public bool isShieldActive = false; // Estado del escudo
    public float shieldDuration = 5f; // Duración del escudo

    public TextMeshProUGUI healthText; // Texto para mostrar la salud

    private void Awake()
    {
        currentHealth = maxHealth;
        UpdateHealthText();
    }

    public void GetDamage(int damage)
    {
        if (isShieldActive) // Si el escudo está activo, ignora el daño
        {
            Debug.Log("Shield absorbed the damage!");
            return;
        }

        currentHealth -= damage;
        UpdateHealthText();

        if (currentHealth <= 0)
        {
            Destruction();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // No exceder la salud máxima
        UpdateHealthText();
    }

    public void ActivateShield()
    {
        StartCoroutine(ShieldCoroutine());
    }

    private IEnumerator ShieldCoroutine()
    {
        isShieldActive = true;
        Debug.Log("Shield activated!");
        yield return new WaitForSeconds(shieldDuration);
        isShieldActive = false;
        Debug.Log("Shield deactivated!");
    }

    private void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + currentHealth;
        }
    }

    private void Destruction()
    {
        Instantiate(destructionFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
