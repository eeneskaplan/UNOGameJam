using UnityEngine;
using System.Collections; // Coroutine için eklendi

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 3f;
    public int damage = 25;

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.right * speed;
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (gameObject.CompareTag("PlayerBullet") && hitInfo.CompareTag("Enemy"))
        {
            // 1. Önce Hasarý Ver
            Health dusmanCan = hitInfo.GetComponent<Health>();
            if (dusmanCan != null)
            {
                dusmanCan.TakeDamage(damage);

                // 2. YENÝ: BUZ ELEMENTÝ (1) KONTROLÜ VE YAVAÞLATMA EFEKTÝ
                if (PlayerPrefs.HasKey("IlkElement") && PlayerPrefs.GetInt("IlkElement") == 1)
                {
                    // Mermi yok olacaðý için yavaþlatma iþlemini düþmanýn üstündeki koda devrediyoruz
                    dusmanCan.StartCoroutine(YavaslatmaEfekti(hitInfo.gameObject));
                }
            }

            Destroy(gameObject);
        }
        else if (gameObject.CompareTag("EnemyBullet") && hitInfo.CompareTag("Player"))
        {
            hitInfo.GetComponent<Health>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (hitInfo.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }

    // YENÝ: Düþmanýn hýzýný 2 saniyeliðine kýran sistem
    private IEnumerator YavaslatmaEfekti(GameObject dusman)
    {
        // Senin yazdýðýn 3 farklý düþman tipinin kodlarýný arýyoruz
        DusmanDash tip1 = dusman.GetComponent<DusmanDash>();
        DusmanKamikaze tip2 = dusman.GetComponent<DusmanKamikaze>();
        EnemyMovement tip3 = dusman.GetComponent<EnemyMovement>();

        // Hangi düþmansa onun hýzýný %40 oranýnda azalt
        if (tip1 != null) tip1.normalHiz *= 0.6f;
        if (tip2 != null) tip2.hareketHizi *= 0.6f;
        if (tip3 != null) tip3.moveSpeed *= 0.6f;

        // 2 Saniye donuk kalsýn
        yield return new WaitForSeconds(2f);

        // 2 saniye sonra düþman hala yaþýyorsa (ölüp yok olmadýysa) hýzýný eski haline getir
        if (dusman != null)
        {
            if (tip1 != null) tip1.normalHiz /= 0.6f;
            if (tip2 != null) tip2.hareketHizi /= 0.6f;
            if (tip3 != null) tip3.moveSpeed /= 0.6f;
        }
    }
}