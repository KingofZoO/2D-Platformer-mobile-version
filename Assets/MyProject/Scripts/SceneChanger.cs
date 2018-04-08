using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    IEnumerator ChangeScene()
    {
        yield return null;
        if (SceneManager.GetActiveScene().buildIndex == 1)
            SceneManager.LoadScene(2);
        else if (SceneManager.GetActiveScene().buildIndex == 2)
            SceneManager.LoadScene(3);
        else if (SceneManager.GetActiveScene().buildIndex == 3)
            SceneManager.LoadScene(1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
            StartCoroutine("ChangeScene");
    }
}
