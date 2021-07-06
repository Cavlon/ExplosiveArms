using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{

    public static bool paused;

    [SerializeField] private Animator pauseAnim;


    private void Start()
    {
        paused = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            paused = !paused;
            Pause();
        }
    }

    private void Pause()
    {
        if (paused)
        {
            Time.timeScale = 0f;
            pauseAnim.SetBool("Pause", true);
        } else
        {
            Time.timeScale = 1f;
            pauseAnim.SetBool("Pause", false);
        }
    }

    public void Resume()
    {
        paused = false;
        Pause();
    }
}
