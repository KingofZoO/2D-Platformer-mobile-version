using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public GameObject Explosion, ExplodeRadius;
    public float speed, lifeTime;

    private Vector3 dir = new Vector3();
    private bool playerIsLeft;
    private GameObject player;
    private bool damageTaken = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerIsLeft = transform.position.x > player.transform.position.x ? true : false;
    }

    private void Start()
    {
        if (!playerIsLeft)
        {
            dir.x = speed * Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad);
            dir.y = speed * Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad);
        }

        if (playerIsLeft)
        {
            dir.x = -speed * Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad);
            dir.y = -speed * Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad);
        }

        GetComponent<Rigidbody2D>().AddForce(dir * 50f);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!damageTaken && col.gameObject.tag == "Player")
        {
            Explode();
            GetDamage();
        }

        if (col.gameObject.tag == "ground")
            Explode();
    }

    private void Explode()
    {
        Instantiate(Explosion, transform.position, transform.rotation);
        Instantiate(ExplodeRadius, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void GetDamage()
    {
        damageTaken = true;
        player.GetComponent<MyPlayerHealth>().TakeDamage(1);
    }
}
