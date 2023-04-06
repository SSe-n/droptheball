using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartManager : MonoBehaviour
{
    public GameObject guide;
    void Start()
    {
        if(PlayerPrefs.HasKey("IsFirst"))
        {
            guide.SetActive(false);
        }
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            this.gameObject.SetActive(false);
        }
    }
}
