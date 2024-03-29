using BigSwordRPG.Utils;
using BigSwordRPG.Utils.Graphics;
using BigSwordRPG.Core;

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

        public void DrawDialogLine(string dialogLine)
        {
            Line = dialogLine;
            GameManager.Instance.Renderer.ResetCursorColors();
            int remainingCharactersCount = Line.Length;
            int charactersToDrawCount = 0;
            int writableAreaWidth = Console.WindowWidth - DIALOG_BOX_MARGIN_RIGHT - CharacterTexture.Size[0] - 5;
            int alreadyWrittenLinesCount = 0;
            // Draw dialog line and take care of Dialog Box Size / Margins
            for (int i = 0; i < DIALOG_BOX_HEIGHT - 2 && remainingCharactersCount > 0; i++)
            {
                Console.SetCursorPosition(Position[0] + 5 + CharacterTexture.Size[0], Position[1] + 2 + i);
                charactersToDrawCount = Math.Min(remainingCharactersCount, writableAreaWidth);
                Console.Write(Line.Substring(alreadyWrittenLinesCount * writableAreaWidth, charactersToDrawCount));
                remainingCharactersCount -= charactersToDrawCount;
                alreadyWrittenLinesCount += 1;
            }

            // Erase remaining characters from previous line
            if(PreviousLineLength > Line.Length)
            {
                string eraseLine = "";
                for (int i = charactersToDrawCount; i < Math.Min(PreviousLineLength, writableAreaWidth); i++)
                {
                    eraseLine += " ";
                }
                Console.Write(eraseLine);
                remainingCharactersCount = PreviousLineLength - alreadyWrittenLinesCount * writableAreaWidth;
                for (int i = alreadyWrittenLinesCount; i < DIALOG_BOX_HEIGHT - 2 && remainingCharactersCount > 0; i++)
                {
                    Console.SetCursorPosition(Position[0] + 5 + CharacterTexture.Size[0], Position[1] + 2 + i);
                    charactersToDrawCount = Math.Min(remainingCharactersCount, writableAreaWidth);

                    for (int j = 0; j < charactersToDrawCount; j++)
                    {
                        eraseLine += " ";
                    }
                    Console.Write(eraseLine);
                    remainingCharactersCount -= charactersToDrawCount;
                }
                PreviousLineLength = 0; // If DrawDialogLine is called again, it won't erase the line again
            }
        }

        public override void Updtate()
        {
            throw new NotImplementedException();
        }
    }
}