using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MegaTap : Booster
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private ParticleSystem _pressVFX;
    [SerializeField] private ParticleSystem _explosionVFX;
    [SerializeField] private float _killOrderStep = 0.06f;

    private MegaTapInput _megaTouchInput;

    public override bool Enabled => _megaTouchInput.asset.enabled;

    protected override void Awake()
    {
        base.Awake();
        _megaTouchInput = new MegaTapInput();
    }

    public override void Enable()
    {
        StartTimer();
        _megaTouchInput.Enable();
    }

    public override void Disable()
    {
        StopTimer();
        _megaTouchInput.Disable();
    }

    protected override void Subscribe()
    {
        base.Subscribe();
        _megaTouchInput.MegaTap.Press.performed += OnPressed;
        _megaTouchInput.MegaTap.Release.performed += OnReleased;
    }

    protected override void Unsubscribe()
    {
        base.Unsubscribe();
        _megaTouchInput.MegaTap.Press.performed -= OnPressed;
        _megaTouchInput.MegaTap.Release.performed -= OnReleased;
    }

    private void Update()
    {
        if (_megaTouchInput.MegaTap.Press.inProgress)
        {
            Drag();
        }
    }

    private void OnPressed(InputAction.CallbackContext context)
    {
        CameraEffects.Instance.Focus();
        _pressVFX.Play();
    }

    private void Drag()
    {
        transform.position = Camera.ScreenToWorldPoint(_megaTouchInput.MegaTap.PressPosition.ReadValue<Vector2>()).WithZ(0f);
    }

    private void OnReleased(InputAction.CallbackContext context)
    {
        _pressVFX.Stop();
        _explosionVFX.Play();
        var bottomLeftCorner = Camera.ScreenToWorldPoint(Vector2.zero);
        var topRightCorner = Camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        var overlappedColliders = Physics2D.OverlapAreaAll(bottomLeftCorner, topRightCorner, _layerMask);

        var activeBalloons = GetActiveBalloons(overlappedColliders);
        StartCoroutine(KillAllByOrder(activeBalloons));
        ForceComplete();
    }

    private List<Balloon> GetActiveBalloons(Collider2D[] overlappedColliders)
    {
        var result = new List<Balloon>();
        foreach (var collider in overlappedColliders)
        {
            if (collider.gameObject.TryGetComponent(out Balloon balloon))
            {
                result.Add(balloon);
            }
        }

        return result;
    }

    private IEnumerator KillAllByOrder(List<Balloon> balloons)
    {
        foreach (var balloon in balloons)
        {
            balloon.StopMoving();
            balloon.Kill();
            yield return new WaitForSecondsRealtime(_killOrderStep);
        }
    }
}