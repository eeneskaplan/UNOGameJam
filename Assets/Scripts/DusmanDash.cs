using System.Collections;
using UnityEngine;

public class DusmanDash : MonoBehaviour
{
    [Header("Hareket ve Dash Ayarlar�")]
    public float normalHiz = 2f;
    public float dashHizi = 15f;
    public float dashMenzili = 5f;
    public float dashHazirlikSuresi = 0.5f;
    public float dashBeklemeSuresi = 1.5f;

    private Transform oyuncu;
    private Rigidbody2D rb; // F�Z�K ���N EKLEND�
    private bool dashYapiliyorMu = false;

    void Start()
    {
        // R�G�DBODY B�LE�EN�N� KODA BA�LADIK
        rb = GetComponent<Rigidbody2D>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            oyuncu = playerObj.transform;
        }
    }

    // F�Z�K ��LEMLER� ���N UPDATE YER�NE FIXEDUPDATE KULLANILIR
    void FixedUpdate()
    {
        if (oyuncu == null || dashYapiliyorMu) return;

        float mesafe = Vector2.Distance(rb.position, (Vector2)oyuncu.position);

        if (mesafe <= dashMenzili)
        {
            StartCoroutine(DashSistemi());
        }
        else
        {
            // F�Z�K KURALLARINA UYGUN NORMAL Y�R�ME
            Vector2 yeniPozisyon = Vector2.MoveTowards(rb.position, (Vector2)oyuncu.position, normalHiz * Time.fixedDeltaTime);
            rb.MovePosition(yeniPozisyon);
        }
    }

    IEnumerator DashSistemi()
    {
        dashYapiliyorMu = true;

        // Haz�rl�k a�amas� - durmas�n� garanti alt�na alal�m ki kaymas�n
        Vector2 dashYonu = (oyuncu.position - transform.position).normalized;
        rb.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(dashHazirlikSuresi);

        float dashSuresi = 0.2f;
        float gecenSure = 0f;

        while (gecenSure < dashSuresi)
        {
            // F�Z�K KURALLARINA UYGUN DASH ATMA
            rb.MovePosition(rb.position + dashYonu * dashHizi * Time.fixedDeltaTime);
            gecenSure += Time.fixedDeltaTime;

            // Frame yerine fizik g�ncellemesini (FixedUpdate) bekliyoruz
            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForSeconds(dashBeklemeSuresi);

        dashYapiliyorMu = false;
    }
}