using UnityEngine;

public class Tutorials : MonoBehaviour
{
    [SerializeField] private float _timeScalingDuration = 0.1f;

    public bool NeedShowBoosterTutorial(BoosterType type)
    {
        switch (type)
        {
            case BoosterType.Slasher:
            {
                if (string.IsNullOrEmpty(PlayerPrefsProvider.GetSlashTutorialShown()))
                {
                    StopTimeScale();
                    PlayerPrefsProvider.SetSlashTutorialShown();
                    return true;
                }

                break;
            }
            case BoosterType.MegaTap:
            {
                if (string.IsNullOrEmpty(PlayerPrefsProvider.GetMegaTapTutorialShown()))
                {
                    StopTimeScale();
                    PlayerPrefsProvider.SetMegaTapTutorialShown();
                    return true;
                }

                break;
            }
        }

        return false;
    }

    public void ResetTimeScale()
    {
        TimeScaler.Instance.StartChange(1, 0f);
    }

    private void StopTimeScale()
    {
        TimeScaler.Instance.StartChange(0, _timeScalingDuration);
    }
}