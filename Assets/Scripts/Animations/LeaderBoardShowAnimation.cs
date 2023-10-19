using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LeaderBoardShowAnimation : MonoBehaviour
{
    private LeaderBoardAnimationSettings _settings;
    private Sequence _sequence;

    private void Awake()
    {
        _settings = SettingsProvider.Get<LeaderBoardAnimationSettings>();
        _sequence = DOTween.Sequence();
        _sequence.SetAutoKill(false);
        _sequence.Pause();
    }

    private void OnDestroy()
    {
        _sequence.Kill();
    }

    public void Show(List<LeaderBoardLineView> lines)
    {
        Show(lines, lines.Count);
    }

    public void Show(List<LeaderBoardLineView> lines, int count)
    {
        if (_sequence.playedOnce)
        {
            _sequence.Restart();
        }
        else
        {
            for (int i = count - 1; i >= 0; i--)
            {
                _sequence.Join(lines[i].Show(_settings.LineShowDuration)).SetDelay(_settings.LineShowDelay);
            }

            _sequence.Play();
        }
    }

    public void PrepareLinesPosition(List<LeaderBoardLineView> lines)
    {
        foreach (var line in lines)
        {
            line.PreparePosition(_settings.LineXPositionOffset);
        }
    }
}
