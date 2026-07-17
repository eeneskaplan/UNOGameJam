using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 3f; // 3 saniye sonra yok olsun
    public int damage = 25;
    void Start()
    {
        // Mermi doðduðu anda ileri doðru uçsun
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        
        
        rb.linearVelocity = transform.right * speed;
        
        // Hafýzayý þiþirmemek için mermiyi bir süre sonra yok et
        Destroy(gameObject, lifetime);
    }

    // Düþmana veya duvara çarpýnca ne olacak?
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // Çarptýðýmýz obje Player ise kodu kes
        if (hitInfo.CompareTag("Player"))
        {
            return;
        }

        // Çarptýðýmýz objede Health scripti var mý diye kontrol et
        Health targetHealth = hitInfo.GetComponent<Health>();

        // Eðer varsa ona 25 hasar ver
        if (targetHealth != null)
        {
            targetHealth.TakeDamage(damage);
        }

        // Hasar versin veya vermesin, duvara/düþmana çarptýðý için mermiyi yok et
        Destroy(gameObject);
    }
}