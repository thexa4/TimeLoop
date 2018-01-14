using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationAutoDestroy : MonoBehaviour
{
    public float Delay = 0f;

    void Start()
    {
        var animator = GetComponent<Animator>();
        if (animator == null) return;
        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length + Delay);
    }
}