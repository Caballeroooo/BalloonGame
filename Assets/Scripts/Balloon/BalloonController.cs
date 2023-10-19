using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BalloonController : MonoBehaviour
{
    [SerializeField] private BalloonSpawner _spawner;
    [SerializeField] private BalloonPool _pool;
    [SerializeField] private BalloonKillVFXPool _killVFXPool;
    [SerializeField] private Transform _upBorder;
    [SerializeField] private Transform _upTutorialBorder;
    [SerializeField] private float _finalAnimationKillStep = 0.06f;

    private List<Balloon> _activeBalloons;
    private Camera _camera;
    private BalloonSettings _balloonSettings;
    private Range _currentSpeedRange;
    private float _tutorialBalloonSpeed;

    public event Action<Vector2> BalloonKilled;
    public event Action BalloonMissed;
    public event Action TutorialMoveCompleted;
    public event Action FinishAnimationCompleted;

    private void OnEnable()
    {
        Subscribe();
    }

    private void Awake()
    {
        _activeBalloons = new List<Balloon>();
        _spawner.Initialize(_pool);
        _camera = Camera.main;
        _balloonSettings = SettingsProvider.Get<BalloonSettings>();
    }

    private void OnDisable()
    {
        Unsubscribe();
    }

    public void StartSpawn()
    {
        _spawner.StartSpawn();
    }

    public void SpawnTutorial(float speed)
    {
        _tutorialBalloonSpeed = speed;
        _spawner.SpawnTutorial();
    }

    public void SetCurrentSpeedRange(Range speedRange)
    {
        _currentSpeedRange = speedRange;
    }

    public void SetCurrentSpawnTimeRange(Range spawnTimeRange)
    {
        _spawner.SetCurrentSpawnTimeRange(spawnTimeRange);
    }

    public void FinishGame()
    {
        Unsubscribe();
        _spawner.StopSpawn();
        StartCoroutine(PlayGameOverAnimation());
    }

    private void Subscribe()
    {
        _spawner.Spawned += OnBalloonSpawned;
        _spawner.TutorialSpawned += OnTutorialSpawned;
        OrientationSwitcher.OrientationSwitched += OnOrientationSwitched;
    }

    private void Unsubscribe()
    {
        _spawner.Spawned -= OnBalloonSpawned;
        _spawner.TutorialSpawned -= OnTutorialSpawned;
        OrientationSwitcher.OrientationSwitched -= OnOrientationSwitched;

        foreach (var balloon in _activeBalloons)
        {
            balloon.MoveCompleted -= OnBalloonMoveCompleted;
            balloon.Killed -= OnBalloonKilled;
            balloon.ForceKilled -= OnBalloonForceKilled;
        }
    }

    private void OnBalloonSpawned(Balloon balloon)
    {
        InitializeBalloon(balloon);
        LaunchBalloon(balloon, _upBorder.position, _currentSpeedRange.GetRandom());
        balloon.MoveCompleted += OnBalloonMoveCompleted;
    }

    private void OnTutorialSpawned(Balloon balloon)
    {
        InitializeBalloon(balloon);
        LaunchBalloon(balloon, _upTutorialBorder.position, _tutorialBalloonSpeed);
        balloon.MoveCompleted += OnBalloonTutorialMoveCompleted;
    }
    
    private void InitializeBalloon(Balloon balloon)
    {
        _activeBalloons.Add(balloon);
        balloon.SetColor(_balloonSettings.GetRandomColor());
        balloon.Show();
        balloon.Killed += OnBalloonKilled;
        balloon.ForceKilled += OnBalloonForceKilled;
    }

    private void LaunchBalloon(Balloon balloon, Vector2 endPoint, float speed)
    {
        balloon.StartMoving(endPoint, speed);
    }

    private void OnBalloonKilled(Balloon balloon)
    {
        balloon.Killed -= OnBalloonKilled;

        _pool.Return(balloon);
        _activeBalloons.Remove(balloon);

        PlayKillVFX(balloon);

        BalloonKilled?.Invoke(balloon.Position);
    }

    private void OnBalloonMoveCompleted(Balloon balloon)
    {
        balloon.MoveCompleted -= OnBalloonMoveCompleted;

        _pool.Return(balloon);
        _activeBalloons.Remove(balloon);
        balloon.Hide();
        BalloonMissed?.Invoke();
    }

    private void OnBalloonTutorialMoveCompleted(Balloon balloon)
    {
        balloon.MoveCompleted -= OnBalloonTutorialMoveCompleted;

        TutorialMoveCompleted?.Invoke();
    }

    private IEnumerator PlayGameOverAnimation()
    {
        var balloons = new List<Balloon>();
        balloons.AddRange(_activeBalloons);

        StopActiveBalloons(balloons);

        foreach (var balloon in balloons)
        {
            balloon.Kill();
            PlayKillVFX(balloon);
            yield return new WaitForSeconds(_finalAnimationKillStep);
        }

        FinishAnimationCompleted?.Invoke();
    }

    private void StopActiveBalloons(List<Balloon> balloons)
    {
        foreach (var balloon in balloons)
        {
            balloon.StopMoving();
        }
    }

    private void PlayKillVFX(Balloon balloon)
    {
        var killVFX = _killVFXPool.Get();
        killVFX.transform.position = balloon.transform.position;
        killVFX.SetColor(balloon.Color);
        killVFX.Play();
        _killVFXPool.Return(killVFX);
    }

    private void OnOrientationSwitched(OrientationRatio ratio)
    {
        StartCoroutine(SkipFrame(ForceKillNonVisibleBalloons));
    }

    private void OnBalloonForceKilled(Balloon balloon)
    {
        balloon.ForceKilled -= OnBalloonForceKilled;
        _pool.Return(balloon);
        _activeBalloons.Remove(balloon);
        balloon.Hide();
    }

    private IEnumerator SkipFrame(Action action)
    {
        yield return null;
        action?.Invoke();
    }

    private void ForceKillNonVisibleBalloons()
    {
        var balloons = new List<Balloon>();
        balloons.AddRange(_activeBalloons);
        var planes = GeometryUtility.CalculateFrustumPlanes(_camera);

        foreach (var balloon in balloons.Where(balloon => !GeometryUtility.TestPlanesAABB(planes, balloon.Bounds)))
        {
            balloon.ForceKill();
        }
    }
}