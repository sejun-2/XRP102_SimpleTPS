using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent] // 하나의 오브젝트에 여러개 붙지 못하게 함.
public class ReferenceProvider : MonoBehaviour
{
    [SerializeField] private Component _component;

    private void Awake() => ReferenceRegistry.Register(this);
    private void OnDestroy() => ReferenceRegistry.Unregister(this);

    public T GetAs<T>() where T : Component
    {
        // 여기에서도 예외처리 필요.
        return _component as T;
    }
}
