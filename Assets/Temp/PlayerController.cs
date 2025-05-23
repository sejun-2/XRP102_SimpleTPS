using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSJ_Test
{
    /// <summary>
    /// Movement 테스트용으로 구현한 클래스 입니다.
    /// Controller 구현하시는 분께서 Movement 호출관련 메서드 정리 끝나시면
    /// 해당 파일은 삭제하셔서 무방합니다.
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        public PlayerMovement _movement;
        public PlayerStatus _status;

        private void Update()
        {
            MoveTest();

            // IsAiming 변경용 테스트 코드
            _status.IsAiming.Value = Input.GetKey(KeyCode.Mouse1); // 마우스 오른쪽 버튼을 누르고 있으면 true, 아니면 false
        }

        /// <summary>
        /// 아래 메서드에 적힌 소스코드와 같은 방식으로 사용합니다.
        /// </summary>
        private void MoveTest()
        {
            // (회전 수행 후)좌우 회전에 대한 벡터 반환
            Vector3 camRotateDir = _movement.SetAimRotation();

            float moveSpeed;    // 이동 속도 변수 설정

            // 이동 속도는 PlayerStatus의 WalkSpeed와 RunSpeed를 통해 설정
            // PlayerStatus의 IsAiming 프로퍼티를 통해 조준 상태인지 확인, 참이면 걷기 속도, 거짓이면 달리기 속도
            if (_status.IsAiming.Value) moveSpeed = _status.WalkSpeed;
            else moveSpeed = _status.RunSpeed;

            Vector3 moveDir = _movement.SetMove(moveSpeed);
            _status.IsMoving.Value = (moveDir != Vector3.zero);  // 조건식 -> Zero가 아닐 경우 true, Zero일 경우 false -> 이동하고 있다

            // 몸체의 회전
            Vector3 avatarDir;
            if (_status.IsAiming.Value) avatarDir = camRotateDir;   // 조준 상태일 경우 카메라 회전 방향으로 몸체 회전
            else avatarDir = moveDir;   // 조준 상태가 아니라면, 이동 방향으로 몸체 회전

            _movement.SetAvatarRotation(avatarDir); // 몸체 회전
        }
    }
}

