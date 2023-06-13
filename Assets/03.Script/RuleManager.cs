using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class RuleManager : MonoBehaviour
{
    public static RuleManager _instance;
    /// <summary>
    /// ��ŲŸ��
    /// </summary>
    public enum BallType
    {
        Balls = 0,
        sdfS
    }
    public static int _maxLevel = 12;               // ���� �ִ� ����
    public BallType _ballType;                      // ���� Ÿ��
    //public List<GameObject> _balls;               
    public Dictionary<string, GameObject> _balls;   // ���� ���۽� ���� ��Ų�� ��Ƽ����
    public static float _reloadTime = 0.5f;         // ���� ���� �ð�
    public int _score;

    public float _maxTime = 25f;            // �ִ� �ð�
    public float _time = 0;                 // ���� ���� �ð�
    [SerializeField] Slider _timeSlider;    // Ÿ�� �����̴�
    [SerializeField] TextMeshProUGUI _timeText;

    // �ӽ�
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
