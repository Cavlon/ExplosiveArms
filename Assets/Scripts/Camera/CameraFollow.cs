using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    float maxScreenPoint = 0.8f;
    public Transform player;
    private Vector3 velocity = Vector3.zero;
    public float dampTime;
    public Camera mainCamera;
    public bool useController;

    public void Update()
    {
        if (!useController)
        {
            Vector3 mousePos = Input.mousePosition * maxScreenPoint + new Vector3(Screen.width, Screen.height, 0f) * ((1f - maxScreenPoint) * 0.5f);
            Vector3 position = (player.position + mainCamera.ScreenToWorldPoint(mousePos)) / 2f;
            Vector3 destination = new Vector3(position.x, position.y, -10);
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        } else
        {
            Vector3 Controller = (Vector2.right * Input.GetAxisRaw("R Horizontal")) + (Vector2.up * Input.GetAxisRaw("R Vertical"));
            Vector3 pos = (mainCamera.WorldToScreenPoint(player.position) + (Controller * 400f)) * maxScreenPoint + new Vector3(Screen.width, Screen.height, 0f) * ((1f - maxScreenPoint) * 0.5f);
            Vector3 position = (player.position + mainCamera.ScreenToWorldPoint(pos)) / 2f;
            Vector3 destination = new Vector3(position.x, position.y, -10);
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }
    }
}
