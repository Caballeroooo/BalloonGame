using UnityEngine;
using Random = UnityEngine.Random;

public class Borders : MonoBehaviour
{
    [SerializeField] private Transform _left;
    [SerializeField] private Transform _right;
    [SerializeField] private float _xOffset = 0.5f;

    private Camera _camera;

    private void OnEnable()
    {
        OrientationSwitcher.OrientationSwitched += OnOrientationSwitched;
    }

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void OnDisable()
    {
        OrientationSwitcher.OrientationSwitched -= OnOrientationSwitched;
    }

    public Vector2 GetRandomPosition()
    {
        return new Vector2(Random.Range(_left.position.x, _right.position.x), _left.position.y);
    }

    public Vector2 GetStartTutorialPosition()
    {
        return new Vector2(0f, _left.position.y);
    }

    private void OnOrientationSwitched(OrientationRatio ratio)
    {
        UpdateBordersPositions();
    }

    private void UpdateBordersPositions()
    {
        _left.position = _camera.ScreenToWorldPoint(Vector2.zero);
        _left.position += Vector3.right * _xOffset;
        _right.position = _camera.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));
        _right.position += Vector3.left * _xOffset;
    }
}
