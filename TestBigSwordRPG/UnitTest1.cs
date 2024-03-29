using BigSwordRPG.Game;

namespace TestBigSwordRPG.Game
{
    public class Tests
    {
        Character character = new Character();

        [Test]
        [TestCase(25, 75)]
        [TestCase(100, 0)]
        [TestCase(110, 0)]

        public void takeDammage(int a, int expected)
        {
            character.MaxHealth = 100;
            character.Health = character.MaxHealth;
            character.TakeDammage(a);

            Assert.That(character.Health, Is.EqualTo(expected));
        }


        [Test]
        [TestCase(25, 100)]
        [TestCase(10, 85)]
        [TestCase(110, 100)]
        public void heal(int a, int expected)
        {
            character.MaxHealth = 100;
            character.Health = 75;
            character.Heal(a);
            Assert.That(character.Health, Is.EqualTo(expected));
        }

        [Test]
        [TestCase(25, 75)]
        [TestCase(100, 0)]
        [TestCase(110, 0)]

        public void useMane(int a, int expected)
        {
            character.PMMax = 100;
            character.PM = character.PMMax;
            character.UseMana(a);

            Assert.That(character.PM, Is.EqualTo(expected));
        }


        [Test]
        [TestCase(25, 100)]
        [TestCase(10, 85)]
        [TestCase(110, 100)]
        public void manaheal(int a, int expected)
        {
            character.PMMax = 100;
            character.PM = 75;
            character.ManaHeal(a);
            Assert.That(character.PM, Is.EqualTo(expected));
        }






    }
}