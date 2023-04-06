using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnClick : MonoBehaviour
{
    public Button btn;

    // Start is called before the first frame update

    void OnClickButton()
    {
        if (this.name == "BackBtn")
        {
            GameObject.Find("SoundManager").GetComponent<SoundManager>().SfxPlay(SoundManager.Sfx.Exit);
        }
        GameObject.Find("SoundManager").GetComponent<SoundManager>().SfxPlay(SoundManager.Sfx.Button);
    }
    void Start()
    {
        btn = this.GetComponent<Button>();
        btn.onClick.AddListener(OnClickButton);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
