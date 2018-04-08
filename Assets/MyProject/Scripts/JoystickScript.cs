using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickScript : MonoBehaviour
{
    private Vector3 mouse;

    private void FixedUpdate()
    {
        if (Input.touchCount == 0)
        {
            transform.localPosition = new Vector3(0, 0, 0);
            return;
        }
        else
        {
            mouse = Camera.main.ScreenToWorldPoint(Input.touches[0].position);

            if (JoystickBaseScript.joyMove == true)
            {
                transform.position = new Vector3(mouse.x, mouse.y);
                if (Input.touches[0].phase == TouchPhase.Ended)
                    transform.localPosition = new Vector3(0, 0, 0);
            }
            else transform.localPosition = new Vector3(0, 0, 0);
        }
    }
}
