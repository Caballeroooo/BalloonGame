using System;
using UnityEngine;

public class BalloonSpawner : MonoBehaviour
{
    [SerializeField] private Borders _borders;

    private BalloonPool _pool;
    private Range _currentSpawnTime;
    private float _currentTime;
    private float _nextSpawnTime;
    private bool _canSpawn;

    public event Action<Balloon> Spawned;
    public event Action<Balloon> TutorialSpawned;

    private void Update()
    {
        if (!_canSpawn)
            return;
        
        _currentTime += Time.deltaTime;
        if (_currentTime >= _nextSpawnTime)
        {
            _currentTime = 0;
            _nextSpawnTime = _currentSpawnTime.GetRandom();
            var balloon = Spawn(_borders.GetRandomPosition());
            Spawned?.Invoke(balloon);
        }
    }

    public void Initialize(BalloonPool pool)
    {
        _pool = pool;
    }

    public void SetCurrentSpawnTimeRange(Range spawnTimeRange)
    {
        _currentSpawnTime = spawnTimeRange;
        _nextSpawnTime = _currentSpawnTime.GetRandom();
    }

    public void SpawnTutorial()
    {
        var balloon = Spawn(_borders.GetStartTutorialPosition());
        TutorialSpawned?.Invoke(balloon);
    }

    public void StartSpawn()
    {
        _canSpawn = true;
    }

    public void StopSpawn()
    {
        _canSpawn = false;
    }

    private Balloon Spawn(Vector2 position)
    {
        var balloon = _pool.Get();
        balloon.Spawn(position);
        return balloon;
    }
}
