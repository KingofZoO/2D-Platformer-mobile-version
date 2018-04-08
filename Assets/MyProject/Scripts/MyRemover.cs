using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyRemover : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        //Если с KillTrigger пересекается враг - играется splash, враг уничтожается
        if (col.gameObject.tag == "Enemy")
            Destroy(col.gameObject);

        //Если с KillTrigger пересекается игрок - игра начинается сначала (сцена загружается снова)
        if (col.gameObject.tag == "Player")
            StartCoroutine("Restart");
    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
