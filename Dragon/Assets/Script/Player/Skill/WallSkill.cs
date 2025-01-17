using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSkill : BaseSkills
{

    [SerializeField]
    private GameObject wallPrefab = default;        //prefab格納用
    private Vector3 mousePos;                       // Mouseの位置
    private float posZ = 10.0f;                     // Mouseのｚ軸調整
    
    private GameObject wallObj = default;           // 壁オブジェクト格納用
    [SerializeField]
    private SkillController skillController;        //スクリプト格納用

    public bool GetWallObj() {return wallObj == null;}
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void WallGeneration()
    {
        if(Input.GetMouseButtonDown(1) && wallObj == null)
        {
            mousePos = Input.mousePosition;
            mousePos.z = posZ;
            wallObj = Instantiate(wallPrefab,Camera.main.ScreenToWorldPoint(mousePos), Quaternion.identity);
            skillController.Skills[3] -= Const.WALL_SKILL;
        }
    }

}
