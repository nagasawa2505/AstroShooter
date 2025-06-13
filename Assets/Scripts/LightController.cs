using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public static bool getLight = false; // ���C�g����肵�Ă��邩
    public static bool onLight = false; // ���C�g�_���Ă邩
    public GameObject playerLight; // ���C�g�̃I�u�W�F�N�g

    // Start is called before the first frame update
    void Start()
    {
        // ���C�g��_���邩�ǂ����������p��
        playerLight.SetActive(onLight);
    }

    // Update is called once per frame
    void Update()
    {
        // ���C�g���肵�ĂȂ���Ή������Ȃ�
        if (!getLight)
        {
            return;
        }

        // �I���I�t�؂�ւ�
        if (Input.GetKeyDown(KeyCode.L))
        {
            onLight = !onLight;
            playerLight.SetActive(onLight);
        }
    }
}
