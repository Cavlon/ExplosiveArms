using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    [SerializeField] protected string anim;
    [SerializeField] protected Animator animator;
    [SerializeField] protected float delay;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        animator.Play(anim);
        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length + delay);
    }
}
