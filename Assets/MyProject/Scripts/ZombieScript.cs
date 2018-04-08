using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : MonoBehaviour
{
    public float maxMoveSpeed = 5f;
    public float forceSpeed = 100f;
    public int damage = 1;
    public int HP = 2;
    public float InvokeStart;
    public float InvokeRepeat;

    public bool isAngry = false;
    private bool facingRight = true;
    private float lastAngryTime;
    private float angryTime = 3f;

    private float attackTime = 0f;
    private float attackColdown = 1f;

    private RaycastHit2D hit;
    private RaycastHit2D detection;
    private float hitDistance = 1f;
    private float detDistance = 15f;

    private GameObject target;
    private Vector3 targetPos;
    private Vector2 direction = new Vector2(1, 0);
    private LayerMask playerMask;
    private Vector3 startPos;
    private Rigidbody2D rigidbody;

    private void Start()
    {
        playerMask = 1 << LayerMask.NameToLayer("Player");
        startPos = transform.position;
        rigidbody = GetComponent<Rigidbody2D>();

        if (!isAngry)
        {
            InvokeRepeating("InvFlip", InvokeStart, InvokeRepeat);
        }
    }

    private void FixedUpdate()
    {
        if (HP <= 0)
            Destroy(gameObject);

        if (Time.time - lastAngryTime > angryTime)
        {
            isAngry = false;
            target = null;
        }

        detection = Physics2D.Raycast(transform.position, direction, detDistance, playerMask);
        hit = Physics2D.Raycast(transform.position, direction, hitDistance, playerMask);

        if (detection.collider != null)
        {
            isAngry = true;
            lastAngryTime = Time.time;
            target = detection.collider.gameObject;
            targetPos = target.transform.position;

            if (hit.collider != null && Time.time > attackTime)
            {
                Attack();
                attackTime = Time.time + attackColdown;
            }
        }

        if (isAngry)
            MoveTo(targetPos);
        else 
            MoveTo(startPos);
    }

    private void MoveTo(Vector3 location)
    {
        float xPos = location.x - transform.position.x;

        if (!facingRight && xPos > 1f)
            Flip();
        if (facingRight && xPos < -1f)
            Flip();

        if (Mathf.Abs(xPos) > 1f)
            rigidbody.AddForce(direction * forceSpeed);

        if (Mathf.Abs(rigidbody.velocity.x) > maxMoveSpeed)
            rigidbody.velocity = new Vector2(direction.x * maxMoveSpeed, rigidbody.velocity.y);
    }

    private void Attack()
    {
        target.GetComponent<MyPlayerHealth>().TakeDamage(damage);
        target.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 200f));
    }

    public void Hurt()
    {
        HP -= 1;
    }

    public void Hurtx2()
    {
        HP -= 2;
    }

    private void InvFlip()
    {
        if (!isAngry && Mathf.Abs(rigidbody.velocity.x) < 0.1f)
            Flip();
    }

    private void Flip()
    {
        facingRight = !facingRight;
        direction *= -1;
        Vector3 flip = transform.localScale;
        flip.x *= -1;
        transform.localScale = flip;
    }
}
