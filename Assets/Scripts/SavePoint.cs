using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    bool inSavePointArea;

    // �g�[�N�L�����o�X�̃I�u�W�F�N�g��F���ł���悤�ɂ���
    GameObject messageCanvas;
    GameObject messagePanel;
    TextMeshProUGUI messageText;

    bool talking; // ��b���t���O

    // Start is called before the first frame update
    void Start()
    {
        messageCanvas = GameObject.FindGameObjectWithTag("Talk");
        messagePanel = messageCanvas.transform.Find("TalkPanel").gameObject;
        messageText = messagePanel.transform.Find("TalkText").gameObject.GetComponent<TextMeshProUGUI>();
        messagePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // E�L�[�������ꂽ��Z�[�u
        if (!talking && Input.GetKeyDown(KeyCode.E))
        {
            if (inSavePointArea)
            {
                SaveSystem.SaveGame();

                GameController.gameState = "talk";
                talking = true;

                // UI�p�l���\��
                messagePanel.SetActive(true);

                // UI�e�L�X�g�̓��e���X�V
                messageText.text = "�Z�[�u���܂���";

                // �Q�[���i�s��~�iUI�͓����j
                Time.timeScale = 0f;
            }
        }
        else if (talking && Input.GetKeyDown(KeyCode.Space))
        {
            messagePanel.SetActive(false);
            messageText.text = "";
            talking = false;
            GameController.gameState = "playing";

            // �Q�[���ĊJ
            Time.timeScale = 1f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            return;
        }
        inSavePointArea = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            return;
        }
        inSavePointArea = false;
    }
}
