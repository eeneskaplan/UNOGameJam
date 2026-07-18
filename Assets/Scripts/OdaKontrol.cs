using UnityEngine;

public class OdaKontrol : MonoBehaviour
{
    [Header("Odanýn Çýkýþ Kapýsý")]
    public GameObject kapi;

    void Start()
    {
        // 1. Oyun baþladýðýnda kapýyý TAMAMEN kapat (fiziðiyle birlikte)
        if (kapi != null)
        {
            kapi.SetActive(false);
        }
    }

    void Update()
    {
        // 2. Sahnede kaç tane "Enemy" etiketli obje kaldýðýný say
        int dusmanSayisi = GameObject.FindGameObjectsWithTag("Enemy").Length;

        // 3. Düþman kalmadýysa ve kapý kapalýysa, kapýyý tamamen görünür ve geçilir yap
        if (dusmanSayisi == 0 && kapi != null && !kapi.activeSelf)
        {
            kapi.SetActive(true);
        }
    }
}