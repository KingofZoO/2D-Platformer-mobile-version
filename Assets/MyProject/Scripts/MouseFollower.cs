using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollower : MonoBehaviour
{
    private MyPlayerControl playerCtrl;
    private Vector3 mouse;
    private float angle;

    private void Awake()
    {
        playerCtrl = GameObject.FindGameObjectWithTag("Player").GetComponent<MyPlayerControl>();
    }

    private void FixedUpdate()
    {
        SetToMousePosition();
    }

    public void SetToMousePosition()
    {
        if (Input.touchCount > 0)
            mouse = Camera.main.ScreenToWorldPoint(Input.touches[Input.touchCount - 1].position);
        else return;

        if (playerCtrl.facingRight)
        {
            angle = Mathf.Clamp(Vector2.Angle(Vector2.right, mouse - transform.position), -90f, 90f);
            transform.eulerAngles = new Vector3(0f, 0f, transform.position.y < mouse.y ? angle : -angle);
        }
        else
        {
            angle = Mathf.Clamp(Vector2.Angle(Vector2.left, mouse - transform.position), -90f, 90f);
            transform.eulerAngles = new Vector3(0f, 0f, transform.position.y > mouse.y ? angle : -angle);
        }
    }
}
