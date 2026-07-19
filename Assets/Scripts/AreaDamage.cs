using System.Collections.Generic; // List kullanabilmek için bunu eklemeliyiz
using UnityEngine;

public class AreaDamage : MonoBehaviour
{
    [Header("Hasar Ayarlarý")]
    public int yetenekHasari = 50; // Ateþ/Duman patladýðýnda ne kadar vuracak?

    // ZATEN HASAR VERDÝÐÝMÝZ DÜÞMANLARI TUTACAÐIMIZ LÝSTE
    private List<Health> hasarAlanlar = new List<Health>();

    // Objenin Is Trigger açýk olan Collider'ýna biri deðdiðinde/içinde doðduðunda çalýþýr
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Eðer patlama oyuncunun (senin) üstüne geldiyse, kendine hasar verme
        if (collision.CompareTag("Player"))
        {
            return;
        }

        // Çarptýðýmýz objede Health scripti var mý diye bak
        Health targetHealth = collision.GetComponent<Health>();

        // EÐER objede Health varsa VE bu düþman daha önce bu patlamadan hasar ALMADIYSA
        if (targetHealth != null && !hasarAlanlar.Contains(targetHealth))
        {
            // Düþmanýn canýný yak!
            targetHealth.TakeDamage(yetenekHasari);

            // Düþmaný "Hasar Alanlar" listesine ekle ki diðer Collider'larýna çarptýðýnda tekrar vurmasýn
            hasarAlanlar.Add(targetHealth);

            Debug.Log("Alan yeteneði " + collision.gameObject.name + " objesine " + yetenekHasari + " hasar verdi!");
        }
    }
}