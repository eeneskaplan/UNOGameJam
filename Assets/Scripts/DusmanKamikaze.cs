using UnityEngine;

public class DusmanKamikaze : MonoBehaviour
{
    [Header("Ayarlar")]
    public float hareketHizi = 4f;
    public float patlamaMenzili = 1.2f;
    public GameObject patlamaEfektiPrefab; // Patlama prefabýný buraya sürükle

    private Transform oyuncu;
    private Rigidbody2D rb; // FÝZÝK ÝÇÝN EKLENDÝ

    void Start()
    {
        // RÝGÝDBODY BÝLEÞENÝNÝ KODA BAÐLADIK
        rb = GetComponent<Rigidbody2D>();

        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) oyuncu = p.transform;
    }

    // FÝZÝK ÝÞLEMLERÝ ÝÇÝN FIXEDUPDATE KULLANILIR
    void FixedUpdate()
    {
        if (oyuncu == null) return;

        // 1. Oyuncuya doðru fizik kurallarýna uygun olarak yürü
        Vector2 yeniPozisyon = Vector2.MoveTowards(rb.position, (Vector2)oyuncu.position, hareketHizi * Time.fixedDeltaTime);
        rb.MovePosition(yeniPozisyon);

        // 2. Menzile girdiyse patla (Mesafeyi ölçerken de rb.position kullanýyoruz)
        if (Vector2.Distance(rb.position, (Vector2)oyuncu.position) <= patlamaMenzili)
        {
            Patla();
        }
    }

    public void Patla()
    {
        // Patlama efektini doður
        if (patlamaEfektiPrefab != null)
        {
            Instantiate(patlamaEfektiPrefab, transform.position, Quaternion.identity);
        }

        // Kendini yok et
        Destroy(gameObject);
    }
}