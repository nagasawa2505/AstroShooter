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

    float axisH, axisV; // �����A�c��
    public float speed = 3.0f;
    public float angleZ = -90.0f; // �p�x
    int direction = 0; // �A�j���̕����ԍ�

    public static int hp = 5; // �v���C���[�̃^�C�g��
    bool inDamage; // �_���[�W���t���O
    bool isMobileInput; // �X�}�z���쒆���ǂ���

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();

        // �������ɂ���
        anime.SetInteger("direction", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.gameState != "playing" || inDamage)
        {
            return;
        }

        // ���o�C������̓��͂��Ȃ��ꍇ�̂�
        if (!isMobileInput)
        {
            // ���������A���������̃L�[���͂����m
            axisH = Input.GetAxisRaw("Horizontal");
            axisV = Input.GetAxisRaw("Vertical");
        }

        VectorAnime(axisH, axisV); // �����A�j�������߂郁�\�b�h
    }

    void FixedUpdate()
    {
        if (GameController.gameState != "playing")
        {
            return;
        }

        if (inDamage)
        {
            // �_�ŏ����i�����̔g������j
            // Time.time�̓Q�[�����s����̌o�ߎ���(�������j
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

        // normalized�Ŏ΂ߕ����̈ړ����x��1�Ƃ��Ē���
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
            // �������~�܂�
            rbody.velocity = Vector2.zero;

            // �m�b�N�o�b�N�i���p�v�Z�ƈړ��j
            Vector3 v = (transform.position - enemy.transform.position).normalized * 4.0f;
            rbody.AddForce(v, ForceMode2D.Impulse);

            // �_���[�W�t���O�𗧂Ă�i�d������j
            inDamage = true;

            // ���ԍ��Ń_���[�W�t���O��������
            Invoke("DamageEnd", 0.25f);

        }
        else
        {
            GameOver();
            UnityEngine.Debug.Log("�Q�[���I�[�o�[");
        }
    }

    // �_���[�W�t���O��������
    void DamageEnd()
    {
        inDamage = false;

        // �v���C���[�̎p�iSpriteRenderer�R���|�[�l���g�j�𖾊m�ɕ\����Ԃɂ��Ă���
        GetComponent<SpriteRenderer>().enabled = true;
    }

    //void VectorAnime(float axisH, float axisV)
    //{
    //    angleZ = GetAngle();

    //    int dir;

    //    // ���E�㍶
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

        //�Ȃ�ׂ��A�j���ԍ����ꎞ�L�^�p
        int dir = direction;

        //if (angleZ > -135 && angleZ < -45) dir = 0; //��
        //else if (angleZ >= -45 && angleZ <= 45) dir = 3; //�E
        //else if (angleZ > 45 && angleZ < 135) dir = 1; //��
        //else dir = 2; //��

        //���E�L�[�������ꂽ��
        if (Mathf.Abs(h) >= Mathf.Abs(v))
        {
            if (h > 0) dir = 3;       // �E
            else if (h < 0) dir = 2;  // ��
        }
        else //���E�L�[��������Ȃ�������
        {
            if (v > 0) dir = 1;       // ��
            else if (v < 0) dir = 0;  // ��
        }

        //�O�t���[����direction�Ƃ��܂���ׂ��A�j���ԍ������ƂȂ��Ă��Ȃ���΂��̂܂�
        if (dir != direction)
        {
            direction = dir;
            anime.SetInteger("direction", direction);
        }
    }

    public float GetAngle()
    {
        Vector2 fromPos = transform.position; // ���ݒn
        Vector2 toPos = new Vector2(fromPos.x + axisH,fromPos.y + axisV); // �ړI�n

        float angle;

        if (axisH != 0 || axisV != 0)
        {
            float dirX = toPos.x - fromPos.x;
            float dirY = toPos.y - fromPos.y;

            // �A�[�N�^���W�F���g��
            // ���F�����A���F���
            // ��^����Ɗp�x���o��i���W�A���l�j
            float rad = Mathf.Atan2(dirY, dirX);

            // ���W�A���l���I�C���[�l�ɕϊ�
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

        // �Q�[���I�[�o�[���o
        GetComponent<CircleCollider2D>().enabled = false;
        rbody.velocity = Vector2.zero;
        rbody.gravityScale = 1;
        rbody.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
        anime.SetTrigger("death");
    }

    // �v���C���[����
    public void PlayerDestroy()
    {
        Destroy(gameObject);
    }
}
