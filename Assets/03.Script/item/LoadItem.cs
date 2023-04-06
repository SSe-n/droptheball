using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadItem : MonoBehaviour
{
    private int item_count = 5;
    private int coin_data = 10000;
    private int selected = 0;

    [HideInInspector]
    public string skin_data = "";

    public Image prefab_skin;
    public GameObject content;
    public TextMeshProUGUI txt_coin;

    void Awake()
    {
        LoadData();

        //임시 데이터
       // skin_data = "01001";

        //코인 데이터 표시
        txt_coin.text = coin_data.ToString();
        Debug.Log(coin_data);

        #region 스킨 이미지 로드
        for (int i = 0; i < item_count; i++)
        {
            //이미지 인스턴스화
            Image image = Instantiate(prefab_skin);
            image.sprite = Resources.Load<Sprite>("Skins/skin" + (i + 1)) as Sprite;
            GameObject padlock = image.transform.GetChild(0).gameObject;

            #region 자물쇠 아이콘
            if (skin_data[i] == '0')
            {
                padlock.SetActive(true);
            }
            else if (skin_data[i] == '1')
            {
                padlock.SetActive(false);
            }
            else
            {
                Debug.Log("Skin data error. Skin data : " + skin_data);
            }
            #endregion

            image.transform.SetParent(content.transform);
        }
        #endregion
    }

    public void LoadData()
    {
        #region 스킨 데이터
        //스킨 데이터 불러오기
        if (PlayerPrefs.HasKey("Skin"))
        {
            skin_data = PlayerPrefs.GetString("Skin");
        }
        else
        {
            //데이터 초기화
            for (int i = 0; i < item_count; i++)
            {
                skin_data += "0";
            }
        }

        // 아이템 갯수가 기존에 저장되어 있는 아이템 데이터 수보다 많으면 이후 값 초기화
        if (skin_data.Length < item_count)
        {
            for (int i = skin_data.Length; i < item_count; i++)
            {
                skin_data += "0";
            }
        }
        #endregion

        #region 코인 데이터
        //코인 데이터 불러오기
        if (PlayerPrefs.HasKey("Coin"))
        {
            coin_data = PlayerPrefs.GetInt("Coin");
        }
        #endregion
    }

    public void BuySkin()
    {
        //선택된 아이템 번호 불러오기
        selected = content.GetComponent<SwipeItem>().selected;
        if (coin_data >= 100) //임시 가격
        {
            //스킨 데이터 변경
            var sd = skin_data.ToCharArray();
            sd[selected] = '1';
            skin_data = string.Concat(sd);

            //코인 데이터 변경
            coin_data -= 100;

            //데이터 저장
            PlayerPrefs.SetString("Skin", skin_data);
            PlayerPrefs.SetInt("Coin", coin_data);

            //버튼 새로고침
            content.GetComponent<SwipeItem>().ShowBuyBtn(selected);
            //선택된 아이템 자물쇠 비활성화
            var image = content.transform.GetChild(selected);
            image.transform.GetChild(0).gameObject.SetActive(false);
            //코인 새로고침
            txt_coin.text = coin_data.ToString();
        }

    }
}
