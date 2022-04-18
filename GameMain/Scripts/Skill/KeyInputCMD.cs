using Google.Protobuf.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AltarOfSword
{
    public class KeyInputCMD : CMDInputBase
    {
        private List<int> ignoreCMDs = new List<int>()
        {
            SkillDefined.ECMD_Nothing,
            SkillDefined.ECMD_None,
            SkillDefined.ECMD_Hrzt,
            SkillDefined.ECMD_Jump,
            SkillDefined.ECMD_MouseL,
            SkillDefined.ECMD_MouseR
        };
        public override List<int> IgnoreCMDs => ignoreCMDs;

        public KeyInputCMD()
        {
            CMDBuffer = new RepeatedField<int>();
            for (var i = SkillDefined.ECMD_Nothing; i < SkillDefined.ECMD_Max; i++)
            {
                CMDDic.Add(i, new CMDItem(i));
                if (IgnoreCMDs.Contains(i)) continue;
                CMDBuffer.Add(i);
            }
        }

        public override void Update()
        {
            HorizontalCMD();
            VerticalCMD();
            JumpCMD();

            KeyDownCMD(KeyCode.Mouse0, SkillDefined.ECMD_MouseLD);
            KeyDownCMD(KeyCode.Mouse1, SkillDefined.ECMD_MouseRD);
            KeyDownCMD(KeyCode.Mouse2, SkillDefined.ECMD_MouseMD);

            KeyDownCMD(KeyCode.W, SkillDefined.ECMD_KeyW);
            KeyDownCMD(KeyCode.S, SkillDefined.ECMD_KeyS);

            foreach (var item in HoldCMD)
            {
                KeyHoldCMD(item.Key, item.Value);
            }
            KeyCombination(SkillDefined.ECMD_KeyS, KeySToCMD);
            KeyCombination(SkillDefined.ECMD_KeyW, KeyWToCMD);

            NothingCMD();

            foreach (var item in CMDDic)
            {
               if(item.Key!=SkillDefined.ECMD_Nothing&&item.Value.IsTrigger)
               {
                   UnityGameFramework.Runtime.Log.Info($"{item.Key}   触发了命令");
               }
            }
        }
        private void KeyHoldCMD(KeyCode holdKey, int cmd)
        {
            IsAnyTrigger = SetCMDState(cmd, Input.GetKey(holdKey));
        }

        private void KeyDownCMD(KeyCode holdKey, int cmd)
        {
            IsAnyTrigger = SetCMDState(cmd, Input.GetKeyDown(holdKey));
        }

        private void NothingCMD()
        {
            CMDItem cmd = CMDDic[SkillDefined.ECMD_Nothing];
            cmd.IsTrigger = !IsAnyTrigger;
        }

        private void HorizontalCMD()
        {
            if (Input.GetButtonDown("Horizontal"))
            {
                IsAnyTrigger = SetCMDState(SkillDefined.ECMD_HrztD, true);
            }
            float value = Input.GetAxisRaw("Horizontal");
            IsAnyTrigger = SetCMDState(SkillDefined.ECMD_Hrzt, value != 0.0f, value);
        }

        private void VerticalCMD()
        {
            if (Input.GetButtonDown("Vertical"))
            {
                IsAnyTrigger = SetCMDState(SkillDefined.ECMD_VertD, true);
            }
            float value = Input.GetAxisRaw("Vertical");
            IsAnyTrigger = SetCMDState(SkillDefined.ECMD_Vert, value != 0.0f, value);
        }

        private void JumpCMD()
        {
            if (Input.GetButtonDown("Jump"))
            {
                IsAnyTrigger = SetCMDState(SkillDefined.ECMD_JumpD, true);
            }
            float value = Input.GetAxisRaw("Jump");
            IsAnyTrigger = SetCMDState(SkillDefined.ECMD_Jump, value > 0.0f, value); //持续按键不触发任意命令
        }

        private Dictionary<KeyCode, int> HoldCMD { get; } = new Dictionary<KeyCode, int>()
        {
            {KeyCode.F,SkillDefined.ECMD_KeyF},
            {KeyCode.A,SkillDefined.ECMD_KeyA},
            {KeyCode.D,SkillDefined.ECMD_KeyD},
            {KeyCode.W,SkillDefined.ECMD_KeyW},
            {KeyCode.S,SkillDefined.ECMD_KeyS},
            {KeyCode.R,SkillDefined.ECMD_KeyR},
            {KeyCode.Q,SkillDefined.ECMD_KeyQ},
            {KeyCode.E,SkillDefined.ECMD_KeyE},
            {KeyCode.Alpha1,SkillDefined.ECMD_Key1},
            {KeyCode.Alpha2,SkillDefined.ECMD_Key2},
            {KeyCode.Alpha3,SkillDefined.ECMD_Key3},
            {KeyCode.Alpha4,SkillDefined.ECMD_Key4},
            {KeyCode.LeftShift,SkillDefined.ECMD_KeyShift},
            {KeyCode.RightShift,SkillDefined.ECMD_KeyShift},
            {KeyCode.RightControl,SkillDefined.ECMD_KeyCtrl},
            {KeyCode.LeftControl,SkillDefined.ECMD_KeyCtrl},
            {KeyCode.Mouse0,SkillDefined.ECMD_MouseL},
            {KeyCode.Mouse1,SkillDefined.ECMD_MouseR},
            {KeyCode.Mouse2,SkillDefined.ECMD_MouseM},
            {KeyCode.Tab,SkillDefined.ECMD_KeyTab},
        };

        private Dictionary<int, int> KeySToCMD = new Dictionary<int, int>()
        {
            {SkillDefined.ECMD_Key1,SkillDefined.ECMD_KeyS1},
            {SkillDefined.ECMD_Key2,SkillDefined.ECMD_KeyS2},
            {SkillDefined.ECMD_Key3,SkillDefined.ECMD_KeyS3},
            {SkillDefined.ECMD_Key4,SkillDefined.ECMD_KeyS4},
            {SkillDefined.ECMD_MouseL,SkillDefined.ECMD_KeySML},
            {SkillDefined.ECMD_MouseR,SkillDefined.ECMD_KeySMR},
            {SkillDefined.ECMD_MouseM,SkillDefined.ECMD_KeySMM},
            {SkillDefined.ECMD_Jump,SkillDefined.ECMD_KeySSpece}
        };

        private Dictionary<int, int> KeyWToCMD = new Dictionary<int, int>()
        {
            {SkillDefined.ECMD_Key1,SkillDefined.ECMD_KeyW1},
            {SkillDefined.ECMD_Key2,SkillDefined.ECMD_KeyW2},
            {SkillDefined.ECMD_Key3,SkillDefined.ECMD_KeyW3},
            {SkillDefined.ECMD_Key4,SkillDefined.ECMD_KeyW4},
            {SkillDefined.ECMD_MouseL,SkillDefined.ECMD_KeyWML},
            {SkillDefined.ECMD_MouseR,SkillDefined.ECMD_KeyWMR},
            {SkillDefined.ECMD_MouseM,SkillDefined.ECMD_KeyWMM},
            {SkillDefined.ECMD_Jump,SkillDefined.ECMD_KeyWSpece}
        };

        //组合键是否触发
        private void KeyCombination(int cmd, Dictionary<int, int> keyToCMD)
        {
            CMDItem cmdItem = CMDDic[cmd];
            if (!cmdItem.IsTrigger) return;
            foreach (var item in keyToCMD)
            {
                cmdItem = CMDDic[item.Key];
                if (!cmdItem.IsTrigger) continue;
                SetCMDState(item.Value, true);
            }
        }
    }
}