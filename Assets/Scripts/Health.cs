using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        // Karakter hasar aldýðýnda kýrmýzýya boyanýp geri dönmesi gibi efektleri ileride buraya ekleyeceðiz

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Þimdilik caný sýfýrlanan objeyi sahneden siliyoruz
        Destroy(gameObject);
    }
}
