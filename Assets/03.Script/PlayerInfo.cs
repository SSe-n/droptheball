using UnityEngine;
using System.IO;

public class PlayerInfo : MonoBehaviour
{
    static public PlayerInfo _instance;
    public int _coin;
    public int _recordScore;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            LoadFromJson();
        }
    }
    // 데이터를 JSON으로 저장하는 함수
    public void SaveToJson()
    {
        // JSON으로 직렬화
        string jsonData = JsonUtility.ToJson(this);

        // JSON 데이터를 파일로 저장
        string savePath = Application.persistentDataPath + "/player_info.json";
        File.WriteAllText(savePath, jsonData);
    }

    // JSON 파일로부터 데이터를 로드하는 함수
    public void LoadFromJson()
    {
        string loadPath = Application.persistentDataPath + "/player_info.json";
        // 파일이 존재하는지 확인
        if (File.Exists(loadPath))
        {
            // JSON 데이터 읽기
            string jsonData = File.ReadAllText(loadPath);

            // JSON 데이터를 PlayerInfo 객체로 역직렬화
            JsonUtility.FromJsonOverwrite(jsonData, this);
        }
    }
}
