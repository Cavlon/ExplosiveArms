using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public int Damping = 5;
    public Transform Player;
 public float Height = 4;
 public float Offset = 5;
 
 private Vector3 Center;
 public float ViewDistance = 5f;
 
 void Update()
    {
        var mousePos = Input.mousePosition;
        mousePos.z = ViewDistance;
        Vector3 CursorPosition = Camera.main.ScreenToWorldPoint(mousePos);

        var PlayerPosition = Player.position;

        Center = new Vector3((PlayerPosition.x + CursorPosition.x) / 2, PlayerPosition.y, (PlayerPosition.z + CursorPosition.z) / 2);

        transform.position = Vector3.Lerp(transform.position, Center + new Vector3(0, Height, Offset), Time.deltaTime * Damping);
    }
}
