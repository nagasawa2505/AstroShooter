using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isGoldDoor;
    public Sprite goldDoorImage;

    public bool isEkey;
    bool inDoorArea;
    bool isDelete;

    // トークキャンバスのオブジェクトを認識できるようにする
    GameObject messageCanvas;
    GameObject messagePanel;
    TextMeshProUGUI messageText;

    bool talking; // 会話発生中かどうか

    // セーブデータ用の識別子
    public int arrangeId;

    // Start is called before the first frame update
    void Start()
    {
        if (isGoldDoor)
        {
            GetComponent<SpriteRenderer>().sprite = goldDoorImage;
        }

        messageCanvas = GameObject.FindGameObjectWithTag("Talk");
        messagePanel = messageCanvas.transform.Find("TalkPanel").gameObject;
        messageText = messagePanel.transform.Find("TalkText").gameObject.GetComponent<TextMeshProUGUI>();
        messagePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!talking && isEkey && inDoorArea)
            {
                string msg;

                if (isGoldDoor && GameController.hasGoldKey > 0)
                {
                    isDelete = true;
                    GameController.hasGoldKey--;
                    msg = "金の鍵を使った！";
                }
                else if (!isGoldDoor && GameController.hasSilverKey > 0)
                {
                    isDelete = true;
                    GameController.hasSilverKey--;
                    msg = "銀の鍵を使った！";
                }
                else
                {
                    msg = "鍵がかかっている";
                }
                Time.timeScale = 0;
                talking = true;
                GameController.gameState = "talk";
                messageText.text = msg;
                messagePanel.SetActive(true);
            }
        }

        // スペースキーでメッセージのウインドウを閉じる
        else if (talking && GameController.gameState == "talk")
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                messagePanel.SetActive(false);
                messageText.text = "";
                talking = false;
                GameController.gameState = "playing";

                if (isDelete)
                {
                    Destroy(gameObject);
                }

                // ゲーム再開
                Time.timeScale = 1f;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isEkey && collision.gameObject.CompareTag("Player"))
        {
            if (!isGoldDoor && GameController.hasSilverKey > 0)
            {
                GameController.hasSilverKey--;
                Destroy(gameObject);
            }

            if (isGoldDoor && GameController.hasGoldKey > 0)
            {
                GameController.hasGoldKey--;
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inDoorArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inDoorArea = false;
        }
    }
}
