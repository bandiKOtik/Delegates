using System;
using System.Collections;
using UnityEngine;

public class Timer
{
    private MonoBehaviour _coroutineStarter;
    private Coroutine _currentProcess;

    public float CurrentTime { get; private set; }

    public Timer(MonoBehaviour coroutineStarter)
        => _coroutineStarter = coroutineStarter;

    public event Action<float> OnChanged;
    public event Action OnTimerStart;
    public event Action OnTimerPause;
    public event Action OnTimerReset;

    public void Start()
    {
        _currentProcess = _coroutineStarter.StartCoroutine(Process());

        OnTimerStart?.Invoke();
    }

    public void Pause()
    {
        Stop();

        OnTimerPause?.Invoke();
    }
    public void Reset()
    {
        Stop();

        OnChanged?.Invoke(CurrentTime);
        OnTimerReset?.Invoke();
    }

    public IEnumerator Process()
    {
        while (_coroutineStarter != null)
        {
            CurrentTime += Time.deltaTime;
            OnChanged?.Invoke(CurrentTime);
            yield return null;
        }
    }

    // private until implemented (it won't)
    private void Stop() => _coroutineStarter?.StopCoroutine(_currentProcess);
}
