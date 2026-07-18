using System.Collections;
using UnityEngine;

public class DebuffManager : MonoBehaviour
{
    [Header("Bar Ayarlarý")]
    public float debuffBar = 0f;
    public float maxDebuffBar = 100f;
    public bool isDebuffActive = false;
    public float debuffSuresi = 5f; // Ceza kaç saniye sürecek?

    [Header("Artýþ Miktarlarý")]
    public float basicAttackArtisi = 5f;  // Sol týk barý ne kadar doldurur
    public float skillArtisi = 25f;       // C tuþu yeteneði barý ne kadar doldurur

    // Karakterin özelliklerini kýsmak için diðer scriptlere referanslar
    private PlayerMovement playerMovement;
    private PlayerAttack playerAttack;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerAttack = GetComponent<PlayerAttack>();
    }

    // Karakter her saldýrdýðýnda diðer scriptlerden bu fonksiyonu çaðýracaðýz
    public void AddToBar(bool isSkill)
    {
        // Eðer zaten debuff yiyorsa bar daha fazla dolmasýn (ceza üstüne ceza binmesin)
        if (isDebuffActive) return;

        if (isSkill)
            debuffBar += skillArtisi;
        else
            debuffBar += basicAttackArtisi;

        // Bar tamamen doldu mu kontrolü
        if (debuffBar >= maxDebuffBar)
        {
            debuffBar = maxDebuffBar;
            StartCoroutine(DebuffTetikle());
        }
    }

    private IEnumerator DebuffTetikle()
    {
        isDebuffActive = true;
        Debug.Log("DEBUFF BAÞLADI!");

        // --- BURADA HANGÝ ELEMENT SEÇÝLÝYSE ONUN CEZASI KESÝLECEK ---
        // Þimdilik örnek olarak "Duman" elementinin cezasýný (Hýz yavaþlamasý) yapalým:

        float orijinalHiz = playerMovement.moveSpeed;
        playerMovement.moveSpeed = orijinalHiz * 0.5f; // Hýzý yarýya düþür (aðýrlaþsýn)

        // Belirlenen süre boyunca (Örn: 5 saniye) cezalý bekle
        yield return new WaitForSeconds(debuffSuresi);

        // --- SÜRE BÝTTÝ: CEZAYI KALDIR VE BARI SIFIRLA ---
        playerMovement.moveSpeed = orijinalHiz;
        debuffBar = 0f;
        isDebuffActive = false;

        Debug.Log("DEBUFF BÝTTÝ, BAR SIFIRLANDI!");
    }
}