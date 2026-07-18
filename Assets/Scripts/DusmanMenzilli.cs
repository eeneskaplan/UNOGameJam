using UnityEngine;

public class DusmanMenzilli : MonoBehaviour
{
    public GameObject mermiPrefab; // Az önce yaptýðýn mermiyi buraya sürükle
    public Transform atisNoktasi;  // Düþmanýn önünde boþ bir obje oluþturup buraya ata
    public float atisAraligi = 2f;
    private float atisZamani = 0f;
    private Transform oyuncu;

    void Start() { oyuncu = GameObject.FindGameObjectWithTag("Player").transform; }

    void Update()
    {
        if (oyuncu == null) return;

        // Oyuncuya bak (döndür)
        Vector2 yon = oyuncu.position - transform.position;
        float aci = Mathf.Atan2(yon.y, yon.x) * Mathf.Rad2Deg;
        atisNoktasi.rotation = Quaternion.Euler(0, 0, aci);

        // Ateþ etme döngüsü
        if (Time.time >= atisZamani)
        {
            Instantiate(mermiPrefab, atisNoktasi.position, atisNoktasi.rotation);
            atisZamani = Time.time + atisAraligi;
        }
    }
}
