using System;

namespace DG_Pack.Base.Reactive {
    public interface IReactive<T> {
        event Action OnChanged;
        T Value { get; set; }
    }
}