using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    // �������g
    public static SaveController Instance;

    // ����ς݃��X�g
    public HashSet<(string tag, int arrangeId)> consumedEvent = new HashSet<(string tag, int arrangeId)>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            // �C���X�^���X��ʃV�[���Ɏ����z��
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Instance�����݂��遨�������g��2�ڈȍ~�̃V�[���̃C���X�^���X�ł���
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

    // ���X�g�ǉ�
    public void ConsumedEvent(string tag, int arrangeId)
    {
        consumedEvent.Add((tag, arrangeId));
    }

    // ���X�g�v�f���݃`�F�b�N
    public bool IsConsumed(string tag, int arrangeId)
    {
        return consumedEvent.Contains((tag, arrangeId));
    }
}
