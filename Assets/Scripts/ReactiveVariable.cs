using System;
using UnityEngine;

public class ReactiveVariable<TValue> : IReadOnlyVariable<TValue> where TValue : IEquatable<TValue>
{
    public event Action<TValue> Changed;

    private TValue _value;

    public ReactiveVariable(TValue value = default) => _value = value;

    public TValue Value
    {
        get => _value;
        set
        {
            TValue oldValue = _value;

            _value = value;

            if (_value.Equals(oldValue) == false)
                Changed?.Invoke(Value);
        }
    }
}