using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeRadius : MonoBehaviour
{
    public float explodeForce = 500f;

    private bool explodeFlag = false;
    private float explodeTime = 0.05f;

    private void Start()
    {
        StartCoroutine("DestroyEff");
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy" && !col.GetComponent<MyEnemy>().isExploded)
            {
                Vector3 delta = col.transform.position - transform.position;
                col.GetComponent<Rigidbody2D>().AddForce(delta.normalized * explodeForce);
                col.GetComponent<MyEnemy>().Hurtx2();
                col.GetComponent<MyEnemy>().Explode(explodeTime);
            }

        if (col.gameObject.tag == "Turret" && !col.GetComponent<CannonFollow>().isExploded)
        {
            Vector3 delta = col.transform.position - transform.position;
            col.GetComponent<Rigidbody2D>().AddForce(delta.normalized * explodeForce);
            col.GetComponent<CannonFollow>().Hurtx2();
            col.GetComponent<CannonFollow>().Explode(explodeTime);
        }

        if (col.gameObject.tag == "Zombie")
            Destroy(col.gameObject);
    }

    IEnumerator DestroyEff()
    {
        yield return new WaitForSeconds(explodeTime);
        Destroy(gameObject);
    }
}
