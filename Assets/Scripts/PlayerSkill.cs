using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    [Header("Yetenek (Tek Element - Q Tuþu)")]
    public bool yetenekAcik = false;
    public ElementTuru aktifElement;
    public float cooldownSuresi = 3f;
    private float sonKullanimZamani = -100f;

    [Header("Yetenek Ayarlarý")]
    public Transform firePoint;

    [Header("Yetenek Prefablarý")]
    public GameObject atesAlaniPrefab;
    public GameObject dumanAlaniPrefab;
    public GameObject buzMermisiPrefab;
    public GameObject elektrikMermisiPrefab;

    private DebuffManager debuffManager;

    void Start()
    {
        debuffManager = GetComponent<DebuffManager>();

        // OYUN BAÞINDA SEÇÝLEN TEK ELEMENTÝ OKU VE YETENEÐÝ AKTÝF ET
        if (PlayerPrefs.HasKey("IlkElement"))
        {
            ElementTuru kaydedilenElement = (ElementTuru)PlayerPrefs.GetInt("IlkElement");
            YetenegiAktifEt(kaydedilenElement);
        }
    }

    void Update()
    {
        // --- TEK YETENEK (Q) TETÝKLEYÝCÝSÝ ---
        if (yetenekAcik && Input.GetKeyDown(KeyCode.Q) && Time.time >= sonKullanimZamani + cooldownSuresi)
        {
            debuffManager.AddToBar(true); // Skill kullanýldýðý için bara daha çok ekle
            YetenekAtesle(aktifElement);
            sonKullanimZamani = Time.time;
        }
    }

    // Seçilen elementi fýrlatan/kullanan merkez fonksiyon
    void YetenekAtesle(ElementTuru kullanilanElement)
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        Vector2 lookDirection = mousePosition - firePoint.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        Quaternion yonluRotasyon = Quaternion.Euler(0, 0, angle);

        switch (kullanilanElement)
        {
            case ElementTuru.Ates:
                if (atesAlaniPrefab != null) Instantiate(atesAlaniPrefab, mousePosition, Quaternion.identity);
                break;
            case ElementTuru.Duman:
                if (dumanAlaniPrefab != null) Instantiate(dumanAlaniPrefab, mousePosition, Quaternion.identity);
                break;
            case ElementTuru.Buz:
                if (buzMermisiPrefab != null) Instantiate(buzMermisiPrefab, firePoint.position, yonluRotasyon);
                break;
            case ElementTuru.Elektrik:
                if (elektrikMermisiPrefab != null) Instantiate(elektrikMermisiPrefab, firePoint.position, yonluRotasyon);
                break;
        }
    }

    // Oyun baþýnda bu fonksiyon çaðrýlýp yeteneðin kilidini açar
    public void YetenegiAktifEt(ElementTuru secilenElement)
    {
        aktifElement = secilenElement;
        yetenekAcik = true;
        Debug.Log("Yetenek açýldý! Element: " + secilenElement.ToString());
    }
}