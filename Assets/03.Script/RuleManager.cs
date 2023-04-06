using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
    public static int _maxLevel = 8;            // 공의 최대 레벨
    public BallType _ballType;                  // 공의 타입
    public List<Material> _balls;               // 게임 시작시 입힌 스킨의 머티리얼
    public static float _reloadTime = 1;        // 공의 장전 시간
    public int _score;
    // 임시
    private void Awake()
    {
        _instance = this;
        Material[] go = Resources.LoadAll<Material>(_ballType.ToString());
        for (int i = 0; i < go.Length; i++)
        {
            _balls.Add(go[i]);
        }
    }

    public void Pause(int t)
    {
        Time.timeScale = t;
    }
}
