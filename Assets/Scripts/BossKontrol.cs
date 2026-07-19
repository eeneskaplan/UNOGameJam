using UnityEngine;
using System.Collections;

public class BossKontrol : MonoBehaviour
{
    [Header("Hedef ve Referanslar")]
    public Transform oyuncu;
    public Transform atisNoktasi;
    private Animator anim;

    [Header("M�himmatlar")]
    public GameObject mermiPrefab;
    public GameObject anlikLazerPrefab;      // �leri uzanan uzun bir sprite (Collider'� kapal� ba�las�n)
  

    void Start()
    {
        anim = GetComponent<Animator>();

        // E�er sahnede oyuncu varsa bul
        if (oyuncu == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) oyuncu = p.transform;
        }

        
        StartCoroutine(BossSavasDongusu());
    }

    IEnumerator BossSavasDongusu()
    {
        // Boss ya�ad��� s�rece bu d�ng� s�rayla devam edecek
        while (true)
        {
            yield return StartCoroutine(Mod1_IleriRandom());
            yield return new WaitForSeconds(1f); // Sald�r�lar aras� nefes alma pay�

            yield return StartCoroutine(Mod2_AnlikLazer());
            yield return new WaitForSeconds(0.7f);

            yield return StartCoroutine(Mod3_CevreselDalga());
            yield return new WaitForSeconds(1f);

           
        }
    }

    // MOD 1: Oyuncuya Doğru Geniş Taramalı Ateş (Mermi sayısı arttı, daha çok dağılıyor)
    IEnumerator Mod1_IleriRandom()
    {
        anim.SetTrigger("Atak1");
        int atisSayisi = 50; // 15'ten 35'e çıkardık (Çok daha fazla ateş edecek)

        for (int i = 0; i < atisSayisi; i++)
        {
            if (oyuncu == null || atisNoktasi == null) yield break;

            Vector2 yonFarki = oyuncu.position - atisNoktasi.position;
            if (yonFarki != Vector2.zero)
            {
                Vector2 temelYon = yonFarki.normalized;
                // Sapma açısını -15/+15 yerine -45/+45 yaptık ki tek çizgi olmasın, geniş bir alana saçılsın
                float sapmaAcisi = Random.Range(-45f, 45f);
                Vector2 sapmisYon = Quaternion.Euler(0, 0, sapmaAcisi) * temelYon;

                float aci = Mathf.Atan2(sapmisYon.y, sapmisYon.x) * Mathf.Rad2Deg;
                Instantiate(mermiPrefab, atisNoktasi.position, Quaternion.Euler(0, 0, aci));
            }
            yield return new WaitForSeconds(0.05f); // 0.1'den 0.05'e çektik (Taramalı gibi çok hızlı atacak)
        }
    }

    // MOD 2: Uzun Süreli Düz Lazer
    // MOD 2: Uzun Süreli Düz Lazer
    // MOD 2: Sağa Sola Tarayan Ölüm Lazeri
    // MOD 2: Sağa Sola Tarayan Kusursuz Ölüm Lazeri
    // MOD 2: 360 Derece Kesintisiz Dönen Ölüm Lazeri
    IEnumerator Mod2_AnlikLazer()
    {
        anim.SetTrigger("Atak2");

        if (oyuncu == null || atisNoktasi == null) yield break;

        // Lazeri oyuncuya doğru nişan alarak başlatıyoruz
        Vector2 yon = oyuncu.position - atisNoktasi.position;
        float baslangicAcisi = Mathf.Atan2(yon.y, yon.x) * Mathf.Rad2Deg;

        GameObject lazer = Instantiate(anlikLazerPrefab, atisNoktasi.position, Quaternion.Euler(0, 0, baslangicAcisi));

        yield return new WaitForSeconds(0.7f); // Oyuncuya kaçması için verilen uyarı süresi

        if (lazer != null)
        {
            lazer.GetComponentInChildren<BoxCollider2D>().enabled = true;
            lazer.GetComponentInChildren<SpriteRenderer>().color = Color.red;
        }

        float sure = 4.5f; // Lazerin ekranda kalma süresi
        float gecenZaman = 0f;

        // Lazerin saniyede kaç derece döneceği
        // (Örneğin 100 yaparsan 3.6 saniyede tam 360 dereceyi tamamlar)
        float donmeHizi = 60f;

        while (gecenZaman < sure)
        {
            if (lazer == null || atisNoktasi == null) yield break;

            gecenZaman += Time.deltaTime;
            lazer.transform.position = atisNoktasi.position;

            // Zamanla açıyı sürekli artırarak tam tur atmasını sağlıyoruz.
            // NOT: Eğer lazerin saat yönünde dönmesini istersen aradaki "+" işaretini "-" yap.
            float guncelAci = baslangicAcisi + (gecenZaman * donmeHizi);

            lazer.transform.rotation = Quaternion.Euler(0, 0, guncelAci);

            yield return null;
        }

        if (lazer != null) Destroy(lazer);
    }

    // MOD 3: Sabit Açılı Pompalı/Yelpaze Atışı (10-20-30 ve 15-25-35)
    // MOD 3: Oyuncuya Doğru Şaşırtmacalı Yelpaze (Stream) Atışı
    // MOD 3: 360 Derece Alternatif Boşluklu Patlama (Çizimdeki Kırmızı/Mavi Dalgalar)
    IEnumerator Mod3_CevreselDalga()
    {
        anim.SetTrigger("Atak1");

        int waveSayisi = 12; // Toplamda kaç kere patlama yapacağı

        for (int w = 0; w < waveSayisi; w++)
        {
            if (oyuncu == null || atisNoktasi == null) yield break;

            // Dalga çift mi tek mi? (Çizimindeki Kırmızı dalga mı, Mavi dalga mı?)
            bool ciftWave = (w % 2 == 0);

            // Çift dalgalar 0'dan başlar (0, 10, 20...), tek dalgalar 5 derece kayar (5, 15, 25...)
            float baslangicAcisi = ciftWave ? 0f : 5f;

            // 360 dereceyi 10'ar derece arayla doldurmak için 36 mermi gerekir
            int mermiSayisi = 36;

            for (int i = 0; i < mermiSayisi; i++)
            {
                // i = 0, 1, 2... oldukça açı 0-10-20 veya 5-15-25 diye tam 360'a kadar dönecek
                float aci = baslangicAcisi + (i * 10f);

                Instantiate(mermiPrefab, atisNoktasi.position, Quaternion.Euler(0, 0, aci));
            }

            // Dalgalar arası bekleme süresi. Çizimindeki o "yuvarlak" katmanlar bu sayede oluşur.
            // Bu satır mermi yaratma (for) döngüsünün KESİNLİKLE dışında olmalı.
            yield return new WaitForSeconds(0.4f);
        }
    }
   
}