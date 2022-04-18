//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework.Resource;
using System.Collections.Generic;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace AltarOfSword
{
    public class ActorEntityLogic:EntityLogic
    {

    }

    public class ActorEntityData : EntityData
    {
        public ActorEntityData(int entityId, int typeId) : base(entityId, typeId)
        {
        }
    }


    public class ProcedureMain : ProcedureBase
    {
        private const float GameOverDelayedSeconds = 2f;

        private readonly Dictionary<GameMode, GameBase> m_Games = new Dictionary<GameMode, GameBase>();
        private GameBase m_CurrentGame = null;
        private bool m_GotoMenu = false;
        private float m_GotoMenuDelaySeconds = 0f;

        public override bool UseNativeDialog
        {
            get
            {
                return false;
            }
        }

        public void GotoMenu()
        {
            m_GotoMenu = true;
        }

        protected override void OnInit(ProcedureOwner procedureOwner)
        {
            base.OnInit(procedureOwner);

            m_Games.Add(GameMode.Survival, new SurvivalGame());
        }

        protected override void OnDestroy(ProcedureOwner procedureOwner)
        {
            base.OnDestroy(procedureOwner);

            m_Games.Clear();
        }

        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);

            //m_GotoMenu = false;
            //GameMode gameMode = (GameMode)procedureOwner.GetData<VarByte>("GameMode").Value;
            //m_CurrentGame = m_Games[gameMode];
            //m_CurrentGame.Initialize();
            GameEntry.Entity.AddEntityGroup(EntityDefined.EG_Actors,60,16,60,0);
            GameEntry.Entity.AddEntityGroup(EntityDefined.EG_Monsters,60,16,60,0);
            GameEntry.Entity.AddEntityGroup(EntityDefined.EG_SkillEffects,60,16,60,0);

            //GameEntry.Entity.ShowEntity(10000);
            GameEntry.Entity.ShowEntity(typeof(ActorSkillLogic),EntityDefined.EG_Actors, Constant.AssetPriority.ActorAsset,1001);


            Log.Debug("加载完成");

            GameEntry.UI.OpenUIForm(EUIForm.UIStartForm);
        }

        private void LoadSkillData()
        {
            //string skillPath = $"{Application.streamingAssetsPath}/SkillData/SkillData.xky";

            string skillPath = "Assets/GameMain/SkillData/SkillData.xky";

            LoadAssetCallbacks loadAssetCallbacks = new LoadAssetCallbacks(
                (assetName, asset, duration, userData) =>
                {
                    UnityEngine.TextAsset textAsset = asset as UnityEngine.TextAsset;
                    byte[] butes = textAsset.bytes;
                    Log.Info($"加载成功 {butes.Length}", skillPath);
                },

                (assetName, status, errorMessage, userData) =>
                {
                    Log.Error("加载失败", skillPath, assetName, errorMessage);
                });
            GameEntry.Resource.LoadAsset(skillPath, typeof(UnityEngine.TextAsset), loadAssetCallbacks);
        }

        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            if (m_CurrentGame != null)
            {
                m_CurrentGame.Shutdown();
                m_CurrentGame = null;
            }

            base.OnLeave(procedureOwner, isShutdown);
        }

        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

            return;
            if (m_CurrentGame != null && !m_CurrentGame.GameOver)
            {
                m_CurrentGame.Update(elapseSeconds, realElapseSeconds);
                return;
            }

            if (!m_GotoMenu)
            {
                m_GotoMenu = true;
                m_GotoMenuDelaySeconds = 0;
            }

            m_GotoMenuDelaySeconds += elapseSeconds;
            if (m_GotoMenuDelaySeconds >= GameOverDelayedSeconds)
            {
                procedureOwner.SetData<VarInt32>("NextSceneId", GameEntry.Config.GetInt("Scene.Menu"));
                ChangeState<ProcedureChangeScene>(procedureOwner);
            }
        }
    }
}
