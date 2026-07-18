using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ElementSecim : MonoBehaviour
{
    [Header("UI Baðlantýlarý")]
    public Button solKartBtn;
    public Button sagKartBtn;

    // Artýk Text yerine butonlarýn üzerindeki Image bileþenini alacaðýz
    public Image solKartImaj;
    public Image sagKartImaj;

    [Header("Kart Görselleri (Sprite'larý Buraya Sürükle)")]
    public Sprite atesKarti;
    public Sprite buzKarti;
    public Sprite dumanKarti;
    public Sprite elektrikKarti;

    [Header("Ayarlar")]
    public string asilOyunSahnesiAdi = "SecondScene";

    private ElementTuru solElement;
    private ElementTuru sagElement;

    void Start()
    {
        List<ElementTuru> tumElementler = new List<ElementTuru>
        {
            ElementTuru.Ates,
            ElementTuru.Buz,
            ElementTuru.Duman,
            ElementTuru.Elektrik
        };

        // 1. KART
        int rastgele1 = Random.Range(0, tumElementler.Count);
        solElement = tumElementler[rastgele1];
        tumElementler.RemoveAt(rastgele1);

        // 2. KART
        int rastgele2 = Random.Range(0, tumElementler.Count);
        sagElement = tumElementler[rastgele2];

        // Rastgele seçilen elementlere göre doðru Sprite'ý butonun resmine ata
        solKartImaj.sprite = GorselGetir(solElement);
        sagKartImaj.sprite = GorselGetir(sagElement);

        solKartBtn.onClick.AddListener(() => ElementiKaydetVeBasla(solElement));
        sagKartBtn.onClick.AddListener(() => ElementiKaydetVeBasla(sagElement));
    }

    // Secilen elemente bakýp senin hazýrladýðýn o güzel kart görselini döndüren yardýmcý fonksiyon
    Sprite GorselGetir(ElementTuru element)
    {
        switch (element)
        {
            case ElementTuru.Ates: return atesKarti;
            case ElementTuru.Buz: return buzKarti;
            case ElementTuru.Duman: return dumanKarti;
            case ElementTuru.Elektrik: return elektrikKarti;
            default: return null;
        }
    }

    void ElementiKaydetVeBasla(ElementTuru secilenElement)
    {
        PlayerPrefs.SetInt("IlkElement", (int)secilenElement);
        PlayerPrefs.Save();
        SceneManager.LoadScene(asilOyunSahnesiAdi);
    }
}