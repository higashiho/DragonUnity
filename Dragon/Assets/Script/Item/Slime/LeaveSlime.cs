using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveSlime : MonoBehaviour
{
    [SerializeField]
    private GameObject player;   // �v���C���[�A�^�b�`�p
    [SerializeField]
    private GameObject slime;    // slime �A�^�b�`�p
    private Vector3 dir;         // �����ۑ��p
    private Quaternion lookAtRotation;
    private Quaternion offsetRotation;
    [SerializeField]
    private Vector3 forward = -Vector3.forward;
    private bool isArea;
    private Vector3 pos;  // slime�̍��W�ۑ��p
    private float waitTime = 3f;

    void Start()
    {
       
        isArea = false;
       
    }

    // Update is called once per frame
    void Update()
    {
        if (isArea)
        {
            calcRotate();
            move();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isArea = true;
            Debug.Log(isArea);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine("waitFlagBreak");
        }
    }
    // �����v�Z�֐�(���������v�Z����move()�ňړ�������)
    private void calcRotate()
    {
        dir = player.transform.position - slime.transform.position;
        lookAtRotation = Quaternion.LookRotation(dir, Vector3.up);
        offsetRotation = Quaternion.FromToRotation(forward, Vector3.forward);

        slime.transform.rotation = lookAtRotation * offsetRotation;
    }
    // �ړ��֐�(������slime���ړ�������)
    private void move()
    {
        pos = slime.transform.position;
        pos += Vector3.up;
        slime.transform.position = pos;
    }
    // ��莞�Ԍ�t���O��܂�֐�
    private IEnumerator waitFlagBreak()
    {
        yield return new WaitForSeconds(waitTime);
        isArea = false;
    }
}
