using UnityEngine;

public class LazerHasar : MonoBehaviour
{
    public int damage = 25; // Lazere deðince verilecek hasar

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // Çarpan þeyin Player etiketi (Tag) varsa hasar ver
        if (hitInfo.CompareTag("Player"))
        {
            hitInfo.GetComponent<Health>().TakeDamage(damage);
        }
    }
}