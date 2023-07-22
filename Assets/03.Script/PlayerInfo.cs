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
    // �����͸� JSON���� �����ϴ� �Լ�
    public void SaveToJson()
    {
        // JSON���� ����ȭ
        string jsonData = JsonUtility.ToJson(this);

        // JSON �����͸� ���Ϸ� ����
        string savePath = Application.persistentDataPath + "/player_info.json";
        File.WriteAllText(savePath, jsonData);
    }

    // JSON ���Ϸκ��� �����͸� �ε��ϴ� �Լ�
    public void LoadFromJson()
    {
        string loadPath = Application.persistentDataPath + "/player_info.json";
        // ������ �����ϴ��� Ȯ��
        if (File.Exists(loadPath))
        {
            // JSON ������ �б�
            string jsonData = File.ReadAllText(loadPath);

            // JSON �����͸� PlayerInfo ��ü�� ������ȭ
            JsonUtility.FromJsonOverwrite(jsonData, this);
        }
    }
}
