using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] PlayerController _player;
    [SerializeField] EnemyController _enemyPrefab;
    [SerializeField] HordeController _hordePrefab;

    [SerializeField] EnemyData Data, HordeData;

    [SerializeField] float _spawnInterval;
    [SerializeField] float _hordeSpawnInterval;

    [SerializeField] int _enemyLimit;
    [SerializeField] int _hordeLimit;
    [SerializeField] int _hordeSize;

    Queue<EnemyController> _availableEnemies = new();
    Queue<HordeController> _availableHorde = new();
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Start()
    {
        InstantiateEnemies(Data, HordeData);
        InvokeRepeating(nameof(SpawnEnemy), 1f, _spawnInterval);
        InvokeRepeating(nameof(SpawnHorde), 60f, _hordeSpawnInterval);
    }

    private void InstantiateEnemies(EnemyData data, EnemyData hordeData)
    {
        Data = data;
        HordeData = hordeData;

        for (int i = 0; i < _enemyLimit; i++)
        {
            //spawn enemies
            var enemy = Instantiate(_enemyPrefab, transform);
            enemy.Initialize(_player.transform, this, data);
            enemy.gameObject.SetActive(false);
            _availableEnemies.Enqueue(enemy);
        }

        for (int i = 0; i < _hordeLimit; i++)
        {
            var hordeEnemy = Instantiate(_hordePrefab, transform);
            hordeEnemy.Initialize(_player.transform, this, hordeData);
            hordeEnemy.gameObject.SetActive(false);
            _availableHorde.Enqueue(hordeEnemy);
        }
    }

    public void ReturnEnemyToPool(EnemyController enemy)
    {
        _availableEnemies.Enqueue(enemy);
        print($"{enemy.name} returned to queue.");
    }

    public void ReturnHordeToPool(HordeController horde)
    {
        _availableHorde.Enqueue(horde);
        print($"{horde.name} returned to queue.");
    }

    private void SpawnEnemy()
    {
        //coordinates outside of camera

        var spawnX = Random.Range(0f, 1f);
        if (spawnX < 0.5f)
        {
            spawnX = 0 - Random.Range(0f, 1f);
        }
        else
        {
            spawnX = 1 + Random.Range(0f, 1f);
        }

        var spawnY = Random.Range(0f, 1f);
        if (spawnY < 0.5f)
        {
            spawnY = 0 - Random.Range(0f, 1f);
        }
        else
        {
            spawnY = 1 + Random.Range(0f, 1f);
        }

        var spawnPosition = _camera.ViewportToWorldPoint(new(spawnX, spawnY, 10f));

        if (_availableEnemies.Count <= 0)
        {
            return;
        }

        var enemy = _availableEnemies.Dequeue();
        enemy.transform.position = spawnPosition;
        enemy.gameObject.SetActive(true);
    }

    private void SpawnHorde()
    {
        var spawnX = Random.Range(0f, 1f);
        if (spawnX < 0.5f)
        {
            spawnX = 0 - Random.Range(0f, 1f);
        }
        else
        {
            spawnX = 1 + Random.Range(0f, 1f);
        }

        var spawnY = Random.Range(0f, 1f);
        if (spawnY < 0.5f)
        {
            spawnY = 0 - Random.Range(0f, 1f);
        }
        else
        {
            spawnY = 1 + Random.Range(0f, 1f);
        }

        var spawnPosition = _camera.ViewportToWorldPoint(new(spawnX, spawnY, 10f));

        if (_availableEnemies.Count <= 0)
        {
            return;
        }

        for (int i = 0; i < _hordeSize; i++)
        {

            var enemy = _availableHorde.Dequeue();
            enemy.transform.position = spawnPosition;
            enemy.gameObject.SetActive(true);
        }
    }


}
