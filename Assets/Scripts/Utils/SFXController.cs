using System.Collections;
using System.Collections.Generic;
using DesignPattern;
using UnityEngine;

public class SFXController : PooledObject
{
    private AudioSource _audioSource;
    private float _currentCount;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _audioSource = GetComponent<AudioSource>();

        //_audioSource.playOnAwake = true;
    }

    private void Update()
    {
        // �� �������� ���ŵɶ������� �ð� = DeltaTime
        _currentCount -= Time.deltaTime;    // �����Ӹ��� �ð� ���.

        if(_currentCount <= 0)
        {
            //_audioSource.Stop();
            //_audioSource.clip = null;
            ReturnPool();
        }
    }

    public void Play(AudioClip clip)
    {
        _audioSource.Stop();
        _audioSource.clip = clip;
        _audioSource.Play();

        _currentCount = clip.length;         // lenght ����� Ŭ���� ��������.?
    }

}
