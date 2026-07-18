using UnityEngine;

public class kapi_cikis : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Çarpan obje "Player" etiketine sahipse içeri girer
        if (other.CompareTag("Player"))
        {
            // Yeni Unity sürümlerinde sarý uyarý vermemesi için FindFirstObjectByType kullanýyoruz
            LevelManager manager = Object.FindFirstObjectByType<LevelManager>();

            if (manager != null)
            {
                // LevelManager bulunduysa sonraki odaya geçiþi tetikle
                manager.SonrakiOdayaGec();
            }
            else
            {
                // Eðer sahnede LevelManager yoksa konsola hata yazdýr (olasý buglarý yakalamak için)
                Debug.LogError("Sahne içinde LevelManager bulunamadý!");
            }
        }
    }
}