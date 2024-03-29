namespace BigSwordRPG_C_.Game
{
    public class Music
    {
        private string test;
        private int[] frequencies;
        private int[] durations;

        public Music()
        {

        }

        public void PlayBeepSelectMenu()
        {
            Console.Beep(392, 100);
        }

        public void PlayEnterMenu()
        {
            Console.Beep(262, 100);
        }
    }
}
