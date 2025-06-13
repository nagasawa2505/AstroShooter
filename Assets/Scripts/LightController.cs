using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public static bool getLight = false; // ライトを入手しているか
    public static bool onLight = false; // ライト点いてるか
    public GameObject playerLight; // ライトのオブジェクト

    // Start is called before the first frame update
    void Start()
    {
        // ライトを点けるかどうかを引き継ぐ
        playerLight.SetActive(onLight);
    }

    // Update is called once per frame
    void Update()
    {
        // ライト入手してなければ何もしない
        if (!getLight)
        {
            return;
        }

        // オンオフ切り替え
        if (Input.GetKeyDown(KeyCode.L))
        {
            onLight = !onLight;
            playerLight.SetActive(onLight);
        }
    }
}
