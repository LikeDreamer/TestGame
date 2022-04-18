﻿using GameFramework.Resource;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace AltarOfSword
{
    public class ProcedureSplash : ProcedureBase
    {
        public override bool UseNativeDialog => true;
       
        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

            // TODO: 这里可以播放一个 Splash 动画
            // ...

            if (GameEntry.Base.EditorResourceMode)
            {
                // 编辑器模式
                Log.Info("Editor resource mode detected.");
                ChangeState<ProcedurePreload>(procedureOwner);
            }
            else
            {
                //单机模式
                Log.Info("单机模式资源初始化.");
                ChangeState<ProcedureInitResources>(procedureOwner);
            }

            //if (GameEntry.Base.EditorResourceMode)
            //{
            //    // 编辑器模式
            //    Log.Info("Editor resource mode detected.");
            //    ChangeState<ProcedurePreload>(procedureOwner);
            //}
            //else if (GameEntry.Resource.ResourceMode == ResourceMode.Package)
            //{
            //    // 单机模式
            //    Log.Info("Package resource mode detected.");
            //    ChangeState<ProcedureInitResources>(procedureOwner);
            //}
            //else
            //{
            //    // 可更新模式
            //    Log.Info("Updatable resource mode detected.");
            //    ChangeState<ProcedureCheckVersion>(procedureOwner);
            //}
        }
    }
}
