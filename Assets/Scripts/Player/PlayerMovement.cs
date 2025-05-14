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
    [SerializeField][Range(-90, 0)] private float _minpitch;
    [SerializeField][Range(0, 90)] private float _maxpitch;
    [SerializeField][Range(0, 5)] private float _mouseSensitivity = 1;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerStatus = GetComponent<PlayerStatus>();
    }

    public Vector3 SetMove(float moveSpeed)
    {
        Vector3 moveDirection = GetMoveDirection();

        Vector3 Velocity = _rigidbody.velocity;
        Velocity.x = moveDirection.x * moveSpeed;
        Velocity.z = moveDirection.z * moveSpeed;

        _rigidbody.velocity = Velocity;  

        return moveDirection;
    }

    public Vector3 SetAimRotation()
    {
        Vector2 mouseDir = GetMouseDirection();

        Vector2 currentRotation = new()  //어떤 각도에 있는가
        {
            x = transform.rotation.eulerAngles.x,
            y = transform.rotation.eulerAngles.y
        };

        currentRotation.x += mouseDir.x; // x축의 경우라면 제한을 걸 필요 없음

        // y축의 경우는 각도 제한을 걸어야 함
        currentRotation.y = Mathf.Clamp(currentRotation.y + mouseDir.y, _minpitch, _maxpitch);

        // 캐릭터의 오브젝트의 경우에는 Y축 회전만 필요함
        transform.rotation = Quaternion.Euler(0, currentRotation.x, 0); // Z축은 회전이 필요 없음

        // 에임의 경우 상하 회전 반영
        Vector3 currentEuler = _aim.localEulerAngles;
        _aim.localEulerAngles = new Vector3(currentRotation.y, currentEuler.y, currentEuler.z); // Y축은 회전이 필요 없음

        // 회전 방향 벡터 반환
        Vector3 rotateDirVector = transform.forward;
        rotateDirVector.y = 0; // Y축은 회전이 필요 없음
        return rotateDirVector.normalized;  //정규화 해서 반환
    }

    public void SetAvatarRotation(Vector3 direction)
    {
        if(direction == Vector3.zero) return; // 이동하지 않을 경우 회전하지 않음

        Quaternion targetRotation = Quaternion.LookRotation(direction); // 바라보는 방향으로 회전

        _avatar.rotation = Quaternion.Lerp(_avatar.rotation, targetRotation, _playerStatus.RotateSpeed * Time.deltaTime); // 부드럽게 회전
    }

    private Vector2 GetMouseDirection()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity;    // 마우스의 센서 설정값
        float mouseY = -Input.GetAxis("Mouse Y") * _mouseSensitivity;   // 반전( - ) 마우스 위 아래는 반전 해야 원하는 방향으로 움직임

        return new Vector2(mouseX, mouseY);
    }

    public Vector3 GetMoveDirection()
    {
        Vector3 input = GetInputDirection();
        Vector3 direction = (transform.right * input.x) + (transform.forward * input.z);

        return direction.normalized;
    }

    public Vector3 GetInputDirection()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        return new Vector3(x, 0, z);
    }

}
