using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject bulletPrefab; 
    public Transform firePoint;     // Merminin çýkacaðý nokta
    public float fireRate = 0.5f;   // Ýki atýþ arasý bekleme süresi
    private float nextFireTime = 0f;

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime) // Sol Týk
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        // Farenin dünyadaki konumunu bul
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        // Karakterden fareye doðru olan yönü hesapla ve o açýya dön
        Vector2 lookDirection = mousePosition - transform.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        // Mermiyi oluþtur ve farenin olduðu açýya doðru döndürerek fýrlat
        Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0, 0, angle));
        GetComponent<DebuffManager>().AddToBar(false);
    }
}