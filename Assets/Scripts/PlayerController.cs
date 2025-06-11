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
    public float angleZ; // �p�x
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
        // normalized�Ŏ΂ߕ����̈ړ����x��1�Ƃ��Ē���
        rbody.velocity = new Vector2(axisH, axisV).normalized * speed;
    }

    void VectorAnime(float axisH, float axisV)
    {
        angleZ = GetAngle();

        int dir;

        // ���E�㍶
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
}
