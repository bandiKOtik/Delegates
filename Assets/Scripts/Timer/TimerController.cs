using UnityEngine;

public class TimerController : MonoBehaviour
{
    private Timer _timer;

    public void Initialize(Timer timer) => _timer = timer;

    public void PlayButtonCall() => _timer.Start();

    public void PauseButtonCall() => _timer.Pause();

    public void ResetButtonCall() => _timer.Reset();
}
