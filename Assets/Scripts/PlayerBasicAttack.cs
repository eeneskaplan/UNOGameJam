using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject bulletPrefab; 
    public Transform firePoint;     // Merminin çýkacaðý nokta
    public float fireRate = 0.5f;   // Ýki atýþ arasý bekleme süresi
    private float nextFireTime = 0f;
    public int mermiHasari = 25;

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
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        Vector2 lookDirection = mousePosition - transform.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        // Mermiyi oluþtur ve bir deðiþkene ata
        GameObject yeniMermi = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0, 0, angle));

        // Merminin hasarýný, karakterin o anki hasarýna eþitle
        yeniMermi.GetComponent<Bullet>().damage = mermiHasari;

        GetComponent<DebuffManager>().AddToBar(false);
    }
}