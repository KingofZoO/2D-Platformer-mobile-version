using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySpawner : MonoBehaviour
{
    public GameObject enemy;        //Объект для спауна
    public float minTimeSpawn = 2f; 
    public float maxTimeSpawn = 4f; //Разброс интервалов между спаунами
    private float timeSpawn;        //Переменная, обеспечивающая случайный спаун

    private void Start()
    {
        timeSpawn = Time.time + minTimeSpawn;   //Первый спаун - по минимальному интервалу
    }

    //Спаун происходит по прошествии времени timeSpawn, после чего к timeSpawn добавляется случайная величина.
    //В итоге имеем случайные интервалы спауна
    private void Update()
    {
        if (Time.time > timeSpawn)
        {
            Spawn();
            timeSpawn += Random.Range(minTimeSpawn, maxTimeSpawn);
        }
    }

    //Спаун - простое создание объекта
    private void Spawn()
    {
        Instantiate(enemy, transform.position, transform.rotation);
    }
}
