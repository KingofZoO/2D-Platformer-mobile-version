using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonsScript : MonoBehaviour
{
    public void SetBomb()
    {
        MyPlayerControl.Player.SetBomb();
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void Jump()
    {
        MyPlayerControl.Player.Jump();
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void ShootStateTrue()
    {
        MyPlayerControl.Player.shootState = true;
    }

    public void ShootStateFalse()
    {
        MyPlayerControl.Player.shootState = false;
    }

    public void OnBombButton()
    {
        MyPlayerControl.Player.joystickOnBombButton = true;
    }

    public void FromBombButton()
    {
        MyPlayerControl.Player.joystickOnBombButton = false;
    }
}
