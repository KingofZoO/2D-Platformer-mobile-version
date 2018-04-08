using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBomb : MonoBehaviour
{
    public GameObject ExplodeEffect;
    public GameObject ExplodeRadius;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "Turret" || col.gameObject.tag == "Zombie")
            StartCoroutine("Explode");
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(2);
        Instantiate(ExplodeEffect, transform.position, transform.rotation);
        Instantiate(ExplodeRadius, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
