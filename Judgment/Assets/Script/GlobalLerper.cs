using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class GlobalLerper : MonoBehaviour
{
    public static GlobalLerper instance;
    private void Awake()
    {
        if (instance != this || instance == null)
            instance = this;
    }

    public static void SimpleLerper_f(object _data, float _end, float _fadeTime = 0.5f)
    {

        instance.StartCoroutine(instance.SimleLerper_float(_data, _end, _fadeTime));
    }
    public static void SimpleLerper_v3(object _data, Vector3 _end, UnityAction<Vector3> _onLerping,float _fadeTime = 0.5f)
    {

        instance.StartCoroutine(instance.SimleLerper_v3(_data, _end, _onLerping,_fadeTime));
    }

    IEnumerator SimleLerper_float(object _start, float _end, float _fadeTime = 0.5f)
    {
        float _timeStartFade = Time.time;
        float _timeSinceStart = Time.time - _timeStartFade;
        float _lerpPercentage = _timeSinceStart / _fadeTime;

        float _tempStart = (float)_start;
        while (true)
        {
            _timeSinceStart = Time.time - _timeStartFade;
            _lerpPercentage = _timeSinceStart / _fadeTime;

            float currentValue = Mathf.Lerp(_tempStart, _end, _lerpPercentage);
            _start = currentValue;

            if (_lerpPercentage >= 1) break;


            yield return new WaitForEndOfFrame();
        }
    }
    IEnumerator SimleLerper_v3(object _start, Vector3 _end, UnityAction<Vector3> _onLerping,float _fadeTime = 0.5f)
    {
        float _timeStartFade = Time.time;
        float _timeSinceStart = Time.time - _timeStartFade;
        float _lerpPercentage = _timeSinceStart / _fadeTime;

        Vector3 _tempStart = (Vector3)_start;
        while (true)
        {
            _timeSinceStart = Time.time - _timeStartFade;
            _lerpPercentage = _timeSinceStart / _fadeTime;

            Vector3 currentValue = Vector3.Lerp(_tempStart, _end, _lerpPercentage);
            _start = currentValue;
            if (_onLerping!=null)
            {
                _onLerping.Invoke(currentValue);
            }
            Debug.Log(_start);
            if (_lerpPercentage >= 1) break;


            yield return new WaitForEndOfFrame();
        }
    }
}
