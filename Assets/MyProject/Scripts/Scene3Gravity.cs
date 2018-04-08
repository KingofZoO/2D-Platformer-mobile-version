using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene3Gravity : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
            col.gameObject.GetComponent<Rigidbody2D>().gravityScale = -1;
    }
}
