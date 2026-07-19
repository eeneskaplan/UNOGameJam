using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("UI Baðlantýlarý")]
    public GameObject pauseMenusu; // Görünür/Görünmez yapacaðýmýz siyah panel

    private bool isPaused = false;

    void Start()
    {
        // Oyun baþýnda menünün kapalý olduðundan ve zamanýn aktýðýndan emin olalým
        pauseMenusu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Update()
    {
        // Oyuncu ESC tuþuna basarsa menüyü aç/kapat
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                DevamEt();
            }
            else
            {
                OyunuDurdur();
            }
        }
    }

    public void OyunuDurdur()
    {
        pauseMenusu.SetActive(true); // Menüyü göster
        Time.timeScale = 0f; // DÝKKAT: Zamaný tamamen durdurur (Oyun donar)
        isPaused = true;
    }

    // --- BUTONLARA BAÐLAYACAÐIMIZ FONKSÝYONLAR ---

    // 1. DEVAM ET BUTONU
    public void DevamEt()
    {
        pauseMenusu.SetActive(false); // Menüyü gizle
        Time.timeScale = 1f; // Zamaný tekrar baþlat
        isPaused = false;
    }

    // 2. YENÝDEN BAÞLA BUTONU
    public void YenidenBasla()
    {
        Time.timeScale = 1f; // Sahne yüklenmeden önce zamaný KESÝNLÝKLE düzeltmeliyiz, yoksa yeni sahne donuk açýlýr!

        // Þu anki aktif sahnenin adýný al ve onu tekrar yükle
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // 3. ANA MENÜ BUTONU
    public void AnaMenuyeDon()
    {
        Time.timeScale = 1f; // Yine zamaný düzeltiyoruz

        // Ana menü sahnesinin adýný tam olarak buraya yazmalýsýn (Örn: "MainMenu")
        SceneManager.LoadScene("MainMenu");
    }
}