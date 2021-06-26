using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsAnimation : MonoBehaviour
{
    [SerializeField] protected Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Animate(bool enter)
    {
        if (enter)
        {
            animator.Play("Options");
        } else
        {
            animator.Play("ExitOptions");
        }
    }
}
