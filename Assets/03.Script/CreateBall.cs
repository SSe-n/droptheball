using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBall : MonoBehaviour
{
    [SerializeField] GameObject _prefab;    // �� ������Ʈ
    public GameObject _effectPrefab;
    public Transform _effectPos;

    bool _isReady = false;                  // ���� ����Ʈ�� �غ� ����� Ȯ��

    LayerMask lMask;                        // touchZone�� ���̾� ����ũ
    Camera _mainCam;
    GameObject _nowball;                    // ������ ��
    Vector3 _lastPosition = Vector3.zero;   // ������ touchZone�� ��ġ ��ġ
    float _time = 0;

    private void Start()
    {
        _mainCam = Camera.main;
        lMask = 1 << LayerMask.NameToLayer("TouchZone");
        _time = RuleManager._reloadTime;
    }
    void Update()
    {
        _time += Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && _time >= RuleManager._reloadTime)
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
    /// ���� ȭ��� �غ��Ų��.
    /// </summary>
    void ReadyBall()
    {
        if (!_isReady)
        {
            _isReady = true;
            _nowball = Instantiate(_prefab, GetScreenPoint(), Quaternion.LookRotation(Vector3.down));
            Color color = _nowball.GetComponentInChildren<MeshRenderer>().material.color;
            _nowball.GetComponentInChildren<MeshRenderer>().material.color =
                new Color(color.r, color.g, color.b, 0.1f);
            _nowball.GetComponent<Rigidbody>().useGravity = false;
        }
        else
        {
            _nowball.transform.position = GetScreenPoint();
        }   
    }
    /// <summary>
    /// ���� ����Ʈ����.
    /// </summary>
    void ThrowBall()
    {
        _nowball.GetComponent<Rigidbody>().useGravity = true;
        _nowball = null;
        _isReady = false;   

        GameObject _effectObj = Instantiate(_effectPrefab, _effectPos);
        ParticleSystem instantEffect = _effectObj.GetComponent<ParticleSystem>();
        BallObj.effect = instantEffect;
    }
    /// <summary>
    /// ��ġ��ǥ�� ����Ƽ������ ��ǥ�� ��ȯ��Ų��.
    /// </summary>
    /// <returns>��ġ��ǥ or ���������� Zone�� ��ġ�� ��ǥ</returns>
    Vector3 GetScreenPoint()
    {
        Ray ray = _mainCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayHit;
        Vector3 pos = Vector3.zero;
        if (Physics.Raycast(ray, out rayHit, Mathf.Infinity, lMask))
        {
            pos = rayHit.point;
            _lastPosition = pos;
        }
        else
            pos = _lastPosition;
        return pos;
    }
}
