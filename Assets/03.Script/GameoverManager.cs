using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameoverManager : MonoBehaviour
{
    public GameObject gameover;
    public TextMeshProUGUI score;
    public TextMeshProUGUI coin;
    private void OnCollisionEnter(Collision collision)
    {
        gameover.SetActive(true);
        //일시정지
        Time.timeScale = 0;

        float s = RuleManager._instance._score;
        score.text = s.ToString();
        //내림
        coin.text = Mathf.Floor(s / 100).ToString();

        //처음하는 게임인지
        PlayerPrefs.SetInt("IsFirst", 1); ;

        #region 최고 점수 여부
        if (PlayerPrefs.HasKey("HighScore"))
        {
            int hs = PlayerPrefs.GetInt("HighScore");
            if (s > hs)
            {
                PlayerPrefs.SetInt("HighScore", (int)s);
            }
        }
        else
        {
            PlayerPrefs.SetInt("HighScore", (int)s);
        }
        #endregion

        #region 코인 획득
        if (PlayerPrefs.HasKey("Coin"))
        {
            int coin = PlayerPrefs.GetInt("Coin");
            coin += (int)Mathf.Floor(s / 100);
            PlayerPrefs.SetInt("Coin", coin);
        }
        else
        {
            Mathf.Floor(s / (int)Mathf.Floor(s / 100));
        }
        #endregion
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
