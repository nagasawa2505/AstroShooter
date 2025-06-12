using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float deleteTime = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, deleteTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Wall"))
        {
            // 相手を親オブジェクトにする
            transform.SetParent(collision.transform);

            // 当たり判定を無効にする
            GetComponent<BoxCollider2D>().enabled = false;

            // 物理処理を無効にする
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            // GetComponent<Rigidbody2D>().simulated = false;
        }
        Destroy(gameObject, 0.1f);
    }
}
