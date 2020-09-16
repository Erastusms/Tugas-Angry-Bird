using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public SlingShooter SlingShooter;
    public TrailController trailController;
    public List<Bird> Birds;
    public List<Enemy> enemies;

    // Untuk booster yg digunakan yellowBird
    private Bird _shotBird;
    public BoxCollider2D TapCollider;

    private bool _isGameEnded = false;

    public UIController UIController;

    void Start()
    {
        // Untuk mencari objek player selanjutnya
        for(int i = 0; i < Birds.Count; i++)
        {
            Birds[i].OnBirdDestroyed += ChangeBird;

            Birds[i].OnBirdShot += AssignTrail;
        }
        
        for(int i = 0; i <enemies.Count; i++)
        {
            enemies[i].OnEnemyDestroyed += CheckGameEnd;
        }

        TapCollider.enabled = false;
        SlingShooter.InitiateBird(Birds[0]);
        _shotBird = Birds[0];

    }

    public void ChangeBird()
    {
        TapCollider.enabled = false;

        if(_isGameEnded)
        {
            return;
        }

        Birds.RemoveAt(0);

        if (Birds.Count > 0)
            SlingShooter.InitiateBird(Birds[0]);

        if (Birds.Count == 0)
            UIController.gameOver();
    }

    public void CheckGameEnd(GameObject destroyedEnemy)
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].gameObject == destroyedEnemy)
            {
                enemies.RemoveAt(i);
                break;
            }
        }

        if (enemies.Count == 0)
        {
            _isGameEnded = true;
            UIController.nextLevel();
        }
    }

    public void AssignTrail (Bird bird)
    {
        trailController.SetBird(bird);
        StartCoroutine(trailController.SpawnTrail());
        TapCollider.enabled = true;
    }

    // Method ini dijalankan ketika klik mouse dilepaskan
    void OnMouseUp()
    {
        if(_shotBird != null)
        {
            _shotBird.OnTap();
        }
    }

}
