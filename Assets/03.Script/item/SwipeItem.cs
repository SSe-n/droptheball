using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeItem : MonoBehaviour
{
    public GameObject scrollbar;
    public GameObject loadItem;
    public GameObject btn_buy;

    float scroll_pos = 0;
    float[] pos;
    float distance = 0;

    [HideInInspector]
    public int selected; //선택된 아이템 번호

    void Start()
    {
        pos = new float[transform.childCount];

        distance = 1f / (pos.Length - 1f);

        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            scroll_pos = scrollbar.GetComponent<Scrollbar>().value;
        }
        else
        {
            //일정 범위에 들어오면 가운데로 위치 고정
            for (int i = 0; i < pos.Length; i++)
            {
                if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
                {
                    scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i], 0.1f);
                }
            }
        }

        //선택된 아이템 크기 조절
        for (int i = 0; i < pos.Length; i++)
        {
            if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
            {
                transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(1f, 1f), 0.1f);

                selected = i;

                ShowBuyBtn(i);

                for (int j = 0; j < pos.Length; j++)
                {
                    if (j != i)
                    {
                        transform.GetChild(j).localScale = Vector2.Lerp(transform.GetChild(j).localScale, new Vector2(0.8f, 0.8f), 0.1f);
                    }
                }
            }
        }
    }

    //Buy 버튼
    public void ShowBuyBtn(int i)
    {
        string skin_data = loadItem.GetComponent<LoadItem>().skin_data;
        if (skin_data[i] == '0')
        {
            btn_buy.SetActive(true);
        }
        else
        {
            btn_buy.SetActive(false);
        }
    }
}
