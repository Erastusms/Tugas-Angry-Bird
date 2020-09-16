using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject panelNextLevel;
    public GameObject panelGameOver;

    private Scene currActiveScene;
    // Start is called before the first frame update
    void Start()
    {
        currActiveScene = SceneManager.GetActiveScene();
    }

    public void restartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(currActiveScene.name);
    }

    public void nextLevel()
    {
        panelNextLevel.SetActive(true);
    }

    public void gameOver()
    {
        panelGameOver.SetActive(true);
    }
}
