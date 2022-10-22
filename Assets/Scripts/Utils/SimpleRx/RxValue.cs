using System;

namespace EasyGames.Utils
{
    public class RxValue<T> : IRxValue<T>
    {
        private T currentValue;
        private Action onChange;

        public T Value
        {
            get => currentValue;
            set => SetValue(value);
        }
        
        private event Action<T> changeEvent;

        public RxValue(T startValue=default, Action onChange=null)
        {
            currentValue = startValue;
            this.onChange = onChange;
        }

        private void SetValue(T value)
        {
            if (value.Equals(currentValue))
                return;
            currentValue = value;
            onChange?.Invoke();
            changeEvent?.Invoke(value);
        }

        public void Subscribe(Action<T> onChange)
        {
            changeEvent += onChange;
        }

        public void Unsubscribe(Action<T> onChange)
        {
            changeEvent -= onChange;
        }

        public IDisposable SubscribeRx(Action<T> onChange)
        {
            onChange?.Invoke(currentValue);
            Subscribe(onChange);
            return new Disposable(() => Unsubscribe(onChange));
        }
    }

    public interface IRxValue<T> : IRxValueRO<T>
    {
        new T Value { get; set; } 
    }

    public interface IRxValueRO<T>
    {
        T Value { get; }

        IDisposable SubscribeRx(Action<T> onChange);
        void Subscribe(Action<T> onChange);
        void Unsubscribe(Action<T> onChange);
        
        IDisposable SubscribeCached(Action<T, T> onChange)
        {
            T lastValue = this.Value;
            
            void OnChange(T newVal)
            {
                var oldVal = lastValue;
                lastValue = newVal;
                onChange?.Invoke(oldVal, newVal);
            }
            return SubscribeRx(OnChange);
        }
    }
}