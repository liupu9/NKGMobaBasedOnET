//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年10月1日 21:22:19
//------------------------------------------------------------

namespace ETModel
{
    /// <summary>
    /// 绑定一个状态
    /// </summary>
    public class BindStateBuffSystem: BuffSystemBase
    {
        /// <summary>
        /// 自身下一个时间点
        /// </summary>
        private float selfNextimer;
        
        public override void OnInit(BuffDataBase BuffDataBase, Unit theUnitFrom, Unit theUnitBelongto)
        {
            //设置Buff来源Unit和归属Unit
            this.theUnitFrom = theUnitFrom;
            this.theUnitBelongto = theUnitBelongto;
            this.MSkillBuffDataBase = BuffDataBase;

            BuffTimerAndOverlayHelper.CalculateTimerAndOverlay(this, this.MSkillBuffDataBase);
        }

        public override void OnExecute()
        {
            BindStateBuffData tempData = MSkillBuffDataBase as BindStateBuffData;
            foreach (var VARIABLE in tempData.OriBuff)
            {
                this.theUnitBelongto.GetComponent<BuffManagerComponent>()
                        .AddBuff(Game.Scene.GetComponent<BuffPoolComponent>().AcquireBuff(VARIABLE, theUnitFrom, theUnitBelongto));
            }

            this.MBuffState = BuffState.Running;
        }

        public override void OnUpdate()
        {
            //只有不是永久Buff的情况下才会执行Update判断
            if ((this.MSkillBuffDataBase as BindStateBuffData).SustainTime + 1 > 0)
            {
                if (TimeHelper.ClientNow() > this.MaxLimitTime)
                {
                    this.MBuffState = BuffState.Finished;
                }
            }
        }

        public override void OnFinished()
        {
        }
    }
}