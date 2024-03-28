using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BigSwordRPG.GameObjects;
using BigSwordRPG.Utils;
using BigSwordRPG.Utils.Graphics;
using BigSwordRPG_C_;

namespace BigSwordRPG.Assets
{
    public class DialogScene : Scene
    {
        public DialogScene():base() {
            GameObjects.Add(new DialogBox());
            RegisterAction(ConsoleKey.Enter, NextDialogLine);
            RegisterAction(ConsoleKey.Escape, Exit);
        }
        public override void Draw()
        {
            base.Draw();
        }

        public override void Run()
        {
            Draw();
        }

        public void NextDialogLine()
        {
            throw new NotImplementedException();
        }

        public override void Exit()
        {
            //Redraw background
            GameManager.Instance.Renderer.HideGameObject(GameObjects[0]);
            GameManager.Instance.SwitchScene(PreviousScene);
        }
    }
}