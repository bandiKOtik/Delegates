using System;
using UnityEngine;
using UnityEngine.UI;

public class TimerTextView : MonoBehaviour
{
    private Timer _timer;
    private Text _timerText;

    public void Initialize(Timer timer)
    {
        _timer = timer;
        _timerText = GetComponent<Text>();

        _timer.CurrentTime.Changed += UpdateText;
        _timer.StartProcess += OnTimerStart;
        _timer.PauseProcess += OnTimerPause;
        _timer.ResetTime += OnTimerReset;
    }

    private void OnDestroy()
    {
        _timer.CurrentTime.Changed -= UpdateText;
        _timer.StartProcess -= OnTimerStart;
        _timer.PauseProcess -= OnTimerPause;
        _timer.ResetTime -= OnTimerReset;
    }

    private void UpdateText(float time)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(time);
        _timerText.text = timeSpan.ToString(@"mm\:ss\:ff");
    }

    private void OnTimerStart() => _timerText.color = Color.white;

    private void OnTimerPause() => _timerText.color = Color.red;

    private void OnTimerReset() => _timerText.color = Color.black;
}
