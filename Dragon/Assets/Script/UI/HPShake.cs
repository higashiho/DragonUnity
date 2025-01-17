using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPShake : MonoBehaviour
{
    [SerializeField]
    private Transform hpPos;
    [SerializeField]
    private float shakePower = 0;     // 揺れの強さ
    private Vector3 hpInitPos;
    
    [SerializeField]
    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        hpInitPos =hpPos.position;     // 開始時の位置を保存
    }

    // Update is called once per frame
    void Update()
    {
        if(!playerController.OnShield){
            shakeHp();
        }
    }

    private void shakeHp(){
        hpPos.position = hpInitPos + Random.insideUnitSphere * shakePower;      // ランダムに揺らす
    }
}