using UnityEngine;

public class MouseTakip : MonoBehaviour
{
    public Transform firePoint; // FirePoint'i buraya sürükle

    void Update()
    {
        // 1. Mouse'un dünya üzerindeki pozisyonunu al
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // 2. Oyuncudan mouse'a doðru olan vektörü hesapla
        Vector2 bakisYonu = mousePos - transform.position;

        // 3. Karakterin (ve içindeki FirePoint'in) o yöne bakmasýný saðla
        float aci = Mathf.Atan2(bakisYonu.y, bakisYonu.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, aci);

        // ÖNEMLÝ: Eðer karakterin ters dönüyorsa (kafasý aþaðý geliyorsa), 
        // þu satýrý kullanarak sprite'ý düzeltiyoruz:
        if (aci > 90 || aci < -90)
        {
            transform.localScale = new Vector3(1, -1, 1); // Y ekseninde ters çevir
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}