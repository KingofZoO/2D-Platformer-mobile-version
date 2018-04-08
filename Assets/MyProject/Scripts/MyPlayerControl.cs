using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyPlayerControl : MonoBehaviour
{
    public GameObject Bullet, StartBullet, Bomb;  //Объект "ракета" и ее начальное положение
    public float acceleration;              //Скорость перемещения игрока по x

    public int rocketsAmmo = 50;
    public int bombsAmmo = 10;

    public Transform Joystick;

    [HideInInspector]
    public static MyPlayerControl Player;

    [HideInInspector]
    public bool facingRight = true;         //Переменная для проверки направления движения игрока 
    [HideInInspector]
    public int score;
    [HideInInspector]
    public int scoreLimit = 10;
    [HideInInspector]
    public bool shootState = true;
    [HideInInspector]
    public bool joystickOnBombButton = false;

    private bool isJumping = false;
    private Vector3 dir = new Vector3(0, 0, 0);
    private bool grounded = false;
    private bool aboveEnemy = false;
    private Transform groundCheck;
    private Animator animator;
    private Vector3 touchPosition;
    private GameManagerScript GameManager;
    private MouseFollower mouseFollower;

    private void Awake()
    {
        Player = this;
        mouseFollower = GetComponentInChildren<MouseFollower>();
    }

    private void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        animator = GetComponent<Animator>();
        score = 0;
        groundCheck = transform.Find("groundCheck");
    }

    private void Update()
    {
        if (Time.timeScale < 1f)
            return;

        if (Input.GetKeyDown(KeyCode.Escape))
            GameManager.ShowMenuPanel();

        animator.SetBool("IsMoving", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > 0.1f);

        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        aboveEnemy = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Enemies"));

        if (Input.touchCount > 1 && !joystickOnBombButton)
            shootState = true;

        if (shootState)
        {
            if (Input.touchCount > 0 && Input.touches[Input.touchCount - 1].phase == TouchPhase.Began)
            {
                PlayerToTouchPos(Input.touchCount - 1);
                Shoot();
            }
        }
    }

    //Движение игрока при нажатии соответствующих клавиш. Также при смене направления движения вызывается метод поворота Flip()
    private void FixedUpdate()
    {
        if (Time.timeScale < 1f)
            return;

        if (score >= scoreLimit)
            StartCoroutine("NextLevel");

        if (Joystick.localPosition.y > 30f && !isJumping)
        {
            isJumping = true;
            Jump();
        }
        else if (Joystick.localPosition.y < 30f)
        {
            isJumping = false;
        }

        if (Joystick.localPosition.x > 10f)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(acceleration, GetComponent<Rigidbody2D>().velocity.y);
            if (!facingRight)
                Flip();
        }
        else if (Joystick.localPosition.x < -10f) 
        {
          GetComponent<Rigidbody2D>().velocity = new Vector2(-acceleration, GetComponent<Rigidbody2D>().velocity.y);
            if (facingRight)
                Flip();
        }
        else GetComponent<Rigidbody2D>().velocity = new Vector2(0f, GetComponent<Rigidbody2D>().velocity.y);
    }

    //Метод, инвертирующий множитель масштаба по x для объекта игрока. Таким образом достигается эффект поворота на 180.
    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 flip = transform.localScale;
        flip.x *= -1;
        transform.localScale = flip;
    }

    private void Shoot()
    {
        if (rocketsAmmo != 0)
        {
            rocketsAmmo -= 1;
            GetComponent<AudioSource>().Play();
            Instantiate(Bullet, StartBullet.transform.position, StartBullet.transform.rotation);
            animator.SetTrigger("Shot");
        }
    }

    private void PlayerToTouchPos(int n)
    {
        touchPosition = Camera.main.ScreenToWorldPoint(Input.touches[n].position);

        if (touchPosition.x > transform.position.x && !facingRight)
            Flip();
        if (touchPosition.x < transform.position.x && facingRight)
            Flip();

        mouseFollower.SetToMousePosition();
    }

    public void PlusScore(int num)
    {
        score += num;
    }

    public void Jump()
    {
        if (grounded || aboveEnemy)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 1000f));
            animator.SetTrigger("Jump");
        }
    }

    public void SetBomb()
    {
        if (bombsAmmo != 0)
        {
            bombsAmmo -= 1;
            Instantiate(Bomb, transform.position, transform.rotation);
        }
    }

    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(2);
    }
}
