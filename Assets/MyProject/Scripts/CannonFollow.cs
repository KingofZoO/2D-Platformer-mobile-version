using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CannonFollow : MonoBehaviour
{
    public int HP = 15;
    public float acceleration;

    [HideInInspector]
    public bool isExploded = false;

    private Rigidbody2D rigidbody;
    private Transform player;
    private float vel;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (HP <= 0)
            PlayerWin();

        vel = Mathf.Clamp(Mathf.Lerp(transform.position.x, player.position.x, 1f), -acceleration, acceleration);
        rigidbody.velocity = new Vector2(vel, rigidbody.velocity.y);
    }

    public void Hurt()
    {
        HP -= 1;
    }

    public void Hurtx2()
    {
        HP -= 2;
    }

    public void Explode(float explodeTime)
    {
        isExploded = true;
        StartCoroutine("Exploded", explodeTime);
    }

    private void PlayerWin()
    {
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (var c in colliders)
            c.isTrigger = true;
        StartCoroutine("NextLevel");
    }

    IEnumerator Exploded(float explodeTime)
    {
        yield return new WaitForSeconds(explodeTime);
        isExploded = false;
    }

    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(3);
    }
}
