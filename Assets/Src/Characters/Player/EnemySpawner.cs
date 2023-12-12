using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class Wave
{
    [SerializeField]
    public GameObject enemy;

    [SerializeField]
    public int enemyCount;

    [SerializeField]
    public float spawnTime;
}

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    public Wave[] waves;

    private int currentWave = -1;
    private int currentWaveEnemyCount;

    // Start is called before the first frame update

    public void Activate()
    {
        SpawnNext();
    }

    public void Reset()
    {
        CancelInvoke();
        this.currentWave = -1;
    }

    void SpawnNext()
    {
        if (waves.Length > currentWave + 1)
        {
            Invoke("SpawnCurrentWave", waves[currentWave + 1].spawnTime);
        }
    }

    void SpawnCurrentWave()
    {
        currentWave++;
        currentWaveEnemyCount = waves[currentWave].enemyCount;
        InvokeRepeating("SpawnEnemyInWave", 0, 0.5f);

        SpawnNext();
    }

    void SpawnEnemyInWave()
    {
        var enemy = Instantiate(waves[currentWave].enemy, transform.position, transform.rotation);
        enemy.GetComponent<SkeletonController>().Target = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>(); // TODO dynamic destination
        
        if (--currentWaveEnemyCount == 0) CancelInvoke("SpawnEnemyInWave");
    }
}
