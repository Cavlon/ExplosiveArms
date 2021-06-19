using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectControlMethod : MonoBehaviour
{

    private weaponScript weapon;
    private Crosshair crosshair;
    private CameraFollow vcam;
    public bool useController;

    // Start is called before the first frame update
    void Start()
    {
        GetWeapon();
        crosshair = GameObject.Find("Crosshair").GetComponent<Crosshair>();
        vcam = GameObject.Find("vcamFollow").GetComponent<CameraFollow>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2) || Input.GetAxisRaw("Mouse X") != 0.0f || Input.GetAxisRaw("Mouse Y") != 0.0f)
        {
            useController = false;
        } else if (Input.GetAxisRaw("R Horizontal") != 0 || Input.GetAxisRaw("R Vertical") != 0 || Input.GetKey(KeyCode.Joystick1Button0) || Input.GetKey(KeyCode.Joystick1Button1) || Input.GetKey(KeyCode.Joystick1Button2) || Input.GetKey(KeyCode.Joystick1Button3) || Input.GetKey(KeyCode.Joystick1Button4) || Input.GetKey(KeyCode.Joystick1Button5) || Input.GetKey(KeyCode.Joystick1Button6) || Input.GetKey(KeyCode.Joystick1Button7) || Input.GetKey(KeyCode.Joystick1Button8) || Input.GetKey(KeyCode.Joystick1Button9))
        {
            useController = true;
        }
        weapon.useController = useController;
        crosshair.useController = useController;
        vcam.useController = useController;
        if (weapon == null)
        {
            GetWeapon();
        }
    }

    public void GetWeapon()
    {
        weapon = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<weaponScript>();
    }
}
