using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static string gameState; // �Q�[���̏�ԊǗ�
    public static int hasBullet = 100; // �i��
    public static int hasGoldKey; // ���̌��̏�����
    public static int hasSilverKey; // ��̌��̏�����

    // Start is called before the first frame update
    void Start()
    {
        gameState = "playing";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
