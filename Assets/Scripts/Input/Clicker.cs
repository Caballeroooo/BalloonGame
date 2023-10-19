using UnityEngine;
using UnityEngine.InputSystem;

public class Clicker : MonoBehaviour
{
    private Camera _camera;
    private TouchInput _touchInput;
    
    private void Awake()
    {
        _touchInput = new TouchInput();
        _camera = Camera.main;
    }

    private void OnEnable()
    {
        Subscribe();
    }

    private void OnDisable()
    {
        Unsubscribe();
    }

    public void Enable()
    {
        _touchInput.Enable();
    }

    public void Disable()
    {
        _touchInput.Disable();
    }
    

    private void Subscribe()
    {
        _touchInput.Touch.TouchPress.performed += OnTouched;
    }

    private void Unsubscribe()
    {
        _touchInput.Touch.TouchPress.performed -= OnTouched;
    }
    
    private void OnTouched(InputAction.CallbackContext context)
    {
        var rayHit =
            Physics2D.GetRayIntersection(
                _camera.ScreenPointToRay(_touchInput.Touch.TouchPosition.ReadValue<Vector2>()));

        if (rayHit.collider != null && rayHit.collider.gameObject.TryGetComponent(out IClickable clickable))
        {
            clickable.OnClick();
        }
    }
}
