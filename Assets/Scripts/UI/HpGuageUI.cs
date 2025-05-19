using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpGuageUI : MonoBehaviour
{
    [SerializeField] private Image _image;
    private Transform _cameraTransform;

    private void Awake() => Init();
    private void LateUpdate() => SetUIForwardVector(_cameraTransform.forward);

    private void Init()
    {
        _cameraTransform = Camera.main.transform;
    }

    // UI 게이지의 FillAmount를 표시 대상의 HP로 설정
    // 현재 수치 / 최대 수치
    public void SetImageFillAmount(float value)
    {
        _image.fillAmount = value;
    }

    private void SetUIForwardVector(Vector3 target)
    {
        transform.forward = target;
    }

    // UI를 현재 카메라의 정면으로 회전시킴. 즉, 카메라가 바라보는 방향 벡터를 적용
    //private void SetUIRotate2()
    //{
    //    transform.forward = Camera.main.transform.forward;
    //}
}
