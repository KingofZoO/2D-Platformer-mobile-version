using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickBaseScript : MonoBehaviour
{
    public static bool joyMove = false;

    public void JoyMoveTrue()
    {
        joyMove = true;
    }

    public void JoyMoveFalse()
    {
        joyMove = false;
    }
}
