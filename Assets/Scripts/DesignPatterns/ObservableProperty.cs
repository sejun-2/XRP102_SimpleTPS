using UnityEngine;
using UnityEngine.Events;

namespace DesignPattern
{
    public class ObservableProperty<T>
    {
        [SerializeField] private T _value;
        public T Value
        {
            get => _value;
            set
            {
                if (_value.Equals(value)) return;
                _value = value;
                Notify();
            }
        }
        private UnityEvent<T> _onValueChanged = new();  // 해당 값이 변경되었을 때 호출되는 이벤트 -> 새로만든다?

        public ObservableProperty(T value = default)    // 값이 안들어 오면 default로 기본값을 설정하는 생성자
        {
            _value = value;     // 값이 들어 온다면 들어온 값으로 초기화
        }

        public void Subscribe(UnityAction<T> action)    // 유니티 이벤트를 사용하여 구독하는 메서드
        {
            _onValueChanged.AddListener(action);
        }

        public void Unsubscribe(UnityAction<T> action)  // 구독 해제하는 메서드
        {
            _onValueChanged.RemoveListener(action);
        }

        public void UnsbscribeAll()     // 모든 구독 해제하는 메서드
        {
            _onValueChanged.RemoveAllListeners();
        }

        private void Notify()       // 구독된 대상자들에게 해당 값이 변경되었을 때 알림을 전송하는 메서드 -> 이벤트를 발생시킨다는 의미
        {
            _onValueChanged?.Invoke(Value);
        }
    }
}
