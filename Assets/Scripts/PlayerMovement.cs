using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Hareket Ayarlarý")]
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector2 lastMoveDirection; // Karakter dururken dash atarsa son gittiði yöne atýlsýn diye

    [Header("Dash Ayarlarý")]
    public float dashSpeed = 15f;      // Dash atarkenki hýz
    public float dashDuration = 0.2f;  // Dash'in havada kalma süresi
    public float dashCooldown = 1f;    // Yeniden dash atmak için bekleme süresi
    private bool isDashing;
    private bool canDash = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Oyun baþýnda saða veya sola dash atabilmesi için varsayýlan bir yön belirliyoruz
        lastMoveDirection = new Vector2(1, 0);
    }

    void Update()
    {
        // Eðer karakter dash atýyorsa, dash bitene kadar klavyeden gelen diðer komutlarý yoksay
        if (isDashing) return;

        // Girdileri al
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;

        // Eðer karakter hareket ediyorsa, son gittiði yönü hafýzaya al
        if (movement.x != 0 || movement.y != 0)
        {
            lastMoveDirection = movement;
        }

        // --- YÖN DÖNDÜRME (FLIP) ÝÞLEMÝ ---
        if (movement.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (movement.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        // --- DASH TETÝKLEME ---
        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(DashRoutine());
        }
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            // Dash sýrasýndayken son yöne doðru yüksek hýzla fýrla
            rb.MovePosition(rb.position + lastMoveDirection * dashSpeed * Time.fixedDeltaTime);
        }
        else
        {
            // Normal yürüyüþ
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }

    // Dash iþlemlerinin zamanlamasýný ayarlayan Coroutine
    private IEnumerator DashRoutine()
    {
        canDash = false;
        isDashing = true;

        // Dash'in bitmesini bekle (Örn: 0.2 saniye boyunca hýzlý gidecek)
        yield return new WaitForSeconds(dashDuration);

        isDashing = false;

        // Yeniden dash atabilmek için cooldown süresi kadar bekle (Örn: 1 saniye)
        yield return new WaitForSeconds(dashCooldown);

        canDash = true;
    }
}