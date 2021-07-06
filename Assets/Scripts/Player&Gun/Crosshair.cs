using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    public bool useController;
    private Image sprite;
    private Camera cam;
    private Transform playertrans;
    private bool gameOver;
    private DetectControlMethod control;
    [SerializeField] protected float distance;


    // Start is called before the first frame update
    void Start()
    {
        playertrans = GameObject.FindGameObjectWithTag("Player").transform;
        cam = Camera.main;
        sprite = GetComponent<Image>();
        control = FindObjectOfType<DetectControlMethod>();
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!useController)
        {
            sprite.enabled = true;
            transform.position = Input.mousePosition;
        } else
        {
            Vector3 pos = (Vector2.right * Input.GetAxisRaw("R Horizontal")) + (Vector2.up * Input.GetAxisRaw("R Vertical"));
            if (pos.sqrMagnitude > 0.0f)
            {
                sprite.enabled = true;
                pos = pos.normalized;
                Vector3 newPos = cam.WorldToScreenPoint(playertrans.position) + (pos * distance);
                transform.position = new Vector3(Mathf.Clamp(newPos.x, 100, Screen.width - 50), Mathf.Clamp(newPos.y, 100, Screen.height - 50), 0);
            } else
            {
                sprite.enabled = false;
            }
        }
        gameOver = control.gameOver;
        if (Cursor.visible == true && !gameOver)
        {
            Cursor.visible = false;
        }
    }
}
