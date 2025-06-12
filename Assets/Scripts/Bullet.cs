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
            // �����e�I�u�W�F�N�g�ɂ���
            transform.SetParent(collision.transform);

            // �����蔻��𖳌��ɂ���
            GetComponent<BoxCollider2D>().enabled = false;

            // ���������𖳌��ɂ���
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            // GetComponent<Rigidbody2D>().simulated = false;
        }
        Destroy(gameObject, 0.1f);
    }
}
