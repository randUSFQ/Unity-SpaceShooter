using UnityEngine;
using TMPro;

[System.Serializable]
public class Guns
{
    public GameObject rightGun, leftGun, centralGun; // Posiciones de las armas
    [HideInInspector] public ParticleSystem leftGunVFX, rightGunVFX, centralGunVFX; // Efectos de disparo
}

public class PlayerShooting : MonoBehaviour
{
    public float fireRate = 5f;
    public GameObject projectileObject;

    public int weaponPower = 1; // Nivel actual del arma
    public int maxweaponPower = 4; // Nivel máximo de armas

    public Guns guns;
    public TextMeshProUGUI weaponLevelText; // Texto para mostrar el nivel de armas

    private float nextFire;

    private void Start()
    {
        guns.leftGunVFX = guns.leftGun.GetComponent<ParticleSystem>();
        guns.rightGunVFX = guns.rightGun.GetComponent<ParticleSystem>();
        guns.centralGunVFX = guns.centralGun.GetComponent<ParticleSystem>();

        UpdateWeaponLevelText();
    }

    private void Update()
    {
        if (Time.time > nextFire)
        {
            MakeAShot();
            nextFire = Time.time + 1 / fireRate;
        }
    }

    void MakeAShot()
    {
        switch (weaponPower)
        {
            case 1:
                CreateLazerShot(projectileObject, guns.centralGun.transform.position, Vector3.zero);
                guns.centralGunVFX.Play();
                break;
            case 2:
                CreateLazerShot(projectileObject, guns.rightGun.transform.position, Vector3.zero);
                CreateLazerShot(projectileObject, guns.leftGun.transform.position, Vector3.zero);
                guns.rightGunVFX.Play();
                guns.leftGunVFX.Play();
                break;
            case 3:
                CreateLazerShot(projectileObject, guns.centralGun.transform.position, Vector3.zero);
                CreateLazerShot(projectileObject, guns.rightGun.transform.position, new Vector3(0, 0, -5));
                CreateLazerShot(projectileObject, guns.leftGun.transform.position, new Vector3(0, 0, 5));
                guns.rightGunVFX.Play();
                guns.leftGunVFX.Play();
                break;
            case 4:
                // Implementa disparos adicionales aquí
                break;
        }
    }

    void CreateLazerShot(GameObject lazer, Vector3 pos, Vector3 rot)
    {
        Instantiate(lazer, pos, Quaternion.Euler(rot));
    }

    public void UpgradeWeapon()
    {
        if (weaponPower < maxweaponPower)
        {
            weaponPower++;
            UpdateWeaponLevelText();
        }
    }

    private void UpdateWeaponLevelText()
    {
        if (weaponLevelText != null)
        {
            weaponLevelText.text = "Weapon Level: " + weaponPower;
        }
    }
}
