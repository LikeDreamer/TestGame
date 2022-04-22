using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace AltarOfSword
{
    public class UIOverForm : UGuiForm
    {
        private Button btnOverGame;
        private ProcedureMain procedureMain;
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            btnOverGame = this.transform.Find("BtnOverGame").GetComponent<Button>();
            if (btnOverGame != null)
            {
                btnOverGame.onClick.AddListener(OnOverGameClick);
            }
        }

        private void OnOverGameClick()
        {
            if (procedureMain == null) return;
            procedureMain.GotoMenu();
        }

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            procedureMain = userData as ProcedureMain;
            if (procedureMain != null)
            {
                Log.Warning("ProcedureMenu is invalid when open MenuForm.");
                return;
            }
        }
    }
}
