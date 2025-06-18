using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    public GameObject itemPrefab; // 中身となるアイテム
    public Sprite openImage; // 開封された宝箱の絵
    public bool isClosed = true; // 未開封かどうか

    GameObject player; // プレイヤー情報（位置）
    bool inBoxArea; // 接触ではなく領域に入ったかどうかのフラグ
    public bool isEkey; // Eキーで開ける仕様かどうか
    public int arrangeId; // セーブデータ用の識別子

    // Start is called before the first frame update
    void Start()
    {
        // 自身が消費済みかチェック
        ExistCheck();
    }

    // Update is called once per frame
    void Update()
    {
        // Eキーが押されたときに入手させる
        if (isClosed && isEkey && Input.GetKeyDown(KeyCode.E))
        {
            if (inBoxArea)
            {
                BoxOpen();
            }
        }
    }

    // 宝箱に接触するだけでアイテムを入手させる
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isEkey || !isClosed || !collision.gameObject.CompareTag("Player"))
        {
            return;
        }
        BoxOpen();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            return;
        }
        inBoxArea = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            return;
        }
        inBoxArea = false;
    }

    // 宝箱開封処理
    void BoxOpen()
    {
        // 未開封フラグを下げる
        isClosed = false;

        // 宝箱の絵の差し替え
        GetComponent<SpriteRenderer>().sprite = openImage;

        // プレイヤーの位置にアイテムを生成する
        if (itemPrefab != null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            Instantiate(itemPrefab, player.transform.position, Quaternion.identity);

            // 消費済みリストに追加してなければ追加
            if (!SaveController.Instance.IsConsumed(this.tag, arrangeId))
            {
                SaveController.Instance.ConsumedEvent(this.tag, arrangeId);
            }
        }
    }

    // 自身が消費済みかチェック
    void ExistCheck()
    {
        if (SaveController.Instance.IsConsumed(this.tag, arrangeId))
        {
            isClosed = false;
            GetComponent<SpriteRenderer>().sprite = openImage;
        }
    }
}
