using System.Collections;
using UnityEngine;

public class DusmanDash : MonoBehaviour
{
    [Header("Hareket ve Dash Ayarlarý")]
    public float normalHiz = 2f;         // Uzaktayken yürüme hýzý
    public float dashHizi = 15f;         // Atýlma (Dash) hýzý
    public float dashMenzili = 5f;       // Oyuncuya ne kadar yaklaþýnca dash atacaðý
    public float dashHazirlikSuresi = 0.5f; // Atýlmadan önce durup güç toplama süresi
    public float dashBeklemeSuresi = 1.5f;  // Dash attýktan sonraki yorgunluk/cooldown süresi

    private Transform oyuncu;
    private bool dashYapiliyorMu = false;

    void Start()
    {
        // Oyuncuyu sahnede bul (Etiketinin "Player" olduðuna emin ol)
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            oyuncu = playerObj.transform;
        }
    }

    void Update()
    {
        // Eðer oyuncu yoksa veya o an dash iþlemi dönüyorsa normal hareketi iptal et
        if (oyuncu == null || dashYapiliyorMu) return;

        // Oyuncuyla aramýzdaki mesafeyi ölç
        float mesafe = Vector2.Distance(transform.position, oyuncu.position);

        if (mesafe <= dashMenzili)
        {
            // Oyuncu menzile girdiyse Dash rutini baþlat
            StartCoroutine(DashSistemi());
        }
        else
        {
            // Menzilde deðilse normal normal üstüne yürü
            transform.position = Vector2.MoveTowards(transform.position, oyuncu.position, normalHiz * Time.deltaTime);
        }
    }

    IEnumerator DashSistemi()
    {
        dashYapiliyorMu = true; // Baþka hareket girmesin diye kilitliyoruz

        // 1. AÞAMA: HAZIRLIK (Dur ve oyuncunun o anki yerini hedef al)
        // Burada istersen düþmanýn rengini beyaz yapýp oyuncuyu uyarabilirsin
        Vector2 dashYonu = (oyuncu.position - transform.position).normalized;
        yield return new WaitForSeconds(dashHazirlikSuresi);

        // 2. AÞAMA: DASH ATMA (Kýsa bir süre boyunca o yöne çok hýzlý git)
        float dashSuresi = 0.2f; // Dash'in havada kalma süresi (çok kýsa olmalý)
        float gecenSure = 0f;

        while (gecenSure < dashSuresi)
        {
            // transform.position üzerinden direkt atýlma
            transform.position += (Vector3)(dashYonu * dashHizi * Time.deltaTime);
            gecenSure += Time.deltaTime;
            yield return null; // Bir sonraki kareyi (frame) bekle
        }

        // 3. AÞAMA: YORGUNLUK / BEKLEME SÜRESÝ
        // Dash bitti, tekrar saldýrmadan önce biraz dinlensin
        yield return new WaitForSeconds(dashBeklemeSuresi);

        dashYapiliyorMu = false; // Kilidi aç, döngü baþa dönsün
    }
}