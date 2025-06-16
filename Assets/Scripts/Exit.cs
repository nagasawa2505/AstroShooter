using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 出口として機能したときのプレイヤーの位置
public enum ExitDirection
{
    right,
    left,
    up,
    down
}

public class Exit : MonoBehaviour
{
    public string sceneName; // 切り替え先のシーン名
    public int doorNumber; // 切り替え先の出入口との連動番号

    // 自作した列挙型でプレイヤーをどの位置におく出口なのか決めておく変数
    public ExitDirection direction = ExitDirection.down;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // シーン切り替え
            RoomController.ChangeScene(sceneName, doorNumber);
        }
    }
}
