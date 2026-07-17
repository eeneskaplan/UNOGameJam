using UnityEngine;

// Bedel türlerini bir listeye alýyoruz. Ýhtiyacýn oldukça buraya yeni bedeller ekleyebilirsin.
public enum BedelTuru
{
    MaksimumCanAzalmasi,
    HareketHiziDususu,
    GorusMesafesiKaranlik,
    DashIptal,
    KanamaGibiZamanlaHasar
}

[CreateAssetMenu(fileName = "Yeni Element", menuName = "Oyun/Element Kartý")]
public class ElementData : ScriptableObject
{
    [Header("Temel Bilgiler")]
    public string elementName;
    public Sprite elementIcon;

    [Header("Yetenek (Güç)")]
    // Sabit mermi yerine, o elementin özel yetenek prefabýný buraya koyacaðýz.
    // Ateþ topuysa ateþ topu prefabý, Dash kýlýcýysa dash kýlýcý prefabý.
    public GameObject yetenekPrefab;
    public float beklemeSuresi;

    [Header("Bedel (Debuff)")]
    public BedelTuru kesilecekBedel; // Editörden açýlýr liste olarak seçeceðiz
    public float bedelMiktari; // O bedelin þiddeti (Örn: Hýz düþüþüyse 0.5f, can ise 20)
}