using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPattern;

public class AudioManager : MonoBehaviour
{
    private AudioSource _bgmSource;
    private ObjectPool _sfxPool;

    [SerializeField] private List<AudioClip> _bgmList = new();
    [SerializeField] private SFXController _sfxPrefab;


    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _bgmSource = GetComponent<AudioSource>();

        _sfxPool = new ObjectPool(transform, _sfxPrefab, 10);
    }

    public void BgmPlay(int index)
    {
        if(0 <= index && index < _bgmList.Count)
        {
            _bgmSource.Stop();
            _bgmSource.clip = _bgmList[index];
            _bgmSource.Play();
        }
    }

    public SFXController GetSFX()
    {
        // Ǯ���� �����ͼ� ��ȯ
        PooledObject po = _sfxPool.PopPool();
        return po as SFXController;
    }

}
