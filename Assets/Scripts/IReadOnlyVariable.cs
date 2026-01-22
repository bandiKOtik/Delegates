using System;

public interface IReadOnlyVariable<TValue> where TValue : IEquatable<TValue>
{
    TValue Value { get; }
    event Action<TValue> Changed;
}
