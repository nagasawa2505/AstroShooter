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
    public float angleZ; // 角度
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
        // normalizedで斜め方向の移動速度を1として調整
        rbody.velocity = new Vector2(axisH, axisV).normalized * speed;
    }

    void VectorAnime(float axisH, float axisV)
    {
        angleZ = GetAngle();

        int dir;

        // 下右上左
        if (angleZ > -135 && angleZ < -45) dir = 0;
        else if (angleZ >= -45 && angleZ <= 45) dir = 3;
        else if (angleZ > 45 && angleZ < 135) dir = 1;
        else dir = 2;

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
}
