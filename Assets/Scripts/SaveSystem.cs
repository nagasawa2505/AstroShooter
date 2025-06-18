using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

// レジストリ用単一セーブデータ
[System.Serializable]
public class SaveData
{
    public string tag;
    public int arrangeId;
    
    // コンストラクタ
    public SaveData(string tag, int arrangeId)
    {
        this.tag = tag;
        this.arrangeId = arrangeId;
    }
}

// レジストリ用セーブデータリスト(JSON化対象)
[System.Serializable]
class Wrapper
{
    public List<SaveData> items;
}

// レジストリ用セーブクラス
public class SaveSystem
{
    public static void SaveGame()
    {
        // プレイヤー情報取得
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        // 消費済みリスト複製先
        List<SaveData> dataList = new List<SaveData>();

        // 消費済みリストの内容を1件ずつ複製
        foreach (var obj in SaveController.Instance.consumedEvent)
        {
            dataList.Add(new SaveData(obj.tag, obj.arrangeId));
        }

        // JSON化対象クラスを生成してデータをセット
        Wrapper wrapper = new Wrapper();
        wrapper.items = dataList;

        // クラスをJSON文字列に変換
        string json = JsonUtility.ToJson(wrapper);
//Debug.Log(JsonUtility.ToJson(wrapper.items));

        // レジストリに記録
        PlayerPrefs.SetString("ConsumedJson", json);
        PlayerPrefs.SetInt("GoldKey", GameController.hasGoldKey);
        PlayerPrefs.SetInt("SilverKey", GameController.hasSilverKey);
        PlayerPrefs.SetInt("Bullet", GameController.hasBullet);
        PlayerPrefs.SetInt("Life", PlayerController.hp);
        int lightCount = LightController.getLight ? 1 : 0;
        PlayerPrefs.SetInt("Light", lightCount);
        PlayerPrefs.SetString("Scene", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetFloat("posX", player.transform.position.x);
        PlayerPrefs.SetFloat("posY", player.transform.position.y);

        // 念押しで明示的にディスクに反映
        PlayerPrefs.Save();
    }

    public static void LoadGame()
    {
        // 消費済みリストの読み込み先を初期化
        SaveController.Instance.consumedEvent.Clear();

        string json = PlayerPrefs.GetString("ConsumedJson");
        if (!string.IsNullOrEmpty(json))
        {
            // JSONからインスタンスを復元
            Wrapper wrapper = JsonUtility.FromJson<Wrapper>(json);

            // 消費済みリストを1件ずつ復元
            foreach (var item in wrapper.items)
            {
                SaveController.Instance.ConsumedEvent(item.tag, item.arrangeId);
            }
        }

        GameController.hasGoldKey = PlayerPrefs.GetInt("GoldKey");
        GameController.hasSilverKey = PlayerPrefs.GetInt("SilvereKey");
        GameController.hasBullet = PlayerPrefs.GetInt("Bullet");
        PlayerController.hp = PlayerPrefs.GetInt("Life");
        int lightCount = PlayerPrefs.GetInt("Light") == 1 ? 1 : 0;
        string sceneName = PlayerPrefs.GetString("Scene");
        if (string.IsNullOrEmpty(sceneName)) {
            sceneName = "Title";
        }
        RoomController.isContinue = true;
        SceneManager.LoadScene(sceneName);
    }
}
