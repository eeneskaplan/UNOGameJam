using UnityEngine;

public class Kapi : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Kapýya çarpan obje "Player" etiketine sahipse çalýþýr
        if (other.CompareTag("Player"))
        {
            // LevelManager'ý bulur ve sýradaki odayý yükleme komutunu verir
            FindObjectOfType<LevelManager>().SonrakiOdayaGec();
        }
    }
}