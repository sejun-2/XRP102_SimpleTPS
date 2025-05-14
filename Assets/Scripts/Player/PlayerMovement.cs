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

    public Vector3 SetMove(float moveSpeed)     // Controller ���� ȣ��� ��ȯ�ؼ� ������ ���� ��ȯ���� Vecter3 �������� 
    {
        Vector3 moveDirection = GetMoveDirection();

        Vector3 velocity = _rigidbody.velocity;
        velocity.x = moveDirection.x * moveSpeed;
        velocity.z = moveDirection.z * moveSpeed;

        _rigidbody.velocity = velocity;

        return moveDirection;   // �̵� ���� ���� ��ȯ
    }

    public Vector3 SetAimRotation()
    {
        Vector2 mouseDir = GetMouseDirection();

        //  x���� ����� ������ �� �ʿ� ����
        _currentRotation.x += mouseDir.x;

        // y���� ��쿣 ���� ������ �ɾ�� ��.  Clamp �� ������ ����
        _currentRotation.y = Mathf.Clamp(
            _currentRotation.y + mouseDir.y,
            _minPitch,
            _maxPitch
            );

        // ĳ���� ������Ʈ�� ��쿡�� Y�� ȸ���� �ݿ�
        transform.rotation = Quaternion.Euler(0, _currentRotation.x, 0);

        // ������ ��� ���� ȸ�� �ݿ�
        Vector3 currentEuler = _aim.localEulerAngles;
        _aim.localEulerAngles = new Vector3(_currentRotation.y, currentEuler.y, currentEuler.z);

        // ȸ�� ���� ���� ��ȯ
        Vector3 rotateDirVector = transform.forward;
        rotateDirVector.y = 0;
        return rotateDirVector.normalized;
    }

    public void SetAvatarRotation(Vector3 direction)    // ����������� ȸ���� �������� ���� ���� ���͸� �޾Ƽ� ȸ���ϴ� �޼���
    {
        if (direction == Vector3.zero) return;  // �̵����� ���� ��� ȸ������ ����

        Quaternion targetRotation = Quaternion.LookRotation(direction); //��� �������� ȸ���� �������� ���� ���ʹϾ��� ��ȯ

        _avatar.rotation = Quaternion.Lerp(
            _avatar.rotation,
            targetRotation,
            _playerStatus.RotateSpeed * Time.deltaTime
            );  // ������ ���� �ε巴�� ȸ��
    }

    private Vector2 GetMouseDirection()     // ���콺�� ȭ�� �̵�
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity;
        float mouseY = -Input.GetAxis("Mouse Y") * _mouseSensitivity;   // (-) �ٿ��� ���Ʒ��� ������, ȭ�� ����

        return new Vector2(mouseX, mouseY);
    }
    
    // ���� �׸� (���� ��)
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


















