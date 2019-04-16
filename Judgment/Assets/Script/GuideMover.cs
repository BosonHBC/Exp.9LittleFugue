using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideMover : MonoBehaviour
{


    bool bMoving;
    Transform _target;
    [SerializeField] float fYOffset = 22f;
    [SerializeField] Transform _origin;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!bMoving && _target != null)
        {
            transform.position = _target.position ;
            transform.position = new Vector3(transform.position.x, fYOffset, transform.position.z);
        }
    }

    public void NewTarget(Transform _tr)
    {
        _target = _tr;
        bMoving = true;
        MoveLerp(_tr.position, 0.3f);
    }
    public void GoBackToOrigin()
    {
        _target = null;
        MoveLerp(_origin.position, 0.4f);
    }

    void MoveLerp(Vector3 _end, float _fadeTime = 0.5f)
    {
        StartCoroutine(SimleLerper_v3(transform.position, _end, _fadeTime));
    }

    IEnumerator SimleLerper_v3(Vector3   _start, Vector3 _end, float _fadeTime = 0.5f)
    {
        float _timeStartFade = Time.time;
        float _timeSinceStart = Time.time - _timeStartFade;
        float _lerpPercentage = _timeSinceStart / _fadeTime;

        while (true)
        {
            _timeSinceStart = Time.time - _timeStartFade;
            _lerpPercentage = _timeSinceStart / _fadeTime;

            Vector3 currentValue = Vector3.Lerp(_start, _end, _lerpPercentage);

            currentValue = new Vector3(currentValue.x, fYOffset, currentValue.z);
            transform.position = currentValue;
            if (_lerpPercentage >= 1) break;


            yield return new WaitForEndOfFrame();
        }
        bMoving = false;
    }
}
