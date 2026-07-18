using System.Collections;
using UnityEngine;
using UnityEngine.UI; // YENÝ: UI kütüphanesini ekledik

public enum ElementTuru { Ates, Buz, Duman, Elektrik }

public class DebuffManager : MonoBehaviour
{
    [Header("Aktif Element Ayarý")]
    public ElementTuru aktifDebuff;

    [Header("Bar Ayarlarý")]
    public float debuffBar = 0f;
    public float maxDebuffBar = 100f;
    public bool isDebuffActive = false;
    public float debuffSuresi = 5f;

    [Header("Artýþ Miktarlarý")]
    public float basicAttackArtisi = 5f;
    public float skillArtisi = 25f;

    [Header("UI Baðlantýlarý")]
    public Image debuffBarFill; // YENÝ: Sarý barýmýzýn görsel referansý

    private PlayerMovement playerMovement;
    private PlayerAttack playerAttack;
    private Health playerHealth;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerAttack = GetComponent<PlayerAttack>();
        playerHealth = GetComponent<Health>();

        // Oyun baþýnda UI barýný sýfýrla
        if (debuffBarFill != null)
        {
            debuffBarFill.fillAmount = 0f;
        }
        // OYUN BAÞINDA SEÇÝLEN ELEMENTÝ OKU VE DEBUFF'I AYARLA
        if (PlayerPrefs.HasKey("IlkElement"))
        {
            aktifDebuff = (ElementTuru)PlayerPrefs.GetInt("IlkElement");
            Debug.Log("Debuff Sistemi Aktif Edildi. Geçerli Bedel: " + aktifDebuff.ToString());
        }
    }

    public void AddToBar(bool isSkill)
    {
        if (isDebuffActive) return;

        debuffBar += isSkill ? skillArtisi : basicAttackArtisi;

        // YENÝ: Deðeri 0 ile 1 arasýna oranlayýp UI'a yansýt
        if (debuffBarFill != null)
        {
            debuffBarFill.fillAmount = debuffBar / maxDebuffBar;
        }

        if (debuffBar >= maxDebuffBar)
        {
            debuffBar = maxDebuffBar;
            StartCoroutine(DebuffTetikle());
        }
    }

    private IEnumerator DebuffTetikle()
    {
        isDebuffActive = true;
        Debug.Log(aktifDebuff.ToString() + " DEBUFF'I BAÞLADI!");

        // --- DEBUFF BAÞLANGIÇ DEÐERLERÝNÝ HAFIZAYA AL ---
        float orijinalHiz = playerMovement.moveSpeed;
        float orijinalFireRate = playerAttack.fireRate;
        int orijinalHasar = playerAttack.mermiHasari;

        // --- HANGÝ ELEMENT SEÇÝLÝYSE ONUN CEZASINI UYGULA ---
        switch (aktifDebuff)
        {
            case ElementTuru.Ates:
                playerAttack.fireRate = orijinalFireRate * 1.75f; // Daha yavaþ ateþ 
                break;

            case ElementTuru.Buz:
                playerHealth.alinanHasarCarpani = 1.25f; // %50 daha fazla hasar yer
                break;

            case ElementTuru.Duman:
                playerMovement.moveSpeed = orijinalHiz * 0.75f; // Yarý hýzda yürür
                break;

            case ElementTuru.Elektrik:
                playerAttack.mermiHasari = Mathf.RoundToInt(orijinalHasar * 0.75f); // Hasarý yarýya düþer
                break;
        }

        yield return new WaitForSeconds(debuffSuresi);

        CezalariKaldir(orijinalHiz, orijinalFireRate, orijinalHasar);

        debuffBar = 0f;
        isDebuffActive = false;

        // YENÝ: Ceza bittiðinde UI barýný sýfýrla
        if (debuffBarFill != null)
        {
            debuffBarFill.fillAmount = 0f;
        }

        Debug.Log("DEBUFF BÝTTÝ, NORMALE DÖNÜLDÜ!");
    }

    public void YeniDebuffAta(ElementTuru yeniElement)
    {
        if (isDebuffActive)
        {
            StopAllCoroutines();
            CezalariKaldir(5f, 0.5f, 25); // Varsayýlan deðerlerle sýfýrla
        }

        aktifDebuff = yeniElement;
        debuffBar = 0f;
        isDebuffActive = false;

        // YENÝ: Yeni element seçildiðinde UI barýný sýfýrla
        if (debuffBarFill != null)
        {
            debuffBarFill.fillAmount = 0f;
        }

        Debug.Log("Eski bedel silindi! Yeni bedel atandý: " + aktifDebuff.ToString());
    }

    private void CezalariKaldir(float hiz, float atisHizi, int hasar)
    {
        playerMovement.moveSpeed = hiz;
        playerAttack.fireRate = atisHizi;
        playerAttack.mermiHasari = hasar;
        playerHealth.alinanHasarCarpani = 1f; // Çarpaný her zaman 1'e (normale) eþitle

        Debug.Log("Karakterin statlarý normale döndü.");
    }
}