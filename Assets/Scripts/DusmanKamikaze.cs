using UnityEngine;

public class DusmanKamikaze : MonoBehaviour
{
    [Header("Ayarlar")]
    public float hareketHizi = 4f;
    public float patlamaMenzili = 1.2f;
    public GameObject patlamaEfektiPrefab; // Patlama prefabýný buraya sürükle

    private Transform oyuncu;

    void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) oyuncu = p.transform;
    }

    void Update()
    {
        if (oyuncu == null) return;

        // 1. Oyuncuya doðru koþ
        transform.position = Vector2.MoveTowards(transform.position, oyuncu.position, hareketHizi * Time.deltaTime);

        // 2. Menzile girdiyse (dibine geldiyse) patla
        if (Vector2.Distance(transform.position, oyuncu.position) <= patlamaMenzili)
        {
            Patla();
        }
    }

    public void Patla()
    {
        // Patlama efektini (hasar vereni) doður
        if (patlamaEfektiPrefab != null)
        {
            Instantiate(patlamaEfektiPrefab, transform.position, Quaternion.identity);
        }

        // Kendini yok et (hem menzilde hem ölünce çaðýrýyoruz)
        Destroy(gameObject);
    }
}