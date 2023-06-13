using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class RuleManager : MonoBehaviour
{
    public static RuleManager _instance;
    /// <summary>
    /// 스킨타입
    /// </summary>
    public enum BallType
    {
        Balls = 0,
        sdfS
    }
    public static int _maxLevel = 12;               // 공의 최대 레벨
    public BallType _ballType;                      // 공의 타입
    //public List<GameObject> _balls;               
    public Dictionary<string, GameObject> _balls;   // 게임 시작시 입힌 스킨의 머티리얼
    public static float _reloadTime = 0.5f;         // 공의 장전 시간
    public int _score;

    public float _maxTime = 25f;            // 최대 시간
    public float _time = 0;                 // 현재 남은 시간
    [SerializeField] Slider _timeSlider;    // 타임 슬라이더
    [SerializeField] TextMeshProUGUI _timeText;

    // 임시
    private void Awake()
    {
        _instance = this;
        _balls = new Dictionary<string, GameObject>();  
        GameObject[] go = Resources.LoadAll<GameObject>(_ballType.ToString());
        Debug.Log(go.Length);
        for (int i = 0; i < go.Length; i++)
        {
            _balls.Add(go[i].name, go[i]);
            Debug.Log(go[i].name);
        }
        _time = _maxTime;
    }
    private void Update()
    {
        _time -= Time.deltaTime;
        _timeSlider.value = _time / _maxTime;
        _timeText.text = _time.ToString("N1");
    }
    public void Pause(int t)
    {
        Time.timeScale = t;
    }

}
