using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BigSwordRPG.Utils;
using BigSwordRPG.GameObjects;
using BigSwordRPG.Utils.Graphics;
using BigSwordRPG_C_;

namespace BigSwordRPG.Assets
{
    public class DialogScene : Scene
    {
        private string[] _dialogLines;
        private int _currentLine;
        private DialogBox _dialogBox;

        private string[] DialogLines { get => _dialogLines; set => _dialogLines = value; }
        private int CurrentLine { get => _currentLine; set => _currentLine = value; }
        public DialogBox DialogBox { get => _dialogBox; set => _dialogBox = value; }

        public DialogScene():base() {
            DialogBox = new DialogBox(TexturesLoader.GetTexture("old_lady"));
            GameObjects.Add(DialogBox);
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
            /*
            if (CurrentLine >= DialogLines.Length)
            {
                Exit();
                return;
            }
            */
            CurrentLine++;
            //DialogBox.Line = DialogLines[CurrentLine];
            DialogBox.Line = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX" +
                "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
            DialogBox.DrawDialogLine();
            DialogBox.Line = "By";
            DialogBox.DrawDialogLine();
        }

        public override void Exit()
        {
            //Redraw background
            GameManager.Instance.Renderer.HideGameObject(GameObjects[0]);
            GameManager.Instance.SwitchScene(PreviousScene);
        }
    }
}