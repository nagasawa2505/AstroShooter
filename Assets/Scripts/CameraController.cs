//using System.Collections;
//using System.Collections.Generic;
//using Unity.VisualScripting;
//using UnityEngine;

//public class CameraController : MonoBehaviour
//{
//    public GameObject otherTarget;
//    GameObject player;

//    // Start is called before the first frame update
//    void Start()
//    {
//        // �ŏ��Ƀv���C���[������
//        player = GameObject.FindGameObjectWithTag("Player");
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (player == null)
//        {
//            return;
//        }

//        // ���ԃ��[�h�̑��肪�Z�b�g����Ă����
//        if (otherTarget != null)
//        {
//            // ���`�⊮�̑�O�����̐i�������50%�ɂ��遨���ԍ��W�̎Z�o
//            Vector2 pos = Vector2.Lerp(player.transform.position, otherTarget.transform.position, 0.5f);

//            // ���g�̍��W�ɔ��f
//            // z����[�܂��0�����ɂȂ�̂Ō��ݒl������
//            transform.position = new Vector3(pos.x, pos.y, transform.position.z);
//        }
//        else
//        {
//            transform.position = new Vector3(player.transform.x, player.transform.y, player.transform.z);
//        }
//    }
//}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject otherTarget;//���Ԓn�_���[�h�̑Ώ�
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        //�ŏ��Ƀv���C���[������
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        if (otherTarget != null) //���ԃ��[�h�̑��肪�Z�b�g����Ă����
        {
            //���Ԓn�_���ʂ�����
            //���`��Ԃ̑�3�����̐i�������50%�����ԍ��W�̎Z�o
            Vector2 pos = Vector2.Lerp(player.transform.position, otherTarget.transform.position, 0.5f);
            //pos�Ɋm�ۂ��Ă����l�������̍��W�ɔ��f
            transform.position = new Vector3(pos.x, pos.y, transform.position.z);
        }
        else //���ԃ��[�h�̑��肪���ɃZ�b�g����Ă��Ȃ����
        {
            //�v���C���[��ǂ��i�w�A�x���W�̓v���C���[�Ɠ����j
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        }
    }
}