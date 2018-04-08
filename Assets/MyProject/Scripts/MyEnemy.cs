using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyEnemy : MonoBehaviour
{
    public GameObject Splash;
    public int HP = 3;              //Запас здоровья врага
    public float moveSpeed = 5f;    //Скорость врага по x
    public LayerMask PlayerMask;

    [HideInInspector]
    public bool isExploded = false;

    private GameObject player;
    private RaycastHit2D hit;
    private float attackColdown = 1f;
    private float attackTime = 0f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    //Перемещение задается через обращение к Rigidbody2D, иначе враги застревают на склонах
    private void FixedUpdate()
    {
        if (HP <= 0)
        {
            Destroy(gameObject);    //Если запас здоровья упал до 0 - враг уничтожен
            player.GetComponent<MyPlayerControl>().PlusScore(1);
        }

        GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);

        if (moveSpeed < 0)
            hit = Physics2D.Raycast(transform.position, Vector2.left, 1, PlayerMask);
        else hit = Physics2D.Raycast(transform.position, Vector2.right, 1, PlayerMask);

        if (hit.collider != null && Time.time > attackTime)
        {
            Attack();
            attackTime = attackColdown + Time.time;
        }
    }

    //Метод, отнимающий единицу здоровья врагу. Вызывается при столкновении с ракетой игрока
    public void Hurt()
    {
        HP -= 1;
    }

    public void Hurtx2()
    {
        HP -= 2;
    }

    private void Attack()
    {
        player.GetComponent<MyPlayerHealth>().TakeDamage(1);
        player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 200f));
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Instantiate(Splash, transform.position, transform.rotation);
            Destroy(gameObject);
            player.GetComponent<MyPlayerControl>().PlusScore(1);
        }
    }

    public void Explode(float explodeTime)
    {
        isExploded = true;
        StartCoroutine("Exploded", explodeTime);
    }

    IEnumerator Exploded(float explodeTime)
    {
        yield return new WaitForSeconds(explodeTime);
        isExploded = false;
    }
}
