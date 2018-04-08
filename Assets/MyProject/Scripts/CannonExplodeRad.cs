using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonExplodeRad : MonoBehaviour
{
    public float explodeForce = 100f;

    private void Start()
    {
        StartCoroutine("DestroyEff");
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Vector3 delta = col.transform.position - transform.position;
            col.GetComponent<Rigidbody2D>().AddForce(delta.normalized * explodeForce);
        }
    }

    IEnumerator DestroyEff()
    {
        yield return new WaitForSeconds(0.05f);
        Destroy(gameObject);
    }
}
