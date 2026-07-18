using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float detectionRadius = 6f; // Düþmanýn oyuncuyu fark etme mesafesi
    private Transform player;
    private Rigidbody2D rb; // FÝZÝK ÝÇÝN EKLENDÝ

    void Start()
    {
        // RÝGÝDBODY BÝLEÞENÝNÝ KODA BAÐLADIK
        rb = GetComponent<Rigidbody2D>();

        // Sahnedeki "Player" etiketine sahip objeyi bul
        GameObject target = GameObject.FindGameObjectWithTag("Player");
        if (target != null)
        {
            player = target.transform;
        }
    }

    // FÝZÝK ÝÞLEMLERÝ ÝÇÝN UPDATE YERÝNE FIXEDUPDATE KULLANILIR
    void FixedUpdate()
    {
        if (player != null)
        {
            // 1. Düþman ile oyuncu arasýndaki mesafeyi hesapla (rb.position üzerinden)
            float distanceToPlayer = Vector2.Distance(rb.position, (Vector2)player.position);

            // 2. Eðer oyuncu, düþmanýn görüþ menziline (detectionRadius) girdiyse harekete geç
            if (distanceToPlayer <= detectionRadius)
            {
                // FÝZÝK KURALLARINA UYGUN OLARAK HEDEF POZÝSYONA ÝLERLE
                Vector2 yeniPozisyon = Vector2.MoveTowards(rb.position, (Vector2)player.position, moveSpeed * Time.fixedDeltaTime);
                rb.MovePosition(yeniPozisyon);
            }
        }
    }

    // --- HAYAT KURTARAN ÝPUCU ---
    // Bu fonksiyon sadece Unity Editöründe çalýþýr.
    // Düþmana týkladýðýnda etrafýnda kýrmýzý bir çember çizer, böylece görüþ menzilini gözünle görüp ayarlayabilirsin.
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}