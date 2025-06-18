using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleController : MonoBehaviour
{
    public string sceneName; // ゲームスタート時のシーン名
    public Button continueButton;

    // Start is called before the first frame update
    void Start()
    {
        // セーブデータが無ければ続きからボタンを無効化
        string lastScene = PlayerPrefs.GetString("Scene");
        if (string.IsNullOrEmpty(lastScene))
        {
            continueButton.interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 最初から
    public void GameStart()
    {
        // セーブデータ削除
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(sceneName);
    }

    // 続きから
    public void ContinueStart()
    {
        SaveSystem.LoadGame();
    }

    // セーブデータ削除
    public void SaveReset()
    {
        // セーブデータの消去
        PlayerPrefs.DeleteAll();
        continueButton.interactable = false;
    }
}
