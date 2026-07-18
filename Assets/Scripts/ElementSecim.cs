using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ElementSecim : MonoBehaviour
{
    [Header("UI Buton Baðlantýlarý")]
    public Button atesButonu;
    public Button buzButonu;
    public Button dumanButonu;
    public Button elektrikButonu;

    [Header("Ayarlar")]
    public string asilOyunSahnesiAdi = "SecondScene";

    void Start()
    {
        // 4 butona da týklandýðýnda kendi elementlerini kaydedip oyunu baþlatma emrini veriyoruz
        atesButonu.onClick.AddListener(() => ElementiKaydetVeBasla(ElementTuru.Ates));
        buzButonu.onClick.AddListener(() => ElementiKaydetVeBasla(ElementTuru.Buz));
        dumanButonu.onClick.AddListener(() => ElementiKaydetVeBasla(ElementTuru.Duman));
        elektrikButonu.onClick.AddListener(() => ElementiKaydetVeBasla(ElementTuru.Elektrik));
    }

    void ElementiKaydetVeBasla(ElementTuru secilenElement)
    {
        // Hangi butona týklandýysa o elementi PlayerPrefs'e kaydet
        PlayerPrefs.SetInt("IlkElement", (int)secilenElement);
        PlayerPrefs.Save();

        // Asýl oyun sahnesini yükle
        SceneManager.LoadScene(asilOyunSahnesiAdi);
    }
}