//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework.Entity;
using GameFramework.Event;
using GameFramework.Resource;
using System.Collections.Generic;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace AltarOfSword
{
    public class ProcedureMain : ProcedureBase
    {
        private const float GameOverDelayedSeconds = 2f;

        private readonly Dictionary<GameMode, GameBase> Games = new Dictionary<GameMode, GameBase>();
        private GameBase currentGame = null;
        private bool gotoMenu = false;
        private float m_GotoMenuDelaySeconds = 0f;

        private UIOverForm overForm;

        public override bool UseNativeDialog => false;
      
        public void GotoMenu()
        {
            gotoMenu = true;
        }

        protected override void OnInit(ProcedureOwner procedureOwner)
        {
            base.OnInit(procedureOwner);

            Games.Add(GameMode.Survival, new SurvivalGame());
        }

        protected override void OnDestroy(ProcedureOwner procedureOwner)
        {
            base.OnDestroy(procedureOwner);
            Games.Clear();
        }

        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);

            GameEntry.UI.OpenUIForm(EUIForm.UIOverForm, this);

            GameEntry.Event.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);

            gotoMenu = false;
            GameMode gameMode = (GameMode)procedureOwner.GetData<VarByte>("GameMode").Value;
            currentGame = Games[gameMode];
            currentGame.Initialize();

            IEntityGroup entityGroup = GameEntry.Entity.GetEntityGroup(EntityDefined.EG_Actors);
            if (entityGroup != null)
            {
                entityGroup.InstanceAutoReleaseInterval = 60;
                entityGroup.InstanceExpireTime = 60;
            }
            else
            {
                GameEntry.Entity.AddEntityGroup(EntityDefined.EG_Actors, 60, 16, 60, 0);
            }
            GameEntry.Entity.AddEntityGroup(EntityDefined.EG_Monsters,60,16,60,0);
            GameEntry.Entity.AddEntityGroup(EntityDefined.EG_SkillEffects,60,16,60,0);

            GameEntry.Entity.ShowEntity(typeof(ActorSkillLogic),EntityDefined.EG_Actors, Constant.AssetPriority.ActorAsset,1001);

            Log.Debug("加载完成");

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
            if (currentGame != null)
            {
                currentGame.Shutdown();
                currentGame = null;
            }

            if(overForm!=null)
            {
                overForm.Close(true);
                overForm = null;
            }

            GameEntry.Event.Unsubscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);

            IEntityGroup entityGroup= GameEntry.Entity.GetEntityGroup(EntityDefined.EG_Actors);
            if(entityGroup!=null)
            {
                entityGroup.InstanceAutoReleaseInterval = 0.0f;
                entityGroup.InstanceExpireTime = 0.0f;
            }

            base.OnLeave(procedureOwner, isShutdown);
        }

        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

            //if (currentGame != null && !currentGame.GameOver)
            //{
            //    currentGame.Update(elapseSeconds, realElapseSeconds);
            //    return;
            //}

            //if (!gotoMenu)
            //{
            //    gotoMenu = true;
            //    m_GotoMenuDelaySeconds = 0;
            //}

            if (!gotoMenu) return;

            m_GotoMenuDelaySeconds += elapseSeconds;
            if (m_GotoMenuDelaySeconds >= GameOverDelayedSeconds)
            {
                procedureOwner.SetData<VarInt32>("NextSceneId", GameEntry.Config.GetInt("Scene.Menu"));
                ChangeState<ProcedureChangeScene>(procedureOwner);
            }
        }
        private void OnOpenUIFormSuccess(object sender, GameEventArgs e)
        {
            OpenUIFormSuccessEventArgs ne = (OpenUIFormSuccessEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }
            overForm = (UIOverForm)ne.UIForm.Logic;
        }
    }
}
