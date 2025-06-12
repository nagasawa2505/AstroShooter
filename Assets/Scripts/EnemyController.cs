using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int hp = 10;
    public float speed = 1.0f;
    public float searchDistance = 100.0f;

    float axisH, axisV;
    Rigidbody2D rbody;
    bool isActive;

    // セーブデータ管理用識別子
    public int arrangeId;

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            return;
        }

        if (isActive)
        {
            float dirX = player.transform.position.x - transform.position.x;
            float dirY = player.transform.position.y - transform.position.y;

            // 差分からラジアン値を計算
            float angle = Mathf.Atan2(dirY, dirX);

            // 長辺を1としたときのXとYを計算
            axisH = Mathf.Cos(angle);
            axisV = Mathf.Sin(angle);
        }

        // 索敵する
        SearchPlayer();
    }

    private void FixedUpdate()
    {
        if (isActive && hp > 0)
        {
            rbody.velocity = new Vector2(axisH, axisV) * speed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            hp--;
            if (hp < 0)
            {
                // 死亡演出
                GetComponent<CapsuleCollider2D>().enabled = false;
                rbody.velocity = Vector2.zero;
                GetComponent<Animator>().SetBool("death", true);
                Destroy(gameObject, 1.0f);
            }
        }
    }

    // プレイヤーとの距離を測る
    void SearchPlayer()
    {
        // プレイヤーとの直線距離を取得
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance <= searchDistance)
        {
            isActive = true;
        }
        else
        {
            isActive = false;

            // 移動を止める
            rbody.velocity = Vector2.zero;
        }
    }
}
