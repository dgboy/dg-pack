using System;

namespace DG_Pack.Base.Reactive {
    public class ReactiveAlways<T> : IReactive<T> {
        public ReactiveAlways(T value = default) => _value = value;

        public event Action OnChanged;
        private T _value;

        public T Value {
            get => _value;
            set {
                _value = value;
                OnChanged?.Invoke();
            }
        }

        public override string ToString() => _value.ToString();
    }
}