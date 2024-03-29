using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigSwordRPG.Utils;
using BigSwordRPG.Utils.Graphics;

namespace BigSwordRPG.GameObjects
{
    public class DialogBox : GameObject
    {
        private Texture _characterTexture;
        private string _line = "";
        private int _previousLineLength;
        private const int DIALOG_BOX_HEIGHT = 20;
        private const int DIALOG_BOX_MARGIN_RIGHT = 10;

        public Texture CharacterTexture { private get => _characterTexture; set => _characterTexture = value; }
        public string Line { 
            private get => _line; 
            set {
                PreviousLineLength = _line.Length;
                _line = value; 
            } 
        }
        private int PreviousLineLength { get => _previousLineLength; set => _previousLineLength = value; }

        public DialogBox(Texture characterTexture):
            base(new int[2] {
                GameManager.Instance.Renderer.Camera.Position[0],
                GameManager.Instance.Renderer.Camera.Position[1] + GameManager.Instance.Renderer.Camera.Size[1] - DIALOG_BOX_HEIGHT
                },
                TexturesLoader.GenerateSolidRectangleTexture(0, new int[2] { Console.WindowWidth, DIALOG_BOX_HEIGHT })
            )
        {
            CharacterTexture = characterTexture;
        }

        public override void Draw()
        {
            base.Draw();
            GameManager.Instance.Renderer.DrawTexture(Position, CharacterTexture);
        }

        public void DrawDialogLine()
        {
            GameManager.Instance.Renderer.ResetCursorColors();
            int remainingCharactersCount = Line.Length;
            int charactersToDrawCount = 0;
            int writableAreaWidth = Console.WindowWidth - DIALOG_BOX_MARGIN_RIGHT - CharacterTexture.Size[0] - 5;
            // Draw dialog line and take care of Dialog Box Size / Margins
            for(int i = 0; i < DIALOG_BOX_HEIGHT - 2 || remainingCharactersCount < 0; i++)
            {
                Console.SetCursorPosition(Position[0] + 5 + CharacterTexture.Size[0], Position[1] + 2 + i);
                charactersToDrawCount = Math.Min(remainingCharactersCount, writableAreaWidth);
                Console.Write(Line.Substring(0, charactersToDrawCount));
                remainingCharactersCount -= charactersToDrawCount;
            }

            // Erase remaining characters from previous line
            if(PreviousLineLength > Line.Length)
            {
                string eraseLine = "";
                remainingCharactersCount = PreviousLineLength - Line.Length;
                for (int i = charactersToDrawCount; i < Math.Min(remainingCharactersCount, writableAreaWidth); i++)
                {
                    eraseLine += " ";
                }
                Console.Write(eraseLine);
                for (int i = Console.CursorTop; i < DIALOG_BOX_HEIGHT - 2 || remainingCharactersCount < 0; i++)
                {
                    Console.SetCursorPosition(Position[0] + 5 + CharacterTexture.Size[0], Position[1] + 2 + i);
                    charactersToDrawCount = Math.Min(Line.Length, Console.WindowWidth - DIALOG_BOX_MARGIN_RIGHT);
                    Console.Write(Line.Substring(0, charactersToDrawCount));
                    remainingCharactersCount -= charactersToDrawCount;
                }
                PreviousLineLength = 0; // If DrawDialogLine is called again, it won't erase the line again
            }
        }
    }
}