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
    public partial class FightInterest : Interest
    {
        private string _fightFilename;

        private string FightFilename { get => _fightFilename; set => _fightFilename = value; }

        public FightInterest(int[] position, string fightFilename) : base(position)
        {
            FightFilename = fightFilename;
        }
        
        public override void Interact()
        {
            FightScene fightScene = new FightScene();
            // Load X set of enemies
            GameManager.Instance.SwitchScene(fightScene);
        }
    }
}
