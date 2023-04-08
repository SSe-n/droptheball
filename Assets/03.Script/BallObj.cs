using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallObj : MonoBehaviour
{
    public static ParticleSystem effect;
    public int _rank;
    bool _isGrowing;
    bool _firstGrow;
    float _time;
    bool _isTouch;
    Vector3 _beforeScale;
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
            if (_rank == temp._rank)
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
