using UnityEngine;

public class BuzMermisi : MonoBehaviour
{
    public float hiz = 12f;
    public int hasar = 40;
    public float yasamSuresi = 3f; // Ekranda sonsuza kadar uçmasýn diye

    void Start()
    {
        // 3 saniye sonra bir þeye çarpmazsa kendini yok et
        Destroy(gameObject, yasamSuresi);
    }

    void Update()
    {
        // Standart top-down 2D mantýðýnda transform.right mermiyi baktýðý yöne uçurur
        transform.Translate(Vector3.right * hiz * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) return;

        Health targetHealth = collision.GetComponent<Health>();

        if (targetHealth != null)
        {
            targetHealth.TakeDamage(hasar);
            // Burada istersen düþmanýn hýzýný yavaþlatan bir kod da çaðýrabilirsin
            Destroy(gameObject); // Çarptýðý an yok ol
        }
    }
}