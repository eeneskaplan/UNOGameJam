using System.Collections.Generic;
using UnityEngine;

public class ElektrikMermisi : MonoBehaviour
{
    [Header("Elektrik Ayarlarý")]
    public float hiz = 15f;
    public int hasar = 30;
    public int maxSekmeSayisi = 3;
    public float sekmeMenzili = 6f;

    private int mevcutSekme = 0;
    private HashSet<GameObject> vurulanDusmanlar = new HashSet<GameObject>();

    void Start()
    {
        Destroy(gameObject, 4f); // 4 saniye sonra her halükarda silinir (güvenlik)
    }

    void Update()
    {
        transform.Translate(Vector3.right * hiz * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Çarpýlan obje oyuncuysa KESÝNLÝKLE hiçbir þey yapma ve es geç
        if (collision.CompareTag("Player")) return;

        Health targetHealth = collision.GetComponent<Health>();

        if (targetHealth != null)
        {
            if (vurulanDusmanlar.Contains(collision.gameObject)) return;

            targetHealth.TakeDamage(hasar);
            vurulanDusmanlar.Add(collision.gameObject);
            mevcutSekme++;

            if (mevcutSekme >= maxSekmeSayisi)
            {
                Destroy(gameObject);
            }
            else
            {
                Sektir();
            }
        }
    }

    void Sektir()
    {
        Collider2D[] etraftakiler = Physics2D.OverlapCircleAll(transform.position, sekmeMenzili);
        Transform enYakinDusman = null;
        float enKisaMesafe = Mathf.Infinity;

        foreach (Collider2D col in etraftakiler)
        {
            // OYUNCUYA SEKMEYÝ ENGELLEYEN KRÝTÝK KONTROL BURASI
            if (col.CompareTag("Player")) continue;

            Health health = col.GetComponent<Health>();

            if (health != null && !vurulanDusmanlar.Contains(col.gameObject))
            {
                float mesafe = Vector2.Distance(transform.position, col.transform.position);
                if (mesafe < enKisaMesafe)
                {
                    enKisaMesafe = mesafe;
                    enYakinDusman = col.transform;
                }
            }
        }

        // Etrafta sekecek (daha önce vurulmamýþ) bir düþman varsa ona dön
        if (enYakinDusman != null)
        {
            Vector2 lookDirection = enYakinDusman.position - transform.position;
            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            // Etrafta düþman yoksa mermiyi anýnda yok et
            Destroy(gameObject);
        }
    }
}