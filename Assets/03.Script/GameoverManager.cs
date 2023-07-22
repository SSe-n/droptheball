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
    private void OnCollisionEnter(Collision collision)
    {
        GameOver();
    }
    public void GameOver()
    {
        gameover.SetActive(true);
        //일시정지
        Time.timeScale = 0;
        GameObject.Find("SoundManager").GetComponent<SoundManager>().SfxPlay(SoundManager.Sfx.GameOver);

        float s = RuleManager._instance._score;
        score.text = s.ToString();
        //내림
        coin.text = Mathf.Floor(s / 100).ToString();

        //처음하는 게임인지
        //PlayerPrefs.SetInt("IsFirst", 1); ;

        #region 최고 점수 여부
        if (s > PlayerInfo._instance._recordScore)
        {
            PlayerInfo._instance._recordScore = (int)s;
            gameover.GetComponent<Animator>().SetTrigger("highScore");
        }
        else
            highscore.SetActive(false);
        #endregion

        #region 코인 획득
        PlayerInfo._instance._coin += (int)Mathf.Floor(s / 100);
        //if (PlayerPrefs.HasKey("Coin"))
        //{

        //    //PlayerPrefs.SetInt("Coin", coin);
        //}
        //else
        //{
        //    Mathf.Floor(s / (int)Mathf.Floor(s / 100));
        //}
        PlayerInfo._instance.SaveToJson();
        #endregion
    }
    public void Restart()
    {
        Time.timeScale = 1;
        GameObject.Find("SoundManager").GetComponent<SoundManager>().SfxPlay(SoundManager.Sfx.Button);
        SceneManager.LoadScene(1);
    }
}
