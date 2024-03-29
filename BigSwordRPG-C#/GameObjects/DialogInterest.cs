using BigSwordRPG.Assets;
using BigSwordRPG.Utils;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigSwordRPG.GameObjects
{
    public partial class DialogInterest : Interest
    {
        private string _dialogFilename;

        private string DialogFilename { get => _dialogFilename; set => _dialogFilename = value; }

        public DialogInterest(int[] position, string dialogFilename) : base(position)
        {
            DialogFilename = dialogFilename;
        }
        
        public override void Interact()
        {
            DialogScene dialogScene = new DialogScene();
            dialogScene.LoadDialog(DialogFilename);
            GameManager.Instance.SwitchScene(dialogScene);
        }
    }
}
