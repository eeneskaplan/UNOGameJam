using UnityEngine;

public class LazerHasar : MonoBehaviour
{
    public int damage = 25; // Ýstediðin gibi ayarla

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // Eðer lazer oyuncuya deðerse hasar ver
        if (hitInfo.CompareTag("Player"))
        {
            hitInfo.GetComponent<Health>().TakeDamage(damage);
        }
    }
}