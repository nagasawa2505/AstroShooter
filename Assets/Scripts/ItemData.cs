using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;

public enum ItemType // 列挙型
{
    light,
    bullet,
    goldkey,
    silverkey,
    life
}

public class ItemData : MonoBehaviour
{
    public ItemType type; // アイテムの種類
    public int stockCount; // 入手数

    public bool isTalk; // トークパネルでアナウンスするか

    // セーブデータの識別番号
    public int arrangeId;

    [TextArea]
    public string message1;
    [TextArea]
    public string message2;

    // トークキャンバスのオブジェクトを認識できるようにする
    GameObject messageCanvas;
    GameObject messagePanel;
    TextMeshProUGUI messageText;

    bool talking; // 会話中フラグ

    Rigidbody2D rbody;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();

        messageCanvas = GameObject.FindGameObjectWithTag("Talk");
        messagePanel = messageCanvas.transform.Find("TalkPanel").gameObject;
        messageText = messagePanel.transform.Find("TalkText").gameObject.GetComponent<TextMeshProUGUI>();
        messagePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // スペースキーでメッセージのウインドウを閉じる
        if (talking && GameController.gameState == "talk")
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                messagePanel.SetActive(false);
                messageText.text = "";
                talking = false;
                GameController.gameState = "playing";

                // ゲーム再開
                Time.timeScale = 1f;

                ItemDestroy();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
            switch (type)
            {
                case ItemType.light:
                    LightController.getLight = true;
                    break;
                case ItemType.bullet:
                    GameController.hasBullet += stockCount;
                    break;
                case ItemType.goldkey:
                    GameController.hasGoldKey += stockCount;
                    break;
                case ItemType.silverkey:
                    GameController.hasSilverKey += stockCount;
                    break;
                case ItemType.life:
                    if (PlayerController.hp < 5)
                    {
                        PlayerController.hp++;
                    }
                    break;
                default:
                    break;
            }

            // メッセージを表示する場合
            if (isTalk)
            {
                // 表示するメッセージを作成
                string message = message1 + stockCount + message2;

                GameController.gameState = "talk";
                talking = true;

                // UIパネル表示
                messagePanel.SetActive(true);

                // UIテキストの内容を更新
                messageText.text = message;

                // ゲーム進行停止（UIは動く）
                Time.timeScale = 0f;
            }
            else
            {
                ItemDestroy();
            }
        }
    }

    // 消滅演出
    void ItemDestroy()
    {
        GetComponent<CircleCollider2D>().enabled = false;
        rbody.gravityScale = 2.5f;
        rbody.AddForce(new Vector2(0, 6), ForceMode2D.Impulse);
        Destroy(gameObject, 0.5f);
    }
}
