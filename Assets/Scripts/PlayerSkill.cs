using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    [Header("Q Yeteneði (1. Element)")]
    public bool qYetenegiAcik = false; // Odayý temizleyince true yapacaðýz
    public ElementTuru qElementi;
    public float qCooldownSuresi = 3f;
    private float qSonKullanimZamani = -100f;

    [Header("E Yeteneði (Boss Sonrasý 2. Element)")]
    public bool eYetenegiAcik = false; // Bossu kesince true yapacaðýz
    public ElementTuru eElementi;
    public float eCooldownSuresi = 5f;
    private float eSonKullanimZamani = -100f;

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
        // SEÇÝM EKRANINDAN GELEN VERÝYÝ OKU VE Q'YA ATA
        if (PlayerPrefs.HasKey("IlkElement"))
        {
            ElementTuru kaydedilenElement = (ElementTuru)PlayerPrefs.GetInt("IlkElement");
            QYeteneginiAktifEt(kaydedilenElement); // Zaten bu fonksiyonu yazmýþtýk!
        }
    }

    void Update()
    {
        // --- Q YETENEÐÝ TETÝKLEYÝCÝSÝ ---
        if (qYetenegiAcik && Input.GetKeyDown(KeyCode.Q) && Time.time >= qSonKullanimZamani + qCooldownSuresi)
        {
            debuffManager.AddToBar(true);
            YetenekAtesle(qElementi);
            qSonKullanimZamani = Time.time;
        }

        // --- E YETENEÐÝ TETÝKLEYÝCÝSÝ ---
        if (eYetenegiAcik && Input.GetKeyDown(KeyCode.E) && Time.time >= eSonKullanimZamani + eCooldownSuresi)
        {
            debuffManager.AddToBar(true);
            YetenekAtesle(eElementi);
            eSonKullanimZamani = Time.time;
        }
    }

    // Hangi tuþa basýldýðýný anlayýp ona göre doðru elementi fýrlatan merkez fonksiyon
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

    // --- DIÞARIDAN ÇAÐRILACAK KÝLÝT AÇMA FONKSÝYONLARI ---

    // Odayý temizleyip ilk elementini seçtiðinde UI butonundan bu çaðrýlacak
    public void QYeteneginiAktifEt(ElementTuru secilenElement)
    {
        qElementi = secilenElement;
        qYetenegiAcik = true;
        Debug.Log("Q Yeteneði açýldý! Element: " + secilenElement.ToString());
    }

    // Boss'u kesip ikinci elementini seçtiðinde UI butonundan bu çaðrýlacak
    public void EYeteneginiAktifEt(ElementTuru secilenElement)
    {
        eElementi = secilenElement;
        eYetenegiAcik = true;
        Debug.Log("E Yeteneði açýldý! Element: " + secilenElement.ToString());
    }
}