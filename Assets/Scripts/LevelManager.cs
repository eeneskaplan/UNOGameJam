using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Oda Prefablarý")]
    public GameObject baslangicOdasi;
    public GameObject[] normalOdalar; // 3 farklý normal oda prefabýný buraya atacaksýn
    public GameObject treasureOdasi;
    public GameObject bossOdasi;

    [Header("Oyun Takibi")]
    public int odaSayaci = 0;
    private GameObject mevcutAktifOda;

    [Header("Oyuncu Ayarlarý")]
    public Transform oyuncuSpawnNoktasi; // Her odaya giriþte karakterin ýþýnlanacaðý yer

    void Start()
    {
        // Oyun baþladýðýnda ilk odayý (Spawn) yükle
        OdaYukle();
    }

    public void OdaYukle()
    {
        // Eðer sahnede halihazýrda bir oda varsa, onu tamamen sil
        if (mevcutAktifOda != null)
        {
            Destroy(mevcutAktifOda);
        }

        GameObject yaratilacakOda = null;

        // Ýstediðin Sýralama Mantýðý
        if (odaSayaci == 0)
        {
            yaratilacakOda = baslangicOdasi;
        }
        else if (odaSayaci == 1 || odaSayaci == 2 || odaSayaci == 4)
        {
            // Normal odalardan rastgele birini seç
            int rastgeleIndex = Random.Range(0, normalOdalar.Length);
            yaratilacakOda = normalOdalar[rastgeleIndex];
        }
        else if (odaSayaci == 3)
        {
            yaratilacakOda = treasureOdasi;
        }
        else if (odaSayaci == 5)
        {
            yaratilacakOda = bossOdasi;
        }
        else
        {
            Debug.Log("Boss Kesildi! Oyun Bitti (Kazanma Ekraný Gelecek)");
            return;
        }

        // Seçilen odayý sahnenin tam merkezine (0,0,0) yarat
        mevcutAktifOda = Instantiate(yaratilacakOda, Vector3.zero, Quaternion.identity);

        // Oyuncuyu bul ve yeni odanýn spawn noktasýna ýþýnla
        GameObject oyuncu = GameObject.FindGameObjectWithTag("Player");
        if (oyuncu != null && oyuncuSpawnNoktasi != null)
        {
            oyuncu.transform.position = oyuncuSpawnNoktasi.position;
        }
    }

    // Kapýdan geçildiðinde bu fonksiyon çaðrýlacak
    public void SonrakiOdayaGec()
    {
        odaSayaci++;
        OdaYukle();
    }
}
