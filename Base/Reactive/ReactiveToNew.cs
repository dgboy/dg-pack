namespace DG_Pack.Base.Reactive {
    public class ReactiveToNew<T> : IReactive<T> {
        public ReactiveToNew(T value = default) => _value = value;

        public event System.Action OnChanged;
        private T _value;

        public T Value {
            get => _value;
            set {
                if (_value.Equals(value))
                    return;

                _value = value;
                OnChanged?.Invoke();
            }
        }

        public override string ToString() => _value.ToString();
    }
}