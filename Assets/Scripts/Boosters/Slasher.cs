using UnityEngine;
using UnityEngine.InputSystem;

public class Slasher : Booster
{
    [SerializeField] private Collider2D _collider2D;
    [SerializeField] private ParticleSystem _trailVFX;

    private SlashInput _slashInput;

    public override bool Enabled => _slashInput.asset.enabled;

    protected override void Awake()
    {
        base.Awake();
        _slashInput = new SlashInput();
    }

    private void Update()
    {
        if (_slashInput.Slash.SlashPress.inProgress)
        {
            Drag();
        }
    }

    public override void Enable()
    {
        _slashInput.Enable();
        StartTimer();
    }

    public override void Disable()
    {
        _collider2D.enabled = false;
        _trailVFX.Stop();
        _slashInput.Disable();
    }

    protected override void Subscribe()
    {
        base.Subscribe();
        _slashInput.Slash.SlashPress.performed += OnPressed;
        _slashInput.Slash.SlashRelease.performed += OnReleased;
    }

    protected override void Unsubscribe()
    {
        base.Unsubscribe();
        _slashInput.Slash.SlashPress.performed -= OnPressed;
        _slashInput.Slash.SlashRelease.performed -= OnReleased;
    }

    private void Drag()
    {
        transform.position = Camera.ScreenToWorldPoint(_slashInput.Slash.SlashPosition.ReadValue<Vector2>()).WithZ(0f);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out Balloon balloon))
        {
            balloon.Kill();
        }
    }

    private void OnPressed(InputAction.CallbackContext callbackContext)
    {
        _collider2D.enabled = true;
        _trailVFX.Play();
    }

    private void OnReleased(InputAction.CallbackContext callbackContext)
    {
        _collider2D.enabled = false;
        _trailVFX.Stop();
    }
}