using BigSwordRPG.Assets;
using BigSwordRPG.Core;

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
