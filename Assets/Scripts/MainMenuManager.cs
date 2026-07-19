using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Oyna butonuna basýldýðýnda çalýþacak fonksiyon
    public void OyunaBasla()
    {
        // Burada Element Seçim ekranýnýn tam adýný yazmalýsýn
        // (Eðer seçim ekranýnýn adý farklýysa týrnak içindeki yazýyý ona göre düzelt)
        SceneManager.LoadScene("IlkElementSec");
    }

    
}