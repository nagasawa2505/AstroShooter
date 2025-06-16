using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isGoldDoor;
    public Sprite goldDoorImage;

    public bool isEkey;
    bool inDoorArea;
    bool isDelete;

    // �g�[�N�L�����o�X�̃I�u�W�F�N�g��F���ł���悤�ɂ���
    GameObject messageCanvas;
    GameObject messagePanel;
    TextMeshProUGUI messageText;

    bool talking; // ��b���������ǂ���

    // �Z�[�u�f�[�^�p�̎��ʎq
    public int arrangeId;

    // Start is called before the first frame update
    void Start()
    {
        if (isGoldDoor)
        {
            GetComponent<SpriteRenderer>().sprite = goldDoorImage;
        }

        messageCanvas = GameObject.FindGameObjectWithTag("Talk");
        messagePanel = messageCanvas.transform.Find("TalkPanel").gameObject;
        messageText = messagePanel.transform.Find("TalkText").gameObject.GetComponent<TextMeshProUGUI>();
        messagePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!talking && isEkey && inDoorArea)
            {
                string msg;

                if (isGoldDoor && GameController.hasGoldKey > 0)
                {
                    isDelete = true;
                    GameController.hasGoldKey--;
                    msg = "���̌����g�����I";
                }
                else if (!isGoldDoor && GameController.hasSilverKey > 0)
                {
                    isDelete = true;
                    GameController.hasSilverKey--;
                    msg = "��̌����g�����I";
                }
                else
                {
                    msg = "�����������Ă���";
                }
                Time.timeScale = 0;
                talking = true;
                GameController.gameState = "talk";
                messageText.text = msg;
                messagePanel.SetActive(true);
            }
        }

        // �X�y�[�X�L�[�Ń��b�Z�[�W�̃E�C���h�E�����
        else if (talking && GameController.gameState == "talk")
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                messagePanel.SetActive(false);
                messageText.text = "";
                talking = false;
                GameController.gameState = "playing";

                if (isDelete)
                {
                    Destroy(gameObject);
                }

                // �Q�[���ĊJ
                Time.timeScale = 1f;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isEkey && collision.gameObject.CompareTag("Player"))
        {
            if (!isGoldDoor && GameController.hasSilverKey > 0)
            {
                GameController.hasSilverKey--;
                Destroy(gameObject);
            }

            if (isGoldDoor && GameController.hasGoldKey > 0)
            {
                GameController.hasGoldKey--;
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inDoorArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inDoorArea = false;
        }
    }
}
