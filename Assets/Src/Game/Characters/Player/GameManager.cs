using UnityEngine;

public enum GameState
{
    Idle,
    InProgress,
    Victory,
    Defeat,
}

public class GameManager : MonoBehaviour
{
    public PlayerSpawner playerSpawner;
    public GameObject enemySpawnerParent;
    EnemySpawner[] enemySpawners;

    void Start()
    {
        enemySpawners = enemySpawnerParent.GetComponentsInChildren<EnemySpawner>();

        SetupGame();
    }
    
    void SetupGame()
    {
        playerSpawner.SpawnPlayer();
        
        foreach (var spawner in enemySpawners)
        {
            spawner.Reset();
            spawner.Activate();
        }
    }
}
