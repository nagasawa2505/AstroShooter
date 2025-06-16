using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �o���Ƃ��ċ@�\�����Ƃ��̃v���C���[�̈ʒu
public enum ExitDirection
{
    right,
    left,
    up,
    down
}

public class Exit : MonoBehaviour
{
    public string sceneName; // �؂�ւ���̃V�[����
    public int doorNumber; // �؂�ւ���̏o�����Ƃ̘A���ԍ�

    // ���삵���񋓌^�Ńv���C���[���ǂ̈ʒu�ɂ����o���Ȃ̂����߂Ă����ϐ�
    public ExitDirection direction = ExitDirection.down;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // �V�[���؂�ւ�
            RoomController.ChangeScene(sceneName, doorNumber);
        }
    }
}
