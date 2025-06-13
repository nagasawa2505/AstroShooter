using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static string gameState; // ƒQ[ƒ€‚Ìó‘ÔŠÇ—
    public static int hasBullet = 100; // ’i”
    public static int hasGoldKey; // ‹à‚ÌŒ®‚ÌŠ”
    public static int hasSilverKey; // ‹â‚ÌŒ®‚ÌŠ”

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
