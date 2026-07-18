using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Hareket Ayarlarý")]
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector2 lastMoveDirection;

    [Header("Dash Ayarlarý")]
    public float dashSpeed = 15f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    private bool isDashing;

    // YENÝ: Double Dash Sistemi
    private int maxDashes = 1;         // Varsayýlan dash sayýsý
    private int currentDashes;         // O an elimizde olan dash hakký
    private bool isRecharging = false; // Dash dolum süreci aktif mi?

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lastMoveDirection = new Vector2(1, 0);

        // OYUN BAÞINDA DUMAN ELEMENTÝ (2) SEÇÝLDÝYSE DOUBLE DASH VER
        if (PlayerPrefs.HasKey("IlkElement") && PlayerPrefs.GetInt("IlkElement") == 2)
        {
            maxDashes = 2;
        }
        currentDashes = maxDashes; // Baþlangýçta cephaneyi fullüyoruz
    }

    void Update()
    {
        if (isDashing) return;

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;

        if (movement.x != 0 || movement.y != 0)
        {
            lastMoveDirection = movement;
        }

        if (movement.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (movement.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        // YENÝ: canDash yerine currentDashes sayýsýný kontrol ediyoruz
        if (Input.GetKeyDown(KeyCode.Space) && currentDashes > 0 && !isDashing)
        {
            StartCoroutine(DashRoutine());
        }
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            rb.MovePosition(rb.position + lastMoveDirection * dashSpeed * Time.fixedDeltaTime);
        }
        else
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }

    private IEnumerator DashRoutine()
    {
        isDashing = true;
        currentDashes--; // Bir dash hakkýný harca

        // Eðer arkada bir dolum iþlemi yoksa, dolumu baþlat
        if (!isRecharging)
        {
            StartCoroutine(RechargeDashRoutine());
        }

        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
    }

    // YENÝ: Dash haklarýný Cooldown süresine göre tek tek dolduran Coroutine
    private IEnumerator RechargeDashRoutine()
    {
        isRecharging = true;

        while (currentDashes < maxDashes)
        {
            yield return new WaitForSeconds(dashCooldown);
            currentDashes++;
        }

        isRecharging = false;
    }
}