using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    public GameObject itemPrefab; // ���g�ƂȂ�A�C�e��
    public Sprite openImage; // �J�����ꂽ�󔠂̊G
    public bool isClosed = true; // ���J�����ǂ���

    GameObject player; // �v���C���[���i�ʒu�j
    bool inBoxArea; // �ڐG�ł͂Ȃ��̈�ɓ��������ǂ����̃t���O
    public bool isEkey; // E�L�[�ŊJ����d�l���ǂ���
    public int arrangeId; // �Z�[�u�f�[�^�p�̎��ʎq

    // Start is called before the first frame update
    void Start()
    {
        // ���g������ς݂��`�F�b�N
        ExistCheck();
    }

    // Update is called once per frame
    void Update()
    {
        // E�L�[�������ꂽ�Ƃ��ɓ��肳����
        if (isClosed && isEkey && Input.GetKeyDown(KeyCode.E))
        {
            if (inBoxArea)
            {
                BoxOpen();
            }
        }
    }

    // �󔠂ɐڐG���邾���ŃA�C�e������肳����
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isEkey || !isClosed || !collision.gameObject.CompareTag("Player"))
        {
            return;
        }
        BoxOpen();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            return;
        }
        inBoxArea = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            return;
        }
        inBoxArea = false;
    }

    // �󔠊J������
    void BoxOpen()
    {
        // ���J���t���O��������
        isClosed = false;

        // �󔠂̊G�̍����ւ�
        GetComponent<SpriteRenderer>().sprite = openImage;

        // �v���C���[�̈ʒu�ɃA�C�e���𐶐�����
        if (itemPrefab != null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            Instantiate(itemPrefab, player.transform.position, Quaternion.identity);

            // ����ς݃��X�g�ɒǉ����ĂȂ���Βǉ�
            if (!SaveController.Instance.IsConsumed(this.tag, arrangeId))
            {
                SaveController.Instance.ConsumedEvent(this.tag, arrangeId);
            }
        }
    }

    // ���g������ς݂��`�F�b�N
    void ExistCheck()
    {
        if (SaveController.Instance.IsConsumed(this.tag, arrangeId))
        {
            isClosed = false;
            GetComponent<SpriteRenderer>().sprite = openImage;
        }
    }
}
