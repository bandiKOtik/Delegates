using System;
using System.Collections;
using UnityEngine;

public class Timer
{
    public event Action<float> Changed;
    public event Action StartProcess;
    public event Action PauseProcess;
    public event Action ResetTime;

    public float CurrentTime { get; private set; }

    private MonoBehaviour _coroutineStarter;
    private Coroutine _currentProcess;

    public Timer(MonoBehaviour coroutineStarter)
        => _coroutineStarter = coroutineStarter;

    public void Start()
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
        CurrentTime = 0;

        Changed?.Invoke(CurrentTime);
        ResetTime?.Invoke();
    }

    public IEnumerator Process()
    {
        while (_coroutineStarter != null)
        {
            CurrentTime += Time.deltaTime;
            Changed?.Invoke(CurrentTime);
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
