using System;
using System.Collections;
using UnityEngine;

public class Timer
{
    public event Action StartProcess;
    public event Action PauseProcess;
    public event Action ResetTime;

    private ReactiveVariable<float> _currentTime;
    private MonoBehaviour _coroutineStarter;
    private Coroutine _currentProcess;

    public IReadOnlyVariable<float> CurrentTime => _currentTime;

    public Timer(MonoBehaviour coroutineStarter)
    {
        _coroutineStarter = coroutineStarter;

        _currentTime = new();
    }

    public void Play()
    {

        Stop();

        _currentProcess = _coroutineStarter.StartCoroutine(Process());

        StartProcess?.Invoke();
    }

    public void Pause()
    {
        Stop();

        PauseProcess?.Invoke();
    }

    public void Reset()
    {
        Stop();
        _currentTime.Value = 0;

        ResetTime?.Invoke();
    }

    public IEnumerator Process()
    {
        while (_coroutineStarter != null)
        {
            _currentTime.Value += Time.deltaTime;
            yield return null;
        }
    }

    private void Stop()
    {
        if (_currentProcess != null)
        {
            _coroutineStarter?.StopCoroutine(_currentProcess);
            _currentProcess = null;
        }
    }
}
