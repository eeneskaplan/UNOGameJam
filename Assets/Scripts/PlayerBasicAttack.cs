using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Atýþ Ayarlarý")]
    public Transform firePoint;     // Merminin çýkacaðý nokta
    public float fireRate = 0.5f;   // Ýki atýþ arasý bekleme süresi
    private float nextFireTime = 0f;
    public int mermiHasari = 20;

    [Header("Element Mermileri (Sýrayla Koy!)")]
    // 0: Ateþ, 1: Buz, 2: Duman, 3: Elektrik olacak þekilde inspector'dan sürükle
    public GameObject[] mermiPrefablari;

    private int aktifElementIndex = 0; // Varsayýlan element

    void Start()
    {
        // Oyun baþýnda seçim ekranýnda kaydedilen elementi oku
        if (PlayerPrefs.HasKey("IlkElement"))
        {
            aktifElementIndex = PlayerPrefs.GetInt("IlkElement");

            // Hangi element seçildiyse onun statlarýný belirle
            switch (aktifElementIndex)
            {
                case 0: // ATEÞ
                    mermiHasari = 25;
                    fireRate = 0.5f;
                    break;

                case 3: // ELEKTRÝK
                    mermiHasari = 15;
                    fireRate = 0.3f;
                    break;

                default: // DÝÐERLERÝ (BUZ VE DUMAN)
                    mermiHasari = 20;
                    fireRate = 0.5f;
                    break;
            }
        }
    }

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

        // 1. ESKÝSÝ YERÝNE YENÝSÝ: Tek mermi yerine, seçili elementin mermisini (prefabý) fýrlat
        GameObject yeniMermi = Instantiate(mermiPrefablari[aktifElementIndex], firePoint.position, Quaternion.Euler(0, 0, angle));

        // 2. Kendi yazdýðýn hasar aktarma sistemi (Bullet scriptine eriþim)
        yeniMermi.GetComponent<Bullet>().damage = mermiHasari;

        // 3. Kendi yazdýðýn Debuff barýný doldurma sistemi
        GetComponent<DebuffManager>().AddToBar(false);
    }
}