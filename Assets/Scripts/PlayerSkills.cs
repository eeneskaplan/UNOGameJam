using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    [Header("Kuþanýlan Yetenek")]
    public ElementData aktifElement; // Oyuncunun seçtiði kart buraya gelecek
    public Transform firePoint;      // Yeteneðin çýkacaðý yer (tabancanýn ucuyla ayný olabilir)

    private float sonKullanimZamani = -100f; // Baþlar baþlamaz kullanýlabilmesi için eksi bir deðer

    void Update()
    {
        // Eðer bir element seçilmemiþse boþuna kodu yorma
        if (aktifElement == null) return;

        // C tuþuna basýldýðýnda ve bekleme süresi (cooldown) dolduðunda
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (Time.time >= sonKullanimZamani + aktifElement.beklemeSuresi)
            {
                YetenekKullan();
                // Sayacý sýfýrla
                sonKullanimZamani = Time.time;
            }
            else
            {
                // Ýsteðe baðlý: Ekrana "Yetenek Bekleme Süresinde!" uyarýsý yazdýrýlabilir
                Debug.Log("Yetenek hala bekleme süresinde!");
            }
        }
    }

    void YetenekKullan()
    {
        // Farenin konumunu alýp yeteneði oraya doðru fýrlatmak için açý hesapla
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        Vector2 lookDirection = mousePosition - transform.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        // Elementin içindeki özel yetenek prefabýný farenin olduðu açýya doðru yarat
        Instantiate(aktifElement.yetenekPrefab, firePoint.position, Quaternion.Euler(0, 0, angle));
    }
}