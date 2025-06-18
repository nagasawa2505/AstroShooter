using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleController : MonoBehaviour
{
    public string sceneName; // �Q�[���X�^�[�g���̃V�[����
    public Button continueButton;

    // Start is called before the first frame update
    void Start()
    {
        // �Z�[�u�f�[�^��������Α�������{�^���𖳌���
        string lastScene = PlayerPrefs.GetString("Scene");
        if (string.IsNullOrEmpty(lastScene))
        {
            continueButton.interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // �ŏ�����
    public void GameStart()
    {
        // �Z�[�u�f�[�^�폜
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(sceneName);
    }

    // ��������
    public void ContinueStart()
    {
        SaveSystem.LoadGame();
    }

    // �Z�[�u�f�[�^�폜
    public void SaveReset()
    {
        // �Z�[�u�f�[�^�̏���
        PlayerPrefs.DeleteAll();
        continueButton.interactable = false;
    }
}
