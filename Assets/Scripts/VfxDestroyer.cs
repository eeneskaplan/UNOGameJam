using UnityEngine;

public class VfxDestroyer : MonoBehaviour
{
    void Start()
    {
        Animator anim = GetComponent<Animator>();

        if (anim != null)
        {
            // Oynayan animasyonun saniye cinsinden tam uzunluðunu otomatik alýr
            float gercekSure = anim.GetCurrentAnimatorStateInfo(0).length;
            Destroy(gameObject, gercekSure);
        }
        else
        {
            // Eðer objede Animator yoksa önlem olarak 1 saniye sonra siler
            Destroy(gameObject, 1f);
        }
    }
}