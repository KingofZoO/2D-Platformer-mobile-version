using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    public GameObject Bullet, StartBullet;

    [HideInInspector]
    public bool playerIsLeft = true;

    private float angle;
    private float shootTime = 0f;
    private Transform player;
    private CannonFollow cannon;

    private void Start()
    {
        cannon = GetComponentInParent<CannonFollow>();
        player = MyPlayerControl.Player.transform;
    }

    private void FixedUpdate()
    {
        if (cannon.HP == 0)
        {
            Instantiate(Bullet.GetComponent<CannonBall>().Explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        if (!playerIsLeft && transform.position.x > player.position.x)
            Flip();
        if (playerIsLeft && transform.position.x < player.position.x)
            Flip();
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (playerIsLeft)
            {
                angle = Mathf.LerpAngle(0f, Vector2.Angle(Vector2.left, player.position - transform.position), Time.time);
                transform.eulerAngles = new Vector3(0f, 0f, transform.position.y > player.position.y ? angle : -angle);
            }

            if (!playerIsLeft)
            {
                angle = Mathf.LerpAngle(0f, Vector2.Angle(Vector2.right, player.position - transform.position), Time.time);
                transform.eulerAngles = new Vector3(0f, 0f, transform.position.y < player.position.y ? angle : -angle);
            }

            if (Time.time > shootTime)
            {
                shootTime = Time.time + 1f;
                StartCoroutine("Shoot");
            }
        }
    }

    private void Flip()
    {
        playerIsLeft = !playerIsLeft;
        Vector3 flip = transform.localScale;
        flip.x *= -1;
        transform.localScale = flip;
    }

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(1);
        GetComponent<AudioSource>().Play();
        Instantiate(Bullet, StartBullet.transform.position, StartBullet.transform.rotation);
    }
}
