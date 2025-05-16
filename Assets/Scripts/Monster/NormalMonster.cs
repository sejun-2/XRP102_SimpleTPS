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
        // ������ ���� ����
        // ü�� ���
        // ü���� 0 ���ϰ� �Ǹ� ���� ó��
        Debug.Log($"Monster took {value} damage.");
    }
}
