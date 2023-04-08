using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBall : MonoBehaviour
{
    static CreateBall _unique;
    public static CreateBall _instance
    {
        get { return _unique; }
    }
    public GameObject _effectPrefab;
    public Transform _effectPos;

    public bool _isReady = false;                  // 공을 떨어트릴 준비가 됬는지 확인
    bool _isZone = false;

    LayerMask lMask;                        // touchZone의 레이어 마스크
    Camera _mainCam;
    GameObject _nowball;                    // 생성될 볼
    Vector3 _lastPosition = Vector3.zero;   // 마지막 touchZone의 터치 위치
    float _time = 0;

    private void Start()
    {
        _mainCam = Camera.main;
        lMask = 1 << LayerMask.NameToLayer("TouchZone");
        _time = RuleManager._reloadTime;
        _unique = this;
    }
    void Update()
    {
        _time += Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && _time >= RuleManager._reloadTime && Time.timeScale != 0)
        {
            ReadyBall();
        }
        else if (Input.GetMouseButtonUp(0) && _isReady)
        {
            ThrowBall();
            _time = 0;
        }
        if (_isReady)
        {
           _nowball.transform.position = GetScreenPoint();
        }
    }
    /// <summary>
    /// 공을 화면상에 준비시킨다.
    /// </summary>
    void ReadyBall()
    {
        if (!_isReady)
        {
            _isReady = true;
            //int level = LevelPercent();
            int level = LevelPercent();
            _nowball = Instantiate(RuleManager._instance._balls[level], GetScreenPoint(), Quaternion.LookRotation(Vector3.down));
            _nowball.GetComponent<Rigidbody>().useGravity = false;
            BallObj temp = _nowball.GetComponent<BallObj>();
            temp._rank = level + 1;
            temp.InitSetData();
            if (!_isZone)
                ThrowBall(true);
        }
        else
        {
            _nowball.transform.position = GetScreenPoint();
        }   
    }
    /// <summary>
    /// 공을 떨어트린다.
    /// </summary>
    void ThrowBall(bool notZone = false)
    {
        if (notZone)
        {
            Destroy(_nowball);
            _isReady = false;
            return;
        }
        _nowball.GetComponent<Rigidbody>().useGravity = true;
        _nowball = null;
        _isReady = false;
        _isZone = false;

        GameObject _effectObj = Instantiate(_effectPrefab, _effectPos);
        ParticleSystem instantEffect = _effectObj.GetComponent<ParticleSystem>();
        BallObj.effect = instantEffect;
    }
    /// <summary>
    /// 터치좌표를 유니티공간의 좌표로 변환시킨다.
    /// </summary>
    /// <returns>터치좌표 or 마지막으로 Zone에 터치된 좌표</returns>
    Vector3 GetScreenPoint()
    {
        Ray ray = _mainCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayHit;
        Vector3 pos = Vector3.zero;
        if (Physics.Raycast(ray, out rayHit, Mathf.Infinity, lMask))
        {
            pos = rayHit.point;
            _lastPosition = pos;
            _isZone = true;
        }
        else if (_isZone)
            pos = _lastPosition;
        return pos;
    }

    int LevelPercent()
    {
        int percent = Random.Range(1, 11);
        if (percent <= 2)
            percent = 3;
        else if (percent > 2 && percent <= 4)
            percent = 2;
        else if (percent > 4 && percent <= 7)
            percent = 1;
        else
            percent = 0;
        return percent;
    }

}
