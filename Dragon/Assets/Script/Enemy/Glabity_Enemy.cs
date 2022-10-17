using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glabity_Enemy : MonoBehaviour
{

    // 2D���W�b�h�{�f�B���󂯎��ϐ�
    private Rigidbody2D rb2D;

    private float move_speed = 0.01f; //�ǐՃX�s�[�h

    private GameObject PlayerObject; // player�I�u�W�F�N�g���󂯎���
    private Transform Player; // �v���C���[�̍��W���Ȃǂ��󂯎���

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        // player��GameObject��T���Ď擾
        PlayerObject = GameObject.Find("Player");
        // player��Transform�����擾
        Player = PlayerObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        enemyMove();
        dispOver();
    }

    private void enemyMove()
    {
        // �L�����N�^�[�̑傫���B�����ɂ���Ɣ��]�����
        Vector3 scale = transform.localScale;
        if (rb2D.velocity.x > 1)      // �E�����ɓ����Ă���
            scale.x = 1;  // �ʏ����(�X�v���C�g�Ɠ����E����)
        else if (rb2D.velocity.x < -1) // �������ɓ����Ă���
            scale.x = -1; // ���]
        // �X�V
        transform.localScale = scale;

        Vector2 e_pos = transform.position;  // ����(�G�L�����N�^)�̐��E���W
        Vector2 p_pos = Player.position;  // �v���C���[�̐��E���W

        // �v���C���[�̕����ɓ����x�N�g��(move_speed�ő��x�𒲐�)
        Vector2 force = (p_pos - e_pos) * move_speed;
        // ���킶��ǐ�
        rb2D.AddForce(force, ForceMode2D.Force);
    }
    private void dispOver() // ���E�O����
    {
        // ��ʂ̍����̍��W���擾 (���ザ��Ȃ��̂Œ���)
        Vector2 screen_LeftBottom = Camera.main.ScreenToWorldPoint(Vector3.zero);
        // ��ʂ̉E��̍��W���擾 (�E������Ȃ��̂Œ���)
        Vector2 screen_RightTop = Camera.main.ScreenToWorldPoint(
            new Vector3(Screen.width, Screen.height, 0));

        // ���݂̓G�L�����N�^�[�̈ړ����(�����Ƌ���)
        Vector2 enemy_velocity = rb2D.velocity;
        // ���݂̓G�L�����N�^�[�̈ʒu���W
        Vector2 enemy_pos = transform.position;

        // ��ʍ��[�ɒB�������A�v���C���[���������ɓ����Ă�����A�E�����̗͂ɔ��]����
        if ((enemy_pos.x < screen_LeftBottom.x) && (enemy_velocity.x < 0))
            enemy_velocity.x *= -1;
        // ��ʉE�[�ɒB�������A�v���C���[���E�����ɓ����Ă�����A�������̗͂ɔ��]����
        if ((enemy_pos.x > screen_RightTop.x) && (enemy_velocity.x > 0))
            enemy_velocity.x *= -1;
        // ��ʏ�[�ɒB�������A�v���C���[��������ɓ����Ă�����A�������̗͂ɔ��]����
        if ((enemy_pos.y > screen_RightTop.y) && (enemy_velocity.y > 0))
            enemy_velocity.y *= -1;
        // ��ʉ��[�ɒB�������A�v���C���[���������ɓ����Ă�����A������̗͂ɔ��]����
        if ((enemy_pos.y < screen_LeftBottom.y) && (enemy_velocity.y < 0))
            enemy_velocity.y *= -1;

        // �X�V
        rb2D.velocity = enemy_velocity;
    }
}
