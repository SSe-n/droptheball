using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TempScore : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _scoreTxt;

    void Update()
    {
        _scoreTxt.text = "Score : " + RuleManager._instance._score.ToString();
    }
}
