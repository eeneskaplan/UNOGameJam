using UnityEngine;
using UnityEngine.UI; // UI kütüphanesi

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public float alinanHasarCarpani = 1f;

    // YENÝ: Slider yerine Image kullanýyoruz
    public Image healthBarFill;

    void Start()
    {
        currentHealth = maxHealth;

        if (healthBarFill != null)
        {
            // Oyun baþýnda bar %100 (1.0f) dolu olsun
            healthBarFill.fillAmount = 1f;
        }
    }

    public void TakeDamage(int damageAmount)
    {
        int gercekHasar = Mathf.RoundToInt(damageAmount * alinanHasarCarpani);
        currentHealth -= gercekHasar;

        // YENÝ: Caný 0-1 arasýna oranlayýp görsele aktarýyoruz (Örn: 50 / 100 = 0.5f)
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = (float)currentHealth / maxHealth;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Health scriptinin içindeki Die metoduna þunu ekle:
    void Die()
    {
        // Eðer bu obje bir kamikaze düþmanýysa, patlama efektini çaðýrarak öl
        DusmanKamikaze kamikaze = GetComponent<DusmanKamikaze>();
        if (kamikaze != null)
        {
            kamikaze.Patla();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}