using UnityEngine;
using UnityEngine.UI;

public class CounterView : MonoBehaviour
{
    private ReactiveVariable<int> _variable;
    private Text _counterText;

    public void Initialize(ReactiveVariable<int> variable)
    {
        _counterText = GetComponentInChildren<Text>();

        if (variable != null)
            variable.Changed -= UpdateText;

        _variable = variable;

        if (isActiveAndEnabled && _variable != null)
        {
            variable.Changed += UpdateText;
            UpdateText(_variable.Value);
        }
    }

    private void Start()
    {
        if (_variable != null)
        {
            _variable.Changed += UpdateText;
            UpdateText(_variable.Value);
        }
    }

    private void OnEnable()
    {
        if (_variable != null)
        {
            _variable.Changed += UpdateText;
            UpdateText(_variable.Value);
        }
    }

    private void OnDestroy()
    {
        _variable.Changed -= UpdateText;
    }

    public void UpdateText(int value)
    {
        if (_counterText != null)
            _counterText.text = value.ToString();
        else
            Debug.LogError("Counter text is null!");
    }
}
