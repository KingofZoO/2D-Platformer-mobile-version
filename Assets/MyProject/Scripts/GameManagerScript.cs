using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject GamePanel;
    [SerializeField]
    private GameObject MenuPanel;

    private void ShowPanel(GameObject panel)
    {
        GamePanel.SetActive(panel == GamePanel);
        MenuPanel.SetActive(panel == MenuPanel);
    }

    public void ShowGamePanel()
    {
        ShowPanel(GamePanel);
    }

    public void ShowMenuPanel()
    {
        Time.timeScale = 0f;
        ShowPanel(MenuPanel);
    }
}
