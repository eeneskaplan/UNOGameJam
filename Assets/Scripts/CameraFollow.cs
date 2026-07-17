using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Hedef ve Ayarlar")]
    public Transform target; // Karakteri buraya sürükleyeceðiz
    public float smoothSpeed = 7f; // Kameranýn yumuþaklýk/gecikme hýzý
    public Vector3 offset = new Vector3(0f, 0f, -10f); // Kameranýn Z ekseninde geride durmasý þart

    void LateUpdate() // Kameranýn titrememesi için kamerayý LateUpdate içinde hareket ettiririz
    {
        if (target != null)
        {
            // Kameranýn gitmesi gereken nihai pozisyon
            Vector3 desiredPosition = target.position + offset;

            // Bulunduðu yerden gitmesi gereken yere yumuþak bir geçiþ (Lerp) yap
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

            transform.position = smoothedPosition;
        }
    }
}