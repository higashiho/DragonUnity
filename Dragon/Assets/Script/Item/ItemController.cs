using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] prefabItem;    // �A�C�e���z��
    [SerializeField]
    private float sponTime = 5.0f;      // �A�C�e���X�|�[���Ԋu(s)
    [SerializeField]
    private float pos_z = 0;            // �`�揇������p
    private int item_number;            // �����_�������pindex

    private int item_counter;           // �V�[�����̃A�C�e�����J�E���g�p
    [SerializeField]
    private int item_Max = 10;               // �V�[�����̃A�C�e�����ő�l
    private float pos_x = 50f;           
    private float pos_y = 50f;

    void Start()
    {
        item_counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(item_counter < item_Max)
        random();
    }

    private void random()
    {
        // TODO 
        // ���ԂŐ������x���R���g���[��
        // �ϐ��ŊǗ�
        item_number = Random.Range(0, prefabItem.Length);

        float x = Random.Range(-pos_x, pos_x);
        float y = Random.Range(-pos_y, pos_y);

        Vector3 pos = new Vector3(x, y, pos_z);
        StartCoroutine("spon");
        Instantiate(prefabItem[item_number], pos, Quaternion.identity);
        item_counter++;
    }
    private IEnumerator spon()
    {
       
        yield return new WaitForSeconds(sponTime);
        //Instantiate(prefabItem[item_number], pos, Quaternion.identity);
    }
}
