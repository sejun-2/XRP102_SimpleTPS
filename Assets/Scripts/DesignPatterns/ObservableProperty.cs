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
        private UnityEvent<T> _onValueChanged = new();  // �ش� ���� ����Ǿ��� �� ȣ��Ǵ� �̺�Ʈ -> ���θ����?

        public ObservableProperty(T value = default)    // ���� �ȵ�� ���� default�� �⺻���� �����ϴ� ������
        {
            _value = value;     // ���� ��� �´ٸ� ���� ������ �ʱ�ȭ
        }

        public void Subscribe(UnityAction<T> action)    // ����Ƽ �̺�Ʈ�� ����Ͽ� �����ϴ� �޼���
        {
            _onValueChanged.AddListener(action);
        }

        public void Unsubscribe(UnityAction<T> action)  // ���� �����ϴ� �޼���
        {
            _onValueChanged.RemoveListener(action);
        }

        public void UnsbscribeAll()     // ��� ���� �����ϴ� �޼���
        {
            _onValueChanged.RemoveAllListeners();
        }

        private void Notify()       // ������ ����ڵ鿡�� �ش� ���� ����Ǿ��� �� �˸��� �����ϴ� �޼��� -> �̺�Ʈ�� �߻���Ų�ٴ� �ǹ�
        {
            _onValueChanged?.Invoke(Value);
        }
    }
}
