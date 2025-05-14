using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSJ_Test
{
    /// <summary>
    /// Movement �׽�Ʈ������ ������ Ŭ���� �Դϴ�.
    /// Controller �����Ͻô� �в��� Movement ȣ����� �޼��� ���� �����ø�
    /// �ش� ������ �����ϼż� �����մϴ�.
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        public PlayerMovement _movement;
        public PlayerStatus _status;

        private void Update()
        {
            MoveTest();

            // IsAiming ����� �׽�Ʈ �ڵ�
            _status.IsAiming.Value = Input.GetKey(KeyCode.Mouse1); // ���� ShiftŰ�� ������ ������ true, �ƴϸ� false
        }

        /// <summary>
        /// �Ʒ� �޼��忡 ���� �ҽ��ڵ�� ���� ������� ����մϴ�.
        /// </summary>
        private void MoveTest()
        {
            // (ȸ�� ���� ��)�¿� ȸ���� ���� ���� ��ȯ
            Vector3 camRotateDir = _movement.SetAimRotation();

            float moveSpeed;
            if (_status.IsAiming.Value) moveSpeed = _status.WalkSpeed;
            else moveSpeed = _status.RunSpeed;

            Vector3 moveDir = _movement.SetMove(moveSpeed);
            _status.IsMoving.Value = (moveDir != Vector3.zero);  // ���ǽ� -> Zero�� �ƴ� ��� true, Zero�� ��� false -> �̵��ϰ� �ִ�

            // ��ü�� ȸ��
            Vector3 avatarDir;
            if (_status.IsAiming.Value) avatarDir = camRotateDir;
            else avatarDir = moveDir;

            _movement.SetAvatarRotation(avatarDir); // ��ü ȸ��
        }
    }
}

