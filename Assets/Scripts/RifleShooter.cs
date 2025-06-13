using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleShooter : MonoBehaviour
{
    PlayerController playerCnt; // プレイヤー操作スクリプト

    public GameObject riflePrefab; // 生成するライフル
    GameObject rifleObj; // 生成したライフル情報
    Transform gate; // ライフルの発射口

    public GameObject bulletPrefab; // 生成する弾丸
    public float shootSpeed = 12.0f; // 弾の速さ
    public float shootDelay = 0.25f; // 発射間隔
    bool inAttack; // 攻撃中フラグ

    // Start is called before the first frame update
    void Start()
    {
        playerCnt = GetComponent<PlayerController>();

        Vector3 playerPos = transform.position; // Playerの位置

        // ライフルを生成しつつ情報を格納
        // プレイヤーの位置に生成、回転なし
        rifleObj = Instantiate(riflePrefab, playerPos, Quaternion.identity);

        // 発射口を取得
        gate = rifleObj.transform.Find("Gate");

        // プレイヤーの子オブジェクトにする
        rifleObj.transform.SetParent(transform);
    }

    // Update is called once per frame
    void Update()
    {
        // ライフルの回転とZ軸（奥行）の優先順位
        float rifleZ = -1; // 基本はライフルを手前に表示

        // 上を向いてるときはライフルを奥に表示
        if (playerCnt.angleZ > 45 && playerCnt.angleZ < 135)
        {
            rifleZ = 1;
        }

        // プレイヤーに合わせてライフルを回転
        // ライフルの絵の向きに合わせて90を足す
        rifleObj.transform.rotation = Quaternion.Euler(0, 0, (playerCnt.angleZ + 90));

        // ライフルのZ軸（奥行）を調整
        rifleObj.transform.position = new Vector3(transform.position.x, transform.position.y, rifleZ);

        // キーが押されたら弾丸発射
        if (!inAttack && Input.GetKeyDown(KeyCode.R))
        {
            Attack();
        }
    }

    // 弾丸発射
    void Attack()
    {
        // 残弾チェック
        if (GameController.hasBullet <= 0)
        {
            return;
        }
        GameController.hasBullet--;

        inAttack = true;

        // プレイヤーの角度を生成する弾丸の角度にする
        Quaternion bulletRotate = Quaternion.Euler(0, 0, playerCnt.angleZ);

        // 弾丸を生成して格納
        GameObject bulletObj = Instantiate(bulletPrefab, gate.position, bulletRotate);

        // angleZと三角関数を使ってX成分とY成分を計算する（ラジアン値）
        float x = Mathf.Cos(playerCnt.angleZ * Mathf.Deg2Rad);
        float y = Mathf.Sin(playerCnt.angleZ * Mathf.Deg2Rad);

        // Z=0は省略可
        Vector3 v = new Vector3(x, y).normalized * shootSpeed;

        // 生成した弾丸のRigidbody2Dを取得
        Rigidbody2D bulletRigid = bulletObj.GetComponent<Rigidbody2D>();

        // 弾丸を飛ばす
        bulletRigid.AddForce(v, ForceMode2D.Impulse);

        // 攻撃中フラグを時間をおいて下げる
        Invoke("StopAttack", shootDelay);
    }

    // 攻撃中フラグを下げる
    void StopAttack()
    {
        inAttack = false;
    }
}
