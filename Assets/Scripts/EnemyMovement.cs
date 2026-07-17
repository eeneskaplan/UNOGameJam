using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float detectionRadius = 6f; // Düþmanýn oyuncuyu fark etme mesafesi
    private Transform player;

    void Start()
    {
        // Sahnedeki "Player" etiketine sahip objeyi bul
        GameObject target = GameObject.FindGameObjectWithTag("Player");
        if (target != null)
        {
            player = target.transform;
        }
    }

    void Update()
    {
        if (player != null)
        {
            // 1. Düþman ile oyuncu arasýndaki mesafeyi hesapla
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            // 2. Eðer oyuncu, düþmanýn görüþ menziline (detectionRadius) girdiyse harekete geç
            if (distanceToPlayer <= detectionRadius)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
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