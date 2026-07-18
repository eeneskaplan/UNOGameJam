using UnityEngine;

public class AreaDamage : MonoBehaviour
{
    [Header("Hasar Ayarlarý")]
    public int yetenekHasari = 50; // Ateþ/Duman patladýðýnda ne kadar vuracak?

    // Objenin Is Trigger açýk olan Collider'ýna biri deðdiðinde/içinde doðduðunda çalýþýr
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Eðer patlama oyuncunun (senin) üstüne geldiyse, kendine hasar verme
        if (collision.CompareTag("Player"))
        {
            return;
        }

        // Çarptýðýmýz objede Health scripti var mý diye bak (Yani bu bir düþman mý?)
        Health targetHealth = collision.GetComponent<Health>();

        if (targetHealth != null)
        {
            // Düþmanýn canýný yak!
            targetHealth.TakeDamage(yetenekHasari);
            Debug.Log("Alan yeteneði " + collision.gameObject.name + " objesine " + yetenekHasari + " hasar verdi!");
        }
    }
}