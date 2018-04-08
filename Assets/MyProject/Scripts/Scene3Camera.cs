using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene3Camera : MonoBehaviour
{
    public float xMin = 1f;
    public float xMax = 70f;
    public float yMin = 1f;
    public float yMax = 40f;
    public Texture2D HealthIcon;
    public Texture2D RocketsIcon;
    public Texture2D BombsIcon;

    private GUIStyle style = new GUIStyle();

    private int HP;
    private int rocketsAmmo;
    private int bombsAmmo;

    float targetX;
    float targetY;

    private void OnGUI()
    {
        for (int i = 0; i < HP; i++)
            GUI.Box(new Rect(10 + i * 30, 40, 30, 30), HealthIcon);

        GUI.Box(new Rect(160, 80, 30, 30), BombsIcon);
        GUI.Label(new Rect(210, 80, 20, 80), "x" + bombsAmmo.ToString(), style);

        GUI.Box(new Rect(10, 80, 60, 30), RocketsIcon);
        GUI.Label(new Rect(80, 80, 20, 80), "x" + rocketsAmmo.ToString(), style);
    }

    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    private void FixedUpdate()
    {
        HP = MyPlayerControl.Player.GetComponent<MyPlayerHealth>().HP;
        rocketsAmmo = MyPlayerControl.Player.rocketsAmmo;
        bombsAmmo = MyPlayerControl.Player.bombsAmmo;
    }

    private void Start()
    {
        style.fontSize = 30;
        style.normal.textColor = Color.white;
    }

    private void LateUpdate()
    {
        targetX = Mathf.Lerp(transform.position.x, MyPlayerControl.Player.transform.position.x, Time.deltaTime);
        targetX = Mathf.Clamp(targetX, xMin, xMax);
        
        targetY = Mathf.Lerp(transform.position.y, MyPlayerControl.Player.transform.position.y, Time.deltaTime);
        targetY = Mathf.Clamp(targetY, yMin, yMax);

        transform.position = new Vector3(targetX, targetY, transform.position.z);
    }
}
