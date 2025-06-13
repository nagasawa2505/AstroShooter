using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Dynamic;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rbody;
    Animator anime;

    float axisH, axisV; // 横軸、縦軸
    public float speed = 3.0f;
    public float angleZ = -90.0f; // 角度
    int direction = 0; // アニメの方向番号

    public static int hp = 5; // プレイヤーのタイトル
    bool inDamage; // ダメージ中フラグ
    bool isMobileInput; // スマホ操作中かどうか

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();

        // 下向きにする
        anime.SetInteger("direction", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.gameState != "playing" || inDamage)
        {
            return;
        }

        // モバイルからの入力がない場合のみ
        if (!isMobileInput)
        {
            // 水平方向、垂直方向のキー入力を検知
            axisH = Input.GetAxisRaw("Horizontal");
            axisV = Input.GetAxisRaw("Vertical");
        }

        VectorAnime(axisH, axisV); // 方向アニメを決めるメソッド
    }

    void FixedUpdate()
    {
        if (GameController.gameState != "playing")
        {
            return;
        }

        if (inDamage)
        {
            // 点滅処理（正負の波をつくる）
            // Time.timeはゲーム実行からの経過時間(小数つき）
            float value = Mathf.Sin(Time.time * 50);
            if (value > 0)
            {
                GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                GetComponent<SpriteRenderer>().enabled = false;
            }

            return;
        }

        // normalizedで斜め方向の移動速度を1として調整
        rbody.velocity = new Vector2(axisH, axisV).normalized * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GetDamage(collision.gameObject);
        }
    }

    void GetDamage(GameObject enemy)
    {
        hp--;
        if (hp > 0)
        {
            // 動きが止まる
            rbody.velocity = Vector2.zero;

            // ノックバック（方角計算と移動）
            Vector3 v = (transform.position - enemy.transform.position).normalized * 4.0f;
            rbody.AddForce(v, ForceMode2D.Impulse);

            // ダメージフラグを立てる（硬直する）
            inDamage = true;

            // 時間差でダメージフラグを下げる
            Invoke("DamageEnd", 0.25f);

        }
        else
        {
            GameOver();
            UnityEngine.Debug.Log("ゲームオーバー");
        }
    }

    // ダメージフラグを下げる
    void DamageEnd()
    {
        inDamage = false;

        // プレイヤーの姿（SpriteRendererコンポーネント）を明確に表示状態にしておく
        GetComponent<SpriteRenderer>().enabled = true;
    }

    //void VectorAnime(float axisH, float axisV)
    //{
    //    angleZ = GetAngle();

    //    int dir;

    //    // 下右上左
    //    if (angleZ > -135 && angleZ < -45) dir = 0;
    //    else if (angleZ >= -45 && angleZ <= 45) dir = 3;
    //    else if (angleZ > 45 && angleZ < 135) dir = 1;
    //    else dir = 2;

    //    if (dir != direction)
    //    {
    //        direction = dir;
    //        anime.SetInteger("direction", direction);
    //    }
    //}

    void VectorAnime(float h, float v)
    {
        angleZ = GetAngle();

        //なるべきアニメ番号を一時記録用
        int dir = direction;

        //if (angleZ > -135 && angleZ < -45) dir = 0; //下
        //else if (angleZ >= -45 && angleZ <= 45) dir = 3; //右
        //else if (angleZ > 45 && angleZ < 135) dir = 1; //上
        //else dir = 2; //左

        //左右キーが押されたら
        if (Mathf.Abs(h) >= Mathf.Abs(v))
        {
            if (h > 0) dir = 3;       // 右
            else if (h < 0) dir = 2;  // 左
        }
        else //左右キーが押されなかったら
        {
            if (v > 0) dir = 1;       // 上
            else if (v < 0) dir = 0;  // 下
        }

        //前フレームのdirectionといまあるべきアニメ番号がことなっていなければそのまま
        if (dir != direction)
        {
            direction = dir;
            anime.SetInteger("direction", direction);
        }
    }

    public float GetAngle()
    {
        Vector2 fromPos = transform.position; // 現在地
        Vector2 toPos = new Vector2(fromPos.x + axisH,fromPos.y + axisV); // 目的地

        float angle;

        if (axisH != 0 || axisV != 0)
        {
            float dirX = toPos.x - fromPos.x;
            float dirY = toPos.y - fromPos.y;

            // アークタンジェントに
            // 第一：高さ、第二：底辺
            // を与えると角度が出る（ラジアン値）
            float rad = Mathf.Atan2(dirY, dirX);

            // ラジアン値をオイラー値に変換
            angle = rad * Mathf.Rad2Deg;
        }
        else
        {
            angle = angleZ;
        }

        return angle;
    }

    void GameOver()
    {
        GameController.gameState = "gameover";

        // ゲームオーバー演出
        GetComponent<CircleCollider2D>().enabled = false;
        rbody.velocity = Vector2.zero;
        rbody.gravityScale = 1;
        rbody.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
        anime.SetTrigger("death");
    }

    // プレイヤー消滅
    public void PlayerDestroy()
    {
        Destroy(gameObject);
    }
}
