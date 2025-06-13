using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleShooter : MonoBehaviour
{
    PlayerController playerCnt; // �v���C���[����X�N���v�g

    public GameObject riflePrefab; // �������郉�C�t��
    GameObject rifleObj; // �����������C�t�����
    Transform gate; // ���C�t���̔��ˌ�

    public GameObject bulletPrefab; // ��������e��
    public float shootSpeed = 12.0f; // �e�̑���
    public float shootDelay = 0.25f; // ���ˊԊu
    bool inAttack; // �U�����t���O

    // Start is called before the first frame update
    void Start()
    {
        playerCnt = GetComponent<PlayerController>();

        Vector3 playerPos = transform.position; // Player�̈ʒu

        // ���C�t���𐶐��������i�[
        // �v���C���[�̈ʒu�ɐ����A��]�Ȃ�
        rifleObj = Instantiate(riflePrefab, playerPos, Quaternion.identity);

        // ���ˌ����擾
        gate = rifleObj.transform.Find("Gate");

        // �v���C���[�̎q�I�u�W�F�N�g�ɂ���
        rifleObj.transform.SetParent(transform);
    }

    // Update is called once per frame
    void Update()
    {
        // ���C�t���̉�]��Z���i���s�j�̗D�揇��
        float rifleZ = -1; // ��{�̓��C�t������O�ɕ\��

        // ��������Ă�Ƃ��̓��C�t�������ɕ\��
        if (playerCnt.angleZ > 45 && playerCnt.angleZ < 135)
        {
            rifleZ = 1;
        }

        // �v���C���[�ɍ��킹�ă��C�t������]
        // ���C�t���̊G�̌����ɍ��킹��90�𑫂�
        rifleObj.transform.rotation = Quaternion.Euler(0, 0, (playerCnt.angleZ + 90));

        // ���C�t����Z���i���s�j�𒲐�
        rifleObj.transform.position = new Vector3(transform.position.x, transform.position.y, rifleZ);

        // �L�[�������ꂽ��e�۔���
        if (!inAttack && Input.GetKeyDown(KeyCode.R))
        {
            Attack();
        }
    }

    // �e�۔���
    void Attack()
    {
        // �c�e�`�F�b�N
        if (GameController.hasBullet <= 0)
        {
            return;
        }
        GameController.hasBullet--;

        inAttack = true;

        // �v���C���[�̊p�x�𐶐�����e�ۂ̊p�x�ɂ���
        Quaternion bulletRotate = Quaternion.Euler(0, 0, playerCnt.angleZ);

        // �e�ۂ𐶐����Ċi�[
        GameObject bulletObj = Instantiate(bulletPrefab, gate.position, bulletRotate);

        // angleZ�ƎO�p�֐����g����X������Y�������v�Z����i���W�A���l�j
        float x = Mathf.Cos(playerCnt.angleZ * Mathf.Deg2Rad);
        float y = Mathf.Sin(playerCnt.angleZ * Mathf.Deg2Rad);

        // Z=0�͏ȗ���
        Vector3 v = new Vector3(x, y).normalized * shootSpeed;

        // ���������e�ۂ�Rigidbody2D���擾
        Rigidbody2D bulletRigid = bulletObj.GetComponent<Rigidbody2D>();

        // �e�ۂ��΂�
        bulletRigid.AddForce(v, ForceMode2D.Impulse);

        // �U�����t���O�����Ԃ������ĉ�����
        Invoke("StopAttack", shootDelay);
    }

    // �U�����t���O��������
    void StopAttack()
    {
        inAttack = false;
    }
}
