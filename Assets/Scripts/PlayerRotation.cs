using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    // Unity�G�f�B�^���PlayerController���A�^�b�`����
    public PlayerController playerCnt;

    // Update is called once per frame
    void Update()
    {
        // PlayerController�������Ă���angleZ�̐��l����RotationBody����]
        transform.rotation = Quaternion.Euler(0, 0, playerCnt.angleZ);
    }
}
