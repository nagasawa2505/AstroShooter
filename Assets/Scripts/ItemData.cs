using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;

public enum ItemType // �񋓌^
{
    light,
    bullet,
    goldkey,
    silverkey,
    life
}

public class ItemData : MonoBehaviour
{
    public ItemType type; // �A�C�e���̎��
    public int stockCount; // ���萔

    public bool isTalk; // �g�[�N�p�l���ŃA�i�E���X���邩

    // �Z�[�u�f�[�^�̎��ʔԍ�
    public int arrangeId;

    [TextArea]
    public string message1;
    [TextArea]
    public string message2;

    // �g�[�N�L�����o�X�̃I�u�W�F�N�g��F���ł���悤�ɂ���
    GameObject messageCanvas;
    GameObject messagePanel;
    TextMeshProUGUI messageText;

    bool talking; // ��b���t���O

    Rigidbody2D rbody;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();

        messageCanvas = GameObject.FindGameObjectWithTag("Talk");
        messagePanel = messageCanvas.transform.Find("TalkPanel").gameObject;
        messageText = messagePanel.transform.Find("TalkText").gameObject.GetComponent<TextMeshProUGUI>();
        messagePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // �X�y�[�X�L�[�Ń��b�Z�[�W�̃E�C���h�E�����
        if (talking && GameController.gameState == "talk")
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                messagePanel.SetActive(false);
                messageText.text = "";
                talking = false;
                GameController.gameState = "playing";

                // �Q�[���ĊJ
                Time.timeScale = 1f;

                ItemDestroy();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
            switch (type)
            {
                case ItemType.light:
                    LightController.getLight = true;
                    break;
                case ItemType.bullet:
                    GameController.hasBullet += stockCount;
                    break;
                case ItemType.goldkey:
                    GameController.hasGoldKey += stockCount;
                    break;
                case ItemType.silverkey:
                    GameController.hasSilverKey += stockCount;
                    break;
                case ItemType.life:
                    if (PlayerController.hp < 5)
                    {
                        PlayerController.hp++;
                    }
                    break;
                default:
                    break;
            }

            // ���b�Z�[�W��\������ꍇ
            if (isTalk)
            {
                // �\�����郁�b�Z�[�W���쐬
                string message = message1 + stockCount + message2;

                GameController.gameState = "talk";
                talking = true;

                // UI�p�l���\��
                messagePanel.SetActive(true);

                // UI�e�L�X�g�̓��e���X�V
                messageText.text = message;

                // �Q�[���i�s��~�iUI�͓����j
                Time.timeScale = 0f;
            }
            else
            {
                ItemDestroy();
            }
        }
    }

    // ���ŉ��o
    void ItemDestroy()
    {
        GetComponent<CircleCollider2D>().enabled = false;
        rbody.gravityScale = 2.5f;
        rbody.AddForce(new Vector2(0, 6), ForceMode2D.Impulse);
        Destroy(gameObject, 0.5f);
    }
}
