using UnityEngine;

public class DusmanSaldiri : MonoBehaviour
{
    [Header("Düþman Saldýrý Ayarlarý")]
    public int hasarMiktari = 10;
    public float vurmaMenzili = 1f;
    public float saldiriHizi = 1.5f;

    private float siradakiSaldiriZamani = 0f;
    private Transform oyuncu;

    // DÝKKAT: "OrtakCanScripti" yazan yerleri kendi scriptinin adýyla (Örn: Health) deðiþtir
    private Health oyuncuCanScripti;

    void Start()
    {
        // 1. Sadece "Player" etiketli objeyi bul
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            oyuncu = playerObj.transform;

            // 2. O objenin (yani Karakterin) üzerindeki ortak can scriptini çek
            oyuncuCanScripti = playerObj.GetComponent<Health>();
        }
    }

    void Update()
    {
        if (oyuncu == null || oyuncuCanScripti == null) return;

        // Mesafeyi ölç
        float mesafe = Vector2.Distance(transform.position, oyuncu.position);

        // Menzildeyse ve cooldown dolduysa vur
        if (mesafe <= vurmaMenzili && Time.time >= siradakiSaldiriZamani)
        {
            Saldir();
            siradakiSaldiriZamani = Time.time + saldiriHizi;
        }
    }

    void Saldir()
    {
        // Ortak scriptindeki hasar alma fonksiyonunun adýný buraya yaz (Örn: HasarAl)
        oyuncuCanScripti.TakeDamage(hasarMiktari);
        Debug.Log("Düþman sana " + hasarMiktari + " hasar vurdu!");
    }
}