using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomController : MonoBehaviour
{
    // セーブデータをロードした際の移動か
    public static bool isContinue;

    // ゲーム中共通して管理するドア番号
    public static int doorNumber;

    // プレイヤーのアニメと方向を計算
    int direction;
    float angleZ;

    // Start is called before the first frame update
    void Start()
    {
        // プレイヤー情報取得
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (isContinue)
        {
            // プレイヤーをセーブ時の位置に移動
            float posX = PlayerPrefs.GetFloat("posX");
            float posY = PlayerPrefs.GetFloat("posY");
            player.transform.position = new Vector2(posX, posY);

            return;
        }

        GameObject[] exits = GameObject.FindGameObjectsWithTag("Exit");
        for (int i = 0; i < exits.Length; i++)
        {
            Exit exit = exits[i].GetComponent<Exit>();
            if (exit == null) Debug.Log("NULL" + exits.Length);
            if (doorNumber == exit.doorNumber)
            {
                float x = exits[i].transform.position.x;
                float y = exits[i].transform.position.y;

                // 移動直後に再度移動しないように少しずらす
                switch (exit.direction)
                {
                    case ExitDirection.up:
                        y += 1;
                        direction = 1;
                        angleZ = 90;
                        break;
                    case ExitDirection.down:
                        y -= 1;
                        direction = 0;
                        angleZ = -90;
                        break;
                    case ExitDirection.left:
                        x -= 1;
                        direction = 2;
                        angleZ = 180;
                        break;
                    case ExitDirection.right:
                        x += 1;
                        direction = 3;
                        angleZ = 0;
                        break;
                    default:
                        break;
                }

                // 位置
                player.transform.position = new Vector3(x, y);
                // 角度
                player.GetComponent<PlayerController>().angleZ = angleZ;
                // アニメ
                player.GetComponent<Animator>().SetInteger("direction", direction);
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 静的メソッド（どこのシーンに何番のドア？)
    public static void ChangeScene(string scenename, int doornum)
    {
        // 次のシーンにドア番号が引き継がれる
        doorNumber = doornum;
        isContinue = false;
        SceneManager.LoadScene(scenename);
    }
}
