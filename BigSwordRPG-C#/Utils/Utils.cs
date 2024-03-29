namespace BigSwordRPG_C_.Utils
{
    internal class Utils
    {
        Utils() { }
        public int RandomFonction(int value, int value2)
        {
            Random random = new Random();

            return random.Next(value, value2);
        }
    }
}
