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

    // �Z�[�u�f�[�^�Ǘ��p���ʎq
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

            // �������烉�W�A���l���v�Z
            float angle = Mathf.Atan2(dirY, dirX);

            // ���ӂ�1�Ƃ����Ƃ���X��Y���v�Z
            axisH = Mathf.Cos(angle);
            axisV = Mathf.Sin(angle);
        }

        // ���G����
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
                // ���S���o
                GetComponent<CapsuleCollider2D>().enabled = false;
                rbody.velocity = Vector2.zero;
                GetComponent<Animator>().SetBool("death", true);
                Destroy(gameObject, 1.0f);
            }
        }
    }

    // �v���C���[�Ƃ̋����𑪂�
    void SearchPlayer()
    {
        // �v���C���[�Ƃ̒����������擾
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance <= searchDistance)
        {
            isActive = true;
        }
        else
        {
            isActive = false;

            // �ړ����~�߂�
            rbody.velocity = Vector2.zero;
        }
    }
}
