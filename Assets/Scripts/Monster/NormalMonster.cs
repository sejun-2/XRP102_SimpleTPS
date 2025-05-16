using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPattern;
using UnityEngine.AI;

public class NormalMonster : Monster, IDamagable
{
    private ObservableProperty<bool> IsMoveing = new();

    private NavMeshAgent _navMeshAgent;

    [SerializeField] private Transform _targetTransform;


    public void TakeDamage(int value)
    {
        // 데미지 판정 구현
        // 체력 깎고
        // 체력이 0 이하가 되면 죽음 처리
        Debug.Log($"Monster took {value} damage.");
    }
}
