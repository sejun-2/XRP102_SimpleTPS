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

        Vector2 currentRotation = new()  //� ������ �ִ°�
        {
            x = transform.rotation.eulerAngles.x,
            y = transform.rotation.eulerAngles.y
        };

        currentRotation.x += mouseDir.x; // x���� ����� ������ �� �ʿ� ����

        // y���� ���� ���� ������ �ɾ�� ��
        currentRotation.y = Mathf.Clamp(currentRotation.y + mouseDir.y, _minpitch, _maxpitch);

        // ĳ������ ������Ʈ�� ��쿡�� Y�� ȸ���� �ʿ���
        transform.rotation = Quaternion.Euler(0, currentRotation.x, 0); // Z���� ȸ���� �ʿ� ����

        // ������ ��� ���� ȸ�� �ݿ�
        Vector3 currentEuler = _aim.localEulerAngles;
        _aim.localEulerAngles = new Vector3(currentRotation.y, currentEuler.y, currentEuler.z); // Y���� ȸ���� �ʿ� ����

        // ȸ�� ���� ���� ��ȯ
        Vector3 rotateDirVector = transform.forward;
        rotateDirVector.y = 0; // Y���� ȸ���� �ʿ� ����
        return rotateDirVector.normalized;  //����ȭ �ؼ� ��ȯ
    }

    public void SetAvatarRotation(Vector3 direction)
    {
        if(direction == Vector3.zero) return; // �̵����� ���� ��� ȸ������ ����

        Quaternion targetRotation = Quaternion.LookRotation(direction); // �ٶ󺸴� �������� ȸ��

        _avatar.rotation = Quaternion.Lerp(_avatar.rotation, targetRotation, _playerStatus.RotateSpeed * Time.deltaTime); // �ε巴�� ȸ��
    }

    private Vector2 GetMouseDirection()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity;    // ���콺�� ���� ������
        float mouseY = -Input.GetAxis("Mouse Y") * _mouseSensitivity;   // ����( - ) ���콺 �� �Ʒ��� ���� �ؾ� ���ϴ� �������� ������

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
