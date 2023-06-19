using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;


public class CoinManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] GameObject coinPrefab;
    [SerializeField] Transform targetPos;
    [SerializeField] Transform startPos;

    [Space]
    [Header("Animation Setting")]
    [SerializeField] [Range(0.5f, 0.9f)] float minAnimDuration;
    [SerializeField] [Range(0.9f, 2f)] float maxAnimDuration;
    [SerializeField] Ease easeType;

    Vector2 targetPosition;
    Vector2 startPosition;

    int holdCoin = 0;

    void Awake()
    {
        targetPosition = targetPos.position;
        startPosition = startPos.position;
    }

    private void Start()
    {
        GetCoin(10);
        coinText.text = holdCoin.ToString();
    }

    public void GetCoin(int amount)
    {
        startPosition = startPos.position;
        targetPosition = targetPos.position;
        for (int i = 0; i < amount; i++)
        {
            GameObject coin;
            coin = Instantiate(coinPrefab);
            coin.transform.SetParent(this.transform);
            coin.transform.position = startPosition;

            float duration = Random.Range(minAnimDuration, maxAnimDuration);
            coin.transform.DOMove(targetPosition, duration)
                .SetEase(easeType)
                .OnComplete(() =>
                {
                    Destroy(coin);
                    holdCoin++;
                    coinText.text = holdCoin.ToString();
                });
        }



    }
}
