using System;
using UnityEngine;
using UnityEngine.UI;

public class TimerTextView : MonoBehaviour
{
    [SerializeField] private Timer _timer;
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

    private void OnDisable()
    {
        _timer.OnChanged -= UpdateText;
        _timer.OnTimerStart -= OnStartView;
        _timer.OnTimerPause -= OnPauseView;
        _timer.OnTimerReset -= OnResetView;
    }

    private void UpdateText()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(_timer.CurrentTime);
        _timerText.text = timeSpan.ToString(@"mm\:ss\:ff");
    }

    private void OnStartView() => _timerText.color = Color.white;
    private void OnPauseView() => _timerText.color = Color.red;
    private void OnResetView() => _timerText.color = Color.black;
}
