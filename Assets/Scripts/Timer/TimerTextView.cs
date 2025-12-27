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

        _timer.OnChanged += UpdateText;
        _timer.OnTimerStart += OnStartView;
        _timer.OnTimerPause += OnPauseView;
        _timer.OnTimerReset += OnResetView;
    }

    private void OnDestroy()
    {
        _timer.OnChanged -= UpdateText;
        _timer.OnTimerStart -= OnStartView;
        _timer.OnTimerPause -= OnPauseView;
        _timer.OnTimerReset -= OnResetView;
    }

    private void UpdateText(float time)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(time);
        _timerText.text = timeSpan.ToString(@"mm\:ss\:ff");
    }

    private void OnStartView() => _timerText.color = Color.white;
    private void OnPauseView() => _timerText.color = Color.red;
    private void OnResetView() => _timerText.color = Color.black;
}
