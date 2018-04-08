using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBullet : MonoBehaviour
{
    public GameObject explosion;    //Анимация взрыва
    public float speed, lifeTime;   //Скорость и время жизни ракеты

    private Vector3 dir = new Vector3(0, 0, 0);
    private MyPlayerControl playerCtrl;     //Экземпляр класса скрипта контроллера игрока, требуется для обращения к текущему состоянию facingRight
    private bool rocketRight;
    private bool isHitted = false;

    private void Awake()
    {
        playerCtrl = GameObject.FindGameObjectWithTag("Player").GetComponent<MyPlayerControl>();
    }

    private void Start()
    {
        //Задание скорости ракеты в зависимости от направления игрока
        if (playerCtrl.facingRight)
        {
            rocketRight = true;
            dir.x = speed * Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad);
            dir.y = speed * Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad);
        }
        else
        {
            rocketRight = false;
            Flip();
            dir.x = -speed * Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad);
            dir.y = -speed * Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad);
        }

        GetComponent<Rigidbody2D>().AddForce(dir*50f);
        StartCoroutine("DestroyRocket");  //Уничтожение ракеты по прошествии времени жизни
    }

    //Движение ракеты
    private void FixedUpdate()
    {
        if(rocketRight)
            transform.Rotate(0, 0, -1.2f);
        else transform.Rotate(0, 0, 1.2f);
    }

    //При столкновении с врагом, вызываются методы Hurt() и Explode() 
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!isHitted)
        {
            if (col.gameObject.tag == "Enemy")
            {
                col.gameObject.GetComponent<MyEnemy>().Hurt();
                Explode();
            }

            if (col.gameObject.tag == "ground")
                Explode();

            if (col.gameObject.tag == "Turret")
            {
                col.gameObject.GetComponent<CannonFollow>().Hurt();
                Explode();
            }

            if (col.gameObject.tag == "Zombie")
            {
                col.gameObject.GetComponent<ZombieScript>().Hurt();
                Explode();
            }
        }
    }

    //Метод взрыва ракеты - вызывается анимация взрыва и уничтожается ракета
    private void Explode()
    {
        isHitted = true;
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void Flip()
    {
        Vector3 flip = transform.localScale;
        flip.x *= -1;
        transform.localScale = flip;
    }

    IEnumerator DestroyRocket()
    {
        yield return new WaitForSeconds(lifeTime);
        Explode();
    }
}
