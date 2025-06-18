using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    bool inSavePointArea;

    // トークキャンバスのオブジェクトを認識できるようにする
    GameObject messageCanvas;
    GameObject messagePanel;
    TextMeshProUGUI messageText;

    bool talking; // 会話中フラグ

    // Start is called before the first frame update
    void Start()
    {
        messageCanvas = GameObject.FindGameObjectWithTag("Talk");
        messagePanel = messageCanvas.transform.Find("TalkPanel").gameObject;
        messageText = messagePanel.transform.Find("TalkText").gameObject.GetComponent<TextMeshProUGUI>();
        messagePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Eキーが押されたらセーブ
        if (!talking && Input.GetKeyDown(KeyCode.E))
        {
            if (inSavePointArea)
            {
                SaveSystem.SaveGame();

                GameController.gameState = "talk";
                talking = true;

                // UIパネル表示
                messagePanel.SetActive(true);

                // UIテキストの内容を更新
                messageText.text = "セーブしました";

                // ゲーム進行停止（UIは動く）
                Time.timeScale = 0f;
            }
        }
        else if (talking && Input.GetKeyDown(KeyCode.Space))
        {
            messagePanel.SetActive(false);
            messageText.text = "";
            talking = false;
            GameController.gameState = "playing";

            // ゲーム再開
            Time.timeScale = 1f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            return;
        }
        inSavePointArea = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            return;
        }
        inSavePointArea = false;
    }
}
