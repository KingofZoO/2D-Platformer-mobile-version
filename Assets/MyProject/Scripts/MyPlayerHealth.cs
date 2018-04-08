using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyPlayerHealth : MonoBehaviour
{
    public int HP = 10;

    private void FixedUpdate()
    {
        if (HP <= 0)
            Die();
    }

    public void TakeDamage(int damage)
    {
        HP -= damage;
        GetComponent<Animator>().SetTrigger("TakeDamage");
    }

    private void Die()
    {
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (var c in colliders)
            c.isTrigger = true;
        StartCoroutine("Restart");
    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
