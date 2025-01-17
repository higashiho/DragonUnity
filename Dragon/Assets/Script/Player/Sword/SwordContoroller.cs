using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordContoroller : BaseSword
{
    
    [SerializeField]
    private Factory objectPool;                     // オブジェクトプール用コントローラー格納
    private BaseSkills shockWaveObj;                // 衝撃波オブジェク
    
    /// 取得系込み変数  
    public bool CoroutineBool{
        get { return coroutineBool ;}
        set { coroutineBool = value ;}
    }
    // 押している時間
    [SerializeField, HeaderAttribute("貯め時間")]
    private float onTime = 0;               
    public float OnTime{
        get { return onTime ;}
        set { onTime = value ;}
    }
    // チャージ中かどうか
    private bool onCharge = false;              
    public bool OnCharge {
        get { return onCharge; }
		set { onCharge = value; }
        }
    // アタック中かどうか
    private bool onAttack;
    public bool OnAttack
    {
        get { return onAttack; }
        set { onAttack = value; }
    }
    

    // Start is called before the first frame update
    void Start()
    {
        renderer.enabled = false;
        collider.enabled = false;
        objectPool = GameObject.Find("ObjectPool").GetComponent<Factory>();

        rotAngleZ = 10.0f;
        startAngleZ = 150.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Approximately(Time.timeScale, 0f))
            return;

        attack();
    }

    //　攻撃挙動
    private void attack()
    {
        // スキルアイテムがない場合
        if(skillController.Skills[4] < Const.SHOCK_SKILL)
        {
            if (!coroutineBool && Input.GetMouseButtonDown(0))
            {
                onAttack = true;
                nomalAttack();
            }
        }
        // スキルアイテムが指定個数ある場合
        else
        {

            if(!coroutineBool)
            {

                var m_nomalSpeed = player.NomalPlayerSpeed;
                if(Input.GetMouseButton(0))
                {
                    onAttack = true;
                    onTime += Time.deltaTime;
                    // Shieldがある場合スピードダウン
                    if(player.OnShield)
                        player.PlayerSpeed
                        = m_nomalSpeed * Const.CHARGE_SPEED_DOWN; 
                    renderer.enabled = true;
                    onCharge = true;
                }

                else if(Input.GetMouseButtonUp(0))
                {
                    nomalAttack();
                    if(onTime > Const.CHARGE_TIMER_MAX)
                        shockWave();
                    else onTime = default;

                    onCharge = false;
                }
            }
        }
    }

    // 普段の攻撃
    private void nomalAttack()
    {
        coroutineBool = true;
        StartCoroutine("Shake");
    }

    // 衝撃波を出す攻撃
    private void shockWave()
    {
        shockWaveObj = objectPool.Launch(this.transform.position, objectPool.ShockWaveQueue, objectPool.ShockWaveobj);

        skillController.Skills[4] -= Const.SHOCK_SKILL;
        
        
        // 衝撃波が拡大する時２倍のスキルアイテムを使い拡大する衝撃波を生成
        // スキルアイテムが４つ以上ないと大きくならないようにする
        if(onTime >= Const.MAX_SHOCKWAVE_TIME && skillController.Skills[4] >= Const.SHOCK_SKILL)
        {
            shockWaveObj.GetComponent<ShockWave>().SetOnSizeUp(true);
                    
            Vector3 startScale = new Vector3(0.8f, 0.2f,1.0f);      // 最初の大きさ
            shockWaveObj.transform.localScale = startScale;

            skillController.Skills[4] -= Const.SHOCK_SKILL;
        }
            

        onTime = default;
    }


    
    // 回す処理、動いている最中のみレンダラーと当たり判定がオン
    private IEnumerator Shake()
    {
        collider.enabled = true;
        renderer.enabled = true;
        for (int turn = 0; turn < stopRotation; turn++)
        {
            transform.Rotate(0, 0, -rotAngleZ);
            yield return new WaitForSeconds(waitTime);
        }
        
        transform.Rotate(0, 0, startAngleZ);
        renderer.enabled = false;
        collider.enabled = false;
        yield return new WaitForSeconds(attackWait);
        onAttack = false;
        coroutineBool = false;
    }
}
