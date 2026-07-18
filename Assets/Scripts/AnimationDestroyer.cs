using UnityEngine;

public class AnimationDestroyer : MonoBehaviour
{
    void Start()
    {
        // Animasyonun uzunluðu kadar bekleyip objeyi yok et
        Animator anim = GetComponent<Animator>();
        if (anim != null)
        {
            float animSuresi = anim.GetCurrentAnimatorStateInfo(0).length;
            Destroy(gameObject, animSuresi);
        }
    }
}