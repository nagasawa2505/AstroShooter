using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomController : MonoBehaviour
{
    // �Z�[�u�f�[�^�����[�h�����ۂ̈ړ���
    public static bool isContinue;

    // �Q�[�������ʂ��ĊǗ�����h�A�ԍ�
    public static int doorNumber;

    // �v���C���[�̃A�j���ƕ������v�Z
    int direction;
    float angleZ;

    // Start is called before the first frame update
    void Start()
    {
        // �v���C���[���擾
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (isContinue)
        {
            // �v���C���[���Z�[�u���̈ʒu�Ɉړ�
            float posX = PlayerPrefs.GetFloat("posX");
            float posY = PlayerPrefs.GetFloat("posY");
            player.transform.position = new Vector2(posX, posY);

            return;
        }

        GameObject[] exits = GameObject.FindGameObjectsWithTag("Exit");
        for (int i = 0; i < exits.Length; i++)
        {
            Exit exit = exits[i].GetComponent<Exit>();
            if (exit == null) Debug.Log("NULL" + exits.Length);
            if (doorNumber == exit.doorNumber)
            {
                float x = exits[i].transform.position.x;
                float y = exits[i].transform.position.y;

                // �ړ�����ɍēx�ړ����Ȃ��悤�ɏ������炷
                switch (exit.direction)
                {
                    case ExitDirection.up:
                        y += 1;
                        direction = 1;
                        angleZ = 90;
                        break;
                    case ExitDirection.down:
                        y -= 1;
                        direction = 0;
                        angleZ = -90;
                        break;
                    case ExitDirection.left:
                        x -= 1;
                        direction = 2;
                        angleZ = 180;
                        break;
                    case ExitDirection.right:
                        x += 1;
                        direction = 3;
                        angleZ = 0;
                        break;
                    default:
                        break;
                }

                // �ʒu
                player.transform.position = new Vector3(x, y);
                // �p�x
                player.GetComponent<PlayerController>().angleZ = angleZ;
                // �A�j��
                player.GetComponent<Animator>().SetInteger("direction", direction);
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // �ÓI���\�b�h�i�ǂ��̃V�[���ɉ��Ԃ̃h�A�H)
    public static void ChangeScene(string scenename, int doornum)
    {
        // ���̃V�[���Ƀh�A�ԍ��������p�����
        doorNumber = doornum;
        isContinue = false;
        SceneManager.LoadScene(scenename);
    }
}
