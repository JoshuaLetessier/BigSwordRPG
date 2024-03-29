namespace BigSwordRPG_C_.Utils
{
    public class Camera
    {
        public Player joueur;

        public Camera()
        {
            joueur = new Player(new int[2] { 150, 60 });
        }

        public void setPositionCamera()
        {
            int[] posJoueur = joueur.Position;
            Console.SetWindowPosition(posJoueur[0] - 70, posJoueur[1] - 25);
        }
    }
}
