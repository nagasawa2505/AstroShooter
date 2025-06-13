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

    // UIController.cs上におけるアイテムの残数、有無
    int hasBullet;
    int hasKeyG;
    int hasKeyS;
    int hasLife;
    bool hasLight;

    // Start is called before the first frame update
    void Start()
    {
        // UI初期表示
        UIDisplay();

        // Life初期化
        hasLife = PlayerController.hp;
        LifeDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        // 把握している内容と差がでたときだけUIを更新
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
        // static変数を取得してテキストとして表示
        hasBullet = GameController.hasBullet;
        bulletText.text = hasBullet.ToString();

        hasKeyG = GameController.hasGoldKey;
        goldKeyText.text = hasKeyG.ToString();

        hasKeyS = GameController.hasSilverKey;
        silverKeyText.text = hasKeyS.ToString();

        hasLight = LightController.getLight;
        lightPanel.SetActive(hasLight);
    }

    // ライフをいったん非表示
    void LifeReset()
    {
        for (int i = 0; i < lifes.Length; i++)
        {
            lifes[i].SetActive(false);
        }
    }

    // 引き継いだ分だけライフを表示
    void LifeDisplay()
    {
        LifeReset();

        for (int i = 0; i < hasLife; i++)
        {
            lifes[i].SetActive(true);
        }
    }
}
