using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    // 自分自身
    public static SaveController Instance;

    // 消費済みリスト
    public HashSet<(string tag, int arrangeId)> consumedEvent = new HashSet<(string tag, int arrangeId)>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            // インスタンスを別シーンに持ち越す
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Instanceが存在する→自分自身が2つ目以降のシーンのインスタンスである
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // リスト追加
    public void ConsumedEvent(string tag, int arrangeId)
    {
        consumedEvent.Add((tag, arrangeId));
    }

    // リスト要素存在チェック
    public bool IsConsumed(string tag, int arrangeId)
    {
        return consumedEvent.Contains((tag, arrangeId));
    }
}
