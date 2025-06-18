using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

// ���W�X�g���p�P��Z�[�u�f�[�^
[System.Serializable]
public class SaveData
{
    public string tag;
    public int arrangeId;
    
    // �R���X�g���N�^
    public SaveData(string tag, int arrangeId)
    {
        this.tag = tag;
        this.arrangeId = arrangeId;
    }
}

// ���W�X�g���p�Z�[�u�f�[�^���X�g(JSON���Ώ�)
[System.Serializable]
class Wrapper
{
    public List<SaveData> items;
}

// ���W�X�g���p�Z�[�u�N���X
public class SaveSystem
{
    public static void SaveGame()
    {
        // �v���C���[���擾
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        // ����ς݃��X�g������
        List<SaveData> dataList = new List<SaveData>();

        // ����ς݃��X�g�̓��e��1��������
        foreach (var obj in SaveController.Instance.consumedEvent)
        {
            dataList.Add(new SaveData(obj.tag, obj.arrangeId));
        }

        // JSON���ΏۃN���X�𐶐����ăf�[�^���Z�b�g
        Wrapper wrapper = new Wrapper();
        wrapper.items = dataList;

        // �N���X��JSON������ɕϊ�
        string json = JsonUtility.ToJson(wrapper);
//Debug.Log(JsonUtility.ToJson(wrapper.items));

        // ���W�X�g���ɋL�^
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

        // �O�����Ŗ����I�Ƀf�B�X�N�ɔ��f
        PlayerPrefs.Save();
    }

    public static void LoadGame()
    {
        // ����ς݃��X�g�̓ǂݍ��ݐ��������
        SaveController.Instance.consumedEvent.Clear();

        string json = PlayerPrefs.GetString("ConsumedJson");
        if (!string.IsNullOrEmpty(json))
        {
            // JSON����C���X�^���X�𕜌�
            Wrapper wrapper = JsonUtility.FromJson<Wrapper>(json);

            // ����ς݃��X�g��1��������
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
