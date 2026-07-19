using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 3f;
    public int damage = 25;

    // YENÝ: Çift hasar bug'ýný önleyen mühür!
    private bool carptiMi = false;

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.right * speed;
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // Eðer mermi zaten bir þeye çarptýysa (ilk hasarýný verdiyse) kodun devamýný okuma!
        if (carptiMi) return;

        if (gameObject.CompareTag("PlayerBullet") && hitInfo.CompareTag("Enemy"))
        {
            carptiMi = true; // Mührü vurduk, artýk ikinci Collider'a deðse de buraya girmeyecek

            Health dusmanCan = hitInfo.GetComponent<Health>();
            if (dusmanCan != null)
            {
                dusmanCan.TakeDamage(damage);

                // BUZ ELEMENTÝ (1) YAVAÞLATMA EFEKTÝ
                if (PlayerPrefs.HasKey("IlkElement") && PlayerPrefs.GetInt("IlkElement") == 1)
                {
                    dusmanCan.StartCoroutine(YavaslatmaEfekti(hitInfo.gameObject));
                }
            }

            Destroy(gameObject);
        }
        else if (gameObject.CompareTag("EnemyBullet") && hitInfo.CompareTag("Player"))
        {
            carptiMi = true; // Düþman mermisi için de mühür
            hitInfo.GetComponent<Health>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (hitInfo.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }

    // Düþmanýn hýzýný 2 saniyeliðine kýran sistem (Health üzerinden çaðrýlýr)
    private IEnumerator YavaslatmaEfekti(GameObject dusman)
    {
        DusmanDash tip1 = dusman.GetComponent<DusmanDash>();
        DusmanKamikaze tip2 = dusman.GetComponent<DusmanKamikaze>();
        EnemyMovement tip3 = dusman.GetComponent<EnemyMovement>();

        if (tip1 != null) tip1.normalHiz *= 0.6f;
        if (tip2 != null) tip2.hareketHizi *= 0.6f;
        if (tip3 != null) tip3.moveSpeed *= 0.6f;

        yield return new WaitForSeconds(2f);

        if (dusman != null)
        {
            if (tip1 != null) tip1.normalHiz /= 0.6f;
            if (tip2 != null) tip2.hareketHizi /= 0.6f;
            if (tip3 != null) tip3.moveSpeed /= 0.6f;
        }
    }
}