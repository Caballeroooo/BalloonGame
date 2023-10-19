using UnityEngine;

public class BalloonKiller : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out Balloon balloon))
        {
            balloon.Kill();
        }
    }
}
