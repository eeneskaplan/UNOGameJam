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
        // Eðer mermi "Player" etiketiyle ateþlendiyse ve çarptýðý þey "Enemy" ise:
        if (gameObject.CompareTag("PlayerBullet") && hitInfo.CompareTag("Enemy"))
        {
            hitInfo.GetComponent<Health>().TakeDamage(damage);
            Destroy(gameObject);
        }
        // Eðer mermi "EnemyBullet" etiketiyle ateþlendiyse ve çarptýðý þey "Player" ise:
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
}