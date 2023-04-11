using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallObj : MonoBehaviour
{
    public static ParticleSystem effect;
    public int _rank;           // 공의 레벨
    public int _value;          // 공이 합병할때 높은숫자의 공으로 합병

    bool _isGrowing;            // 공이 성장중인가
    bool _firstGrow;            // 공의 성장할때의 포지션 한정
    float _time;                // 공의 성장시간
    Vector3 _beforeScale;       // 공의 성장전 크기 저장
    bool _isTouch;              // 공이 오브젝트와 충돌하였는가
    //float _growSpeed = 1;

    private void Awake()
    {
        InitSetData();
    }
    private void Update()
    {
        if (_isGrowing)
        {
            Growing();
        }
    }

    public void InitSetData(bool upgrade = false)
    {
        if (upgrade)
        {
            _isGrowing = true;
            return;
        }
        _value = Random.Range(0, int.MaxValue);
        transform.localScale = Vector3.one * (_rank / 2f);
    }

    void Growing()
    {
        if (!_firstGrow)
        {
            _firstGrow = true;
            _beforeScale = transform.localScale;
        }
        _time += Time.deltaTime;
        transform.localScale += Vector3.one * (_rank - 1) * (_time * 0.5f);

        if (transform.localScale.x >= _rank / 2f)
        {
            _time = _rank / 2f;
            _isGrowing = false;
            _firstGrow = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            BallObj temp = other.GetComponent<BallObj>();
            if (_rank == temp._rank && _value > temp._value)
            {
                if (CreateBall._instance._isReady)
                    return;
                if (_rank == RuleManager._maxLevel)
                    return;
                Destroy(other.gameObject);
                EffectPlay();
                _isGrowing = true;
                _rank++;
                GameObject.Find("SoundManager").GetComponent<SoundManager>().SfxPlay(SoundManager.Sfx.RankUp);
                RuleManager._instance._score += 10 * _rank;

                Transform t = transform;
                GameObject go = Instantiate(RuleManager._instance._balls[_rank - 1]);
                go.transform.localScale = Vector3.one;
                BallObj ball = go.GetComponent<BallObj>();
                ball._rank = _rank;
                ball.InitSetData(true);
                go.transform.position = transform.position;

                Destroy(gameObject);
            }
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Box")
        {
            StartCoroutine("TouchRoutine");
        }

    }
    IEnumerator TouchRoutine()
    {
        if (_isTouch)
        {
            yield break;
        }

        _isTouch = true;

        GameObject.Find("SoundManager").GetComponent<SoundManager>().SfxPlay(SoundManager.Sfx.Touch_small);

        yield return new WaitForSeconds(0.2f);
        _isTouch = false;

    }

    void EffectPlay()
    {
        effect.transform.position = transform.position;
        effect.transform.localScale = transform.localScale;
        effect.Play();
    }

}
