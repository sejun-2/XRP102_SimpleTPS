using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _avatar;
    [SerializeField] private Transform _aim;

    private Rigidbody _rigidbody;
    private PlayerStatus _playerStatus;

    [Header("Mouse Config")]
    [SerializeField][Range(-90, 0)] private float _minPitch;
    [SerializeField][Range(0, 90)] private float _maxPitch;
    [SerializeField][Range(0, 5)] private float _mouseSensitivity = 1;

    private Vector2 _currentRotation;

    private void Awake() => Init();

    private void Init()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerStatus = GetComponent<PlayerStatus>();
    }

    public Vector3 SetMove(float moveSpeed)     // Controller 에서 호출시 반환해서 보내기 위해 반환형을 Vecter3 형식으로 
    {
        Vector3 moveDirection = GetMoveDirection();

        Vector3 velocity = _rigidbody.velocity;
        velocity.x = moveDirection.x * moveSpeed;
        velocity.z = moveDirection.z * moveSpeed;

        _rigidbody.velocity = velocity;

        return moveDirection;   // 이동 방향 벡터 반환
    }

    public Vector3 SetAimRotation()
    {
        Vector2 mouseDir = GetMouseDirection();

        //  x축의 경우라면 제한을 걸 필요 없음
        _currentRotation.x += mouseDir.x;

        // y축의 경우엔 각도 제한을 걸어야 함.  Clamp 로 범위를 지정
        _currentRotation.y = Mathf.Clamp(
            _currentRotation.y + mouseDir.y,
            _minPitch,
            _maxPitch
            );

        // 캐릭터 오브젝트의 경우에는 Y축 회전만 반영
        transform.rotation = Quaternion.Euler(0, _currentRotation.x, 0);

        // 에임의 경우 상하 회전 반영
        Vector3 currentEuler = _aim.localEulerAngles;
        _aim.localEulerAngles = new Vector3(_currentRotation.y, currentEuler.y, currentEuler.z);

        // 회전 방향 벡터 반환
        Vector3 rotateDirVector = transform.forward;
        rotateDirVector.y = 0;
        return rotateDirVector.normalized;
    }

    public void SetAvatarRotation(Vector3 direction)    // 어느방향으로 회전할 것인지에 대한 방향 벡터를 받아서 회전하는 메서드
    {
        if (direction == Vector3.zero) return;  // 이동하지 않을 경우 회전하지 않음

        Quaternion targetRotation = Quaternion.LookRotation(direction); //어느 방향으로 회전할 것인지에 대한 쿼터니언을 반환

        _avatar.rotation = Quaternion.Lerp(
            _avatar.rotation,
            targetRotation,
            _playerStatus.RotateSpeed * Time.deltaTime
            );  // 보간을 통해 부드럽게 회전
    }

    private Vector2 GetMouseDirection()     // 마우스로 화면 이동
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity;
        float mouseY = -Input.GetAxis("Mouse Y") * _mouseSensitivity;   // (-) 붙여야 위아래로 움직임, 화면 반전

        return new Vector2(mouseX, mouseY);
    }
    
    // 벡터 그림 (수업 후)
    public Vector3 GetMoveDirection()
    {
        Vector3 input = GetInputDirection();

        Vector3 direction =
           (transform.right * input.x) + 
           (transform.forward * input.z);

        return direction.normalized;
    }

    public Vector3 GetInputDirection()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        return new Vector3(x, 0, z);
    }
}


















