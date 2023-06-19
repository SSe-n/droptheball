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
    public GameObject highscore;
    [SerializeField] CoinManager cm;
    private void OnCollisionEnter(Collision collision)
    {
        gameover.SetActive(true);
        //일시정지
        Time.timeScale = 0;
        GameObject.Find("SoundManager").GetComponent<SoundManager>().SfxPlay(SoundManager.Sfx.GameOver);

        float s = RuleManager._instance._score;
        score.text = s.ToString();
        //내림
        float coinAmount = Mathf.Floor(s / 100);
        coin.text = coinAmount.ToString();

        //처음하는 게임인지
        PlayerPrefs.SetInt("IsFirst", 1); ;

        #region 최고 점수 여부
        if (PlayerPrefs.HasKey("HighScore"))
        {
            int hs = PlayerPrefs.GetInt("HighScore");
            Debug.Log("high score : " + hs);
            if (s > hs)
            {
                PlayerPrefs.SetInt("HighScore", (int)s);
                gameover.GetComponent<Animator>().SetTrigger("highScore");
            }
            else
            {
                highscore.SetActive(false);
            }
        }
        else
        {
            PlayerPrefs.SetInt("HighScore", (int)s);
            gameover.GetComponent<Animator>().SetTrigger("highScore");
        }
        #endregion

        #region 코인 획득
        cm.GetCoin((int)coinAmount);

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
        cm.GetCoin(10);
        //Time.timeScale = 1;
        //GameObject.Find("SoundManager").GetComponent<SoundManager>().SfxPlay(SoundManager.Sfx.Button);
        //SceneManager.LoadScene(1);
    }
}
