using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour
{
    private Transform _camPos;
    [SerializeField] private Transform[] _backgrounds;

    [SerializeField] private float _smoothness;
    private float[] _parLvl;
    private Vector3 _prevCamPos;


    private void Awake()
    {
        _camPos = Camera.main.transform;
    }
    void Start()
    {
        _prevCamPos = _camPos.position;
        _parLvl = new float[_backgrounds.Length];

        for(int i = 0; i < _backgrounds.Length; i++)
        {
            _parLvl[i] = _backgrounds[i].position.z;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < _backgrounds.Length; i++)
        {
            float parMove = (_prevCamPos.x - _camPos.position.x) * _parLvl[i] * Time.deltaTime;
            Vector3 backgroundTargetPos = new Vector3(_backgrounds[i].position.x + parMove, 
                _backgrounds[i].position.y, _backgrounds[i].position.z);

            _backgrounds[i].position = Vector3.Lerp(_backgrounds[i].position, backgroundTargetPos, _smoothness);
        }
        _prevCamPos = _camPos.position;
    }
}
