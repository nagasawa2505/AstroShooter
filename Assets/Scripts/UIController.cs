using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI bulletText;
    public TextMeshProUGUI goldKeyText;
    public TextMeshProUGUI silverKeyText;
    public GameObject lightPanel;
    public GameObject[] lifes;

    // UIController.cs��ɂ�����A�C�e���̎c���A�L��
    int hasBullet;
    int hasKeyG;
    int hasKeyS;
    int hasLife;
    bool hasLight;

    // Start is called before the first frame update
    void Start()
    {
        // UI�����\��
        UIDisplay();

        // Life������
        hasLife = PlayerController.hp;
        LifeDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        // �c�����Ă�����e�ƍ����ł��Ƃ�����UI���X�V
        if (hasBullet != GameController.hasBullet)
        {
            hasBullet = GameController.hasBullet;
            bulletText.text = hasBullet.ToString();
        }

        if (hasKeyG != GameController.hasGoldKey)
        {
            hasKeyG = GameController.hasGoldKey;
            goldKeyText.text = hasKeyG.ToString();
        }

        if (hasKeyS != GameController.hasSilverKey)
        {
            hasKeyS = GameController.hasSilverKey;
            silverKeyText.text = hasKeyS.ToString();
        }

        if (!hasLight && LightController.getLight)
        {
            hasLight = true;
            lightPanel.SetActive(hasLight);
        }

        if (hasLife != PlayerController.hp)
        {
            hasLife = PlayerController.hp;
            LifeDisplay();
        }
    }

    void UIDisplay()
    {
        // static�ϐ����擾���ăe�L�X�g�Ƃ��ĕ\��
        hasBullet = GameController.hasBullet;
        bulletText.text = hasBullet.ToString();

        hasKeyG = GameController.hasGoldKey;
        goldKeyText.text = hasKeyG.ToString();

        hasKeyS = GameController.hasSilverKey;
        silverKeyText.text = hasKeyS.ToString();

        hasLight = LightController.getLight;
        lightPanel.SetActive(hasLight);
    }

    // ���C�t�����������\��
    void LifeReset()
    {
        for (int i = 0; i < lifes.Length; i++)
        {
            lifes[i].SetActive(false);
        }
    }

    // �����p�������������C�t��\��
    void LifeDisplay()
    {
        LifeReset();

        for (int i = 0; i < hasLife; i++)
        {
            lifes[i].SetActive(true);
        }
    }
}
