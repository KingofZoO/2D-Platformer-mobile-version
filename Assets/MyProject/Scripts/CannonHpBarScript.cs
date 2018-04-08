using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CannonHpBarScript : MonoBehaviour
{
    private GameObject cannon;
    private int startHP;

    private float scaleX;
    private float scaleY;
    private float scaleZ;
    private float finalScaleX;

    private void Awake()
    {
        cannon = GameObject.Find("Cannon");
        startHP = cannon.GetComponent<CannonFollow>().HP;

        scaleX = transform.localScale.x;
        scaleY = transform.localScale.y;
        scaleZ = transform.localScale.z;

        finalScaleX = scaleX / startHP;
    }

    private void FixedUpdate()
    {
        transform.localScale = new Vector3(cannon.GetComponent<CannonFollow>().HP * finalScaleX, scaleY, scaleZ);
    }
}
