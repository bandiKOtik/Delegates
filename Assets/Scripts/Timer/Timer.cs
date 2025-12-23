using System;
using UnityEngine;

public class Timer
{
    [SerializeField] private TimerController _controller;
    public float CurrentTime { get; private set; }
    private bool _isProcessing;

    public event Action OnChanged;
    public event Action OnTimerStart;
    public event Action OnTimerPause;
    public event Action OnTimerReset;

    public void StartTimer()
    {
        _isProcessing = true;
        OnTimerStart?.Invoke();
    }

    public void PauseTimer()
    {
        _isProcessing = false;
        OnTimerPause?.Invoke();
    }
    public void ResetTimer()
    {
        CurrentTime = 0;
        OnChanged?.Invoke();
        OnTimerReset?.Invoke();
    }

    public void UpdateLogic()
    {
        if (_isProcessing == false)
            return;

        CurrentTime += Time.deltaTime;
        OnChanged?.Invoke();
    }
}
