using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{

    public Slider SoundSlider;

    int bar;
    int on;
    // Start is called before the first frame update
    void Awake()
    {
        bar=100;
        on=100;
    }

    // Update is called once per frame
    void Update()
    {
        SoundSlider.value = (float)bar/on;
    }

    public void OnClickButtonA(){
        bar=bar-100;
    }
}
