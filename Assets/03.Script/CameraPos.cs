using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPos : MonoBehaviour
{
    public Transform[] _camPos;
    public Transform _centerPos;
    int _nowIndex;

    private void Awake()
    {
        GameObject[] go = GameObject.FindGameObjectsWithTag("CameraPos");
        _camPos = new Transform[go.Length];
        for (int i = 0; i < go.Length; i++)
        {
            _camPos[i] = go[i].transform;
        }
        gameObject.transform.position = _camPos[0].position;
        gameObject.transform.rotation = _camPos[0].rotation;
    }

    private void Update()
    {

    }
    private void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 150, 40), "Left"))
        {
            if (_nowIndex == 0)
            {
                _nowIndex = _camPos.Length - 1;
            }
            else
            {
                _nowIndex--;
            }
            iTween.MoveTo(gameObject, iTween.Hash("position", _camPos[_nowIndex]));
            iTween.RotateTo(gameObject, iTween.Hash("rotation", _camPos[_nowIndex])); 
        }
        if (GUI.Button(new Rect(0, 50, 150, 40), "Right"))
        {
            if (_nowIndex == _camPos.Length - 1)
            {
                _nowIndex = 0;
            }
            else
            {
                _nowIndex++;
            }
            iTween.MoveTo(gameObject, iTween.Hash("position", _camPos[_nowIndex]));
            iTween.RotateTo(gameObject, iTween.Hash("rotation", _camPos[_nowIndex]));
        }
    }
}
