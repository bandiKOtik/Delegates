using UnityEngine;

public class TimerExample : MonoBehaviour
{
    [SerializeField] private TimerTextView _textView;
    [SerializeField] private TimerController _controller;

    private Timer _timer;

    void Awake()
    {
        _timer = new Timer();

        _textView.Initialize(_timer);
        _controller.Initialize(_timer);
    }

    void Update()
    {
        _timer.UpdateLogic();
    }
}
