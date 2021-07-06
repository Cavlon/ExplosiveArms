using UnityEngine;

public class OptionsMenu : MonoBehaviour
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
