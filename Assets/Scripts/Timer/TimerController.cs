using UnityEngine;

public class TimerController : MonoBehaviour
{
    private Timer _timer;

    public void Initialize(Timer timer) => _timer = timer;

    public void PlayButtonCall() => _timer.StartTimer();
    public void PauseButtonCall() => _timer.PauseTimer();
    public void ResetButtonCall() => _timer.ResetTimer();
}
