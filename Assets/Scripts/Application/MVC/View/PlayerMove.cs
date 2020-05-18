using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : View
{
    #region 常量
    private const float gravity = 9.8f;
    private const float m_jumpValue = 5;
    private const float moveSpeed = 13;

    private const float m_speedAddDistance = 200;
    private const float m_speedAddRae = 0.5f;
    private const float m_maxSpeed = 40;
    #endregion

    #region 事件
    #endregion

    #region 字段
    private float speed = 20;

    private CharacterController m_cc;
    private InputDirection m_inputDir = InputDirection.Null;
    private bool activeInput = false;
    private Vector3 m_mousePos;
    private int m_nowIndex = 1;
    private int m_targetIndex = 1;
    private float m_xDistance;
    private float m_yDistance;

    private bool m_isSlide = false;
    private float m_slideTime;

    private float m_speedAddCount;

    private GameModel gm;

    //标记速度
    private float m_markSpeed;
    private float m_addRate = 10;
    private bool m_isHit = false;

    //Item相关
    public int m_doubleTime = 1;
    private float m_skillTime;
    private IEnumerator MultiplyCor;//双倍金币协程
    private IEnumerator MagnetCor;
    private IEnumerator InvincibleCor;
    private SphereCollider m_magnetCollider;
    private bool m_isInvincible = false;

    //和射门相关
    private GameObject m_ball;
    private GameObject m_trail;
    private IEnumerator GoalCor;
    private bool m_isGoal = false;

    //更新皮肤
    public SkinnedMeshRenderer skinRenderer;
    public MeshRenderer ballRenderer;
    #endregion

    #region 属性
    public override string Name
    {
        get
        {
            return Consts.V_PlayerMoveName;
        }
    }

    public float Speed
    {
        get
        {
            return speed;
        }

        set
        {
            speed = value;
            if(speed > m_maxSpeed)
            {
                speed = m_maxSpeed; 
            }
        }
    }
    #endregion

    #region 方法

    #region 移动
    IEnumerator UpdateAction()
    {
        while (true)
        {
            if(!gm.IsPause && gm.IsPlay)
            {
                //更新UI Distance
                UpdateDis();

                m_yDistance -= gravity * Time.deltaTime;
                m_cc.Move((transform.forward * Speed + new Vector3(0, m_yDistance, 0)) * Time.deltaTime);
                MoveControl();
                UpdatePosition();
                UpdateSpeed();
            }
           
            yield return 0;
        }
    }

    //更新UI Distance
    private void UpdateDis()
    {
        DistanceArgs e = new DistanceArgs()
        {
            distance = (int)transform.position.z
        };

        SendEvent(Consts.E_UpdateDisEventName, e);
    }

    //获取输入
    void GetInputDirection()
    {
        //手势识别
        m_inputDir = InputDirection.Null;
        if (Input.GetMouseButtonDown(0))
        {
            activeInput = true;
            m_mousePos = Input.mousePosition;
        }
        if(Input.GetMouseButton(0) && activeInput == true)
        {
            Vector3 dir = Input.mousePosition - m_mousePos;
            if(dir.magnitude > 20)
            {
                if(Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
                {
                    if(dir.x > 0)
                    {
                        m_inputDir = InputDirection.Right;
                    }
                    else
                    {
                        m_inputDir = InputDirection.Left;
                    }
                }
                else
                {
                    if (dir.y > 0)
                    {
                        m_inputDir = InputDirection.Up;
                    }
                    else
                    {
                        m_inputDir = InputDirection.Down;
                    }
                }

                activeInput = false;
            }
        }

        //键盘识别
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        {
            m_inputDir = InputDirection.Up;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            m_inputDir = InputDirection.Down;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            m_inputDir = InputDirection.Left;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            m_inputDir = InputDirection.Right;
        }
    }

    //更新位置
    void UpdatePosition()
    {
        GetInputDirection();

        switch (m_inputDir)
        {
            case InputDirection.Null:
                break;
            case InputDirection.Right:
                if(m_targetIndex < 2)
                {
                    m_targetIndex++;
                    m_xDistance = 2;
                    SendMessage("AnimManager",m_inputDir);
                    Game.Instance.sound.PlayEffect("Se_UI_Huadong");
                }
                break;
            case InputDirection.Left:
                if(m_targetIndex > 0)
                {
                    m_targetIndex--;
                    m_xDistance = -2;
                    SendMessage("AnimManager", m_inputDir);
                    Game.Instance.sound.PlayEffect("Se_UI_Huadong");
                }
                break;
            case InputDirection.Up:
                if (m_cc.isGrounded)
                {
                    m_yDistance = m_jumpValue;
                    SendMessage("AnimManager", m_inputDir);
                    Game.Instance.sound.PlayEffect("Se_UI_Jump");
                }
                break;
            case InputDirection.Down:
                if (m_isSlide == false)
                {
                    m_isSlide = true;
                    m_slideTime = 0.733f;
                    SendMessage("AnimManager", m_inputDir);
                    Game.Instance.sound.PlayEffect("Se_UI_Slide");
                }
                
                break;
            default:
                break;
        }
    }

    //更新速度，随着跑的距离越来越远速度会越来越大，增加难度嘛 最大速度40
    void UpdateSpeed()
    {
        m_speedAddCount = +Speed * Time.deltaTime;
        if(m_speedAddCount > m_speedAddDistance)
        {
            m_speedAddCount = 0;
            Speed += m_speedAddRae;
        }
    }

    //移动控制
    void MoveControl()
    {
        //水平移动
        if(m_targetIndex != m_nowIndex)
        {
            float move = Mathf.Lerp(0, m_xDistance, moveSpeed * Time.deltaTime);
            transform.position += new Vector3(move, 0, 0);
            m_xDistance -= move;
            if(Mathf.Abs(m_xDistance) < 0.05f)
            {
                m_xDistance = 0;
                m_nowIndex = m_targetIndex;

                switch (m_nowIndex)
                {
                    case 0:
                        transform.position = new Vector3(-2, transform.position.y, transform.position.z);
                        break;
                    case 1:
                        transform.position = new Vector3(0, transform.position.y, transform.position.z);
                        break;
                    case 2:
                        transform.position = new Vector3(2, transform.position.y, transform.position.z);
                        break;
                }
            }
        }

        //滑动
        if (m_isSlide)
        {
            m_slideTime -= Time.deltaTime;
            if(m_slideTime <= 0)
            {
                m_isSlide = false;
                m_slideTime = 0;
            }
        }
    }
    #endregion

    #region 减速并恢复
    void HitObstacle()
    {
        if (m_isHit)
            return;
        m_isHit = true;
        m_markSpeed = Speed;
        Speed = 0;

        StartCoroutine(AddSpeed());
    }
    IEnumerator AddSpeed()
    {
        while (Speed <= m_markSpeed)
        {
            Speed += m_addRate * Time.deltaTime;
            yield return 0;
        }
        m_isHit = false;
    }
    #endregion

    #region 技能效果
    //吃金币
    public void HitCoin()
    {
        //print("吃到金币");
        CoinArgs e = new CoinArgs()
        {
            coin = m_doubleTime
        };

        SendEvent(Consts.E_UpdateCoinEventName, e);
    }

    //双倍金币
    public void HitMultiply()
    {
        //控制单一协程
        if(MultiplyCor != null)
        {
            StopCoroutine(MultiplyCor);
        }
        MultiplyCor = MultiplyCoroutine();
        StartCoroutine(MultiplyCor);
    }
    IEnumerator MultiplyCoroutine()
    {
        m_doubleTime = 2;

        float timer = m_skillTime;
        while (timer > 0)
        {
            if(!gm.IsPause && gm.IsPlay)
            {
                timer -= Time.deltaTime;
            }
            yield return 0;
        }
        //yield return new WaitForSeconds(m_skillTime);
        m_doubleTime = 1;
    }

    //吸铁石
    public void HitMagnet()
    {
        //控制单一协程
        if (MagnetCor != null)
        {
            StopCoroutine(MagnetCor);
        }
        MagnetCor = MagnetCoroutine();
        StartCoroutine(MagnetCor);
    }
    IEnumerator MagnetCoroutine()
    {
        m_magnetCollider.enabled = true;

        float timer = m_skillTime;
        while (timer > 0)
        {
            if (!gm.IsPause && gm.IsPlay)
            {
                timer -= Time.deltaTime;
            }
            yield return 0;
        }
        //yield return new WaitForSeconds(m_skillTime);
        m_magnetCollider.enabled = false;
    }

    //加时间
    public void HitAddTime()
    {
        //sendEvent
        //print("加时间");
        SendEvent(Consts.E_AddTimeEventName);
    }

    //吃到哨子进入无敌状态，无视大小Fence
    public void HitWhist()
    {
        //控制单一协程
        if (InvincibleCor != null)
        {
            StopCoroutine(InvincibleCor);
        }
        InvincibleCor = InvinclbleCoroutine();
        StartCoroutine(InvincibleCor);
    }
    IEnumerator InvinclbleCoroutine()
    {
        m_isInvincible = true;

        float timer = m_skillTime;
        while (timer > 0)
        {
            if (!gm.IsPause && gm.IsPlay)
            {
                timer -= Time.deltaTime;
            }
            yield return 0;
        }
        //yield return new WaitForSeconds(m_skillTime);
        m_isInvincible = false;
    }

    //碰撞到技能物品的统一处理方法
    public void HitItem(ItemKind item)
    {
        ItemArgs e = new ItemArgs()
        {
            hitCount = 0,
            kind = item

        };
        SendEvent(Consts.E_HitItemEventName, e);
        //switch (item)
        //{
        //    case ItemKind.InvincibleItem:
        //        HitWhist();
        //        break;
        //    case ItemKind.MultiplyItem:
        //        HitMultiply();
        //        break;
        //    case ItemKind.MagnetItem:
        //        HitMagnet();
        //        break;
        //    default:
        //        break;
        //}
    }
    #endregion
    
    /************************************************/
    //射门相关
    public void OnGoalClick()
    {
        if(GoalCor != null)
        {
            StopCoroutine(GoalCor);
        }
        SendMessage("MessagePlayShoot");//播放射门动画
        m_ball.SetActive(false);
        m_trail.SetActive(true);
        GoalCor = MoveBall();
        StartCoroutine(GoalCor);
    }
    IEnumerator MoveBall()
    {
        while (true)
        {
            if (!gm.IsPause && gm.IsPlay)
            {
                m_trail.transform.Translate(transform.forward * 40 * Time.deltaTime, Space.Self);
            }
            
            yield return 0;
        }
    }
    //球进了
    public void HitBallDoor()
    {
        //1、停止射门协程
        StopCoroutine(GoalCor);
        //2、归位
        m_trail.transform.localPosition = new Vector3(0, 1.5f, 2.6f);
        m_trail.SetActive(false);
        m_ball.SetActive(true);
        //3、isGoal设为true
        m_isGoal = true;
        //4、生成进球的特效
        Game.Instance.objectPool.Spawn("FX_GOAL", m_trail.transform.parent);
        //5、播放进球的音效
        Game.Instance.sound.PlayEffect("Se_UI_Goal");
        //6、给UIBoard发送事件 使m_goalCount + 1
        SendEvent(Consts.E_ShootGoalEventName);
    }
    #endregion

    #region Unity回调
    void Awake()
    {
        m_cc = GetComponent<CharacterController>();
        gm = GetModel<GameModel>();

        m_skillTime = gm.SkillTime;
        m_magnetCollider = GetComponentInChildren<SphereCollider>();
        m_magnetCollider.enabled = false;

        //对射门相关字段赋值
        m_ball = transform.Find("Ball").gameObject;
        m_trail = GameObject.Find("Trail");
        m_trail.SetActive(false);

        //更新皮肤
        skinRenderer.material = Game.Instance.staticData.GetPlayerClothesInfo(gm.PlayerClothesEquipped.skinId, gm.PlayerClothesEquipped.clothesId).footballMat;
        ballRenderer.material = Game.Instance.staticData.GetFootballInfo(gm.FootballEquipped).footballMat;
    }
    void Start()
    {
        StartCoroutine(UpdateAction());
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.V))
        //{
        //    gm.IsPause = true;
        //}
        //else if (Input.GetKeyDown(KeyCode.M))
        //{
        //    gm.IsPause = false;
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == Tag.smallFence)
        {
            if (m_isInvincible)
                return;

            //播放声音
            Game.Instance.sound.PlayEffect("Se_UI_Hit");

            other.gameObject.SendMessage("HitPlayer", transform.position);
            HitObstacle();
        }
        else if(other.tag == Tag.bigFence)
        {
            if (m_isInvincible)
                return;
            if (m_isSlide )
                return;

            //播放声音
            Game.Instance.sound.PlayEffect("Se_UI_Hit");

            other.gameObject.SendMessage("HitPlayer", transform.position);
            HitObstacle();
        }
        else if (other.tag == Tag.block)//游戏结束
        {
            //播放声音
            Game.Instance.sound.PlayEffect("Se_UI_End");

            other.gameObject.SendMessage("HitPlayer", transform.position);

            //SendEvent 游戏结束
            SendEvent(Consts.E_EndGameEventName);
        }
        else if (other.tag == Tag.smallBlock)//游戏结束
        {
            //播放声音
            Game.Instance.sound.PlayEffect("Se_UI_End");

            other.transform.parent.parent.SendMessage("HitPlayer", transform.position);

            //SendEvent 游戏结束
            SendEvent(Consts.E_EndGameEventName);
        }
        else if (other.tag == Tag.beforeTrigger)//汽车触发区域
        {
            other.transform.parent.SendMessage("HitTrigger",SendMessageOptions.RequireReceiver);
        }
        else if (other.tag == Tag.beforeGoalTrigger)//射门触发区域
        {
            //准备射门
            //发消息给UIBoard
            SendEvent(Consts.E_HitGoalTriggerEventName);
            //生成加速特效
            Game.Instance.objectPool.Spawn("FX_JiaSu", m_trail.transform.parent);
        }
        else if (other.tag == Tag.goalKeeper)//撞到守门员
        {
            HitObstacle();
            //守门员被撞飞
            other.transform.parent.parent.parent.SendMessage("HitGoalKeeper", SendMessageOptions.RequireReceiver);

        }
        else if (other.tag == Tag.ballDoor)//撞到球门
        {
            if (m_isGoal)
            {
                m_isGoal = false;
                return;
            }
            //1、减速
            HitObstacle();
            //2、球网上身
            Game.Instance.objectPool.Spawn("Effect_QiuWang", m_trail.transform.parent);
            //3、播放球门动画
            //4、球门球网消失
            other.transform.parent.parent.SendMessage("HitDoor",m_nowIndex);
            
        }
    }
    #endregion

    #region 事件回调
    public override void RegisterAttentionEvent()
    {
        AttentionEventList.Add(Consts.E_ClickGoalButtonEventName);
    }

    public override void HandleEvent(string eventName, object data)
    {
        switch(eventName)
        {
            case Consts.E_ClickGoalButtonEventName:
                OnGoalClick();
                break;
        }
    }
    #endregion

    #region 帮助方法
    #endregion
}
