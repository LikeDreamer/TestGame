using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace AltarOfSword
{
    public class UIStartForm : UGuiForm
    {
        private Button btnStartGame;
        private ProcedureMenu procedureMenu;
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            btnStartGame = this.transform.Find("BtnStartGame").GetComponent<Button>();
            if(btnStartGame!=null)
            {
                btnStartGame.onClick.AddListener(OnStartGameClick);
            }
        }

        private void OnStartGameClick()
        {
            if (procedureMenu == null) return;
            procedureMenu.StartGame();
        }

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            procedureMenu= userData as ProcedureMenu;
            if (procedureMenu!=null)
            {
                Log.Warning("ProcedureMenu is invalid when open MenuForm.");
                return;
            }
        }
    }
}
