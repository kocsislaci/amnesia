using System.Collections;
using System.Collections.Generic;
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

    GameState state = GameState.Idle;

    // Start is called before the first frame update
    void Start()
    {
        enemySpawners = enemySpawnerParent.GetComponentsInChildren<EnemySpawner>();

        SetupGame();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SetupGame()
    {
        if (state != GameState.Idle)
        {
            ClearGame();
        }

        playerSpawner.SpawnPlayer();

        foreach (var spawner in enemySpawners)
        {
            spawner.Activate();
        }

        state = GameState.InProgress;
    }

    void ClearGame()
    {
        // TODO remove player 
        // TODO remove enemies
        foreach (var spawner in enemySpawners)
        {
            spawner.Reset();
        }
    }
}
