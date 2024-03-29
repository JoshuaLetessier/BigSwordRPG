using BigSwordRPG.Game;
using BigSwordRPG.Utils;
using BigSwordRPG.Utils.Graphics;
using BigSwordRPG_C_.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigSwordRPG_C_
{
    public enum Axis
    {
        HORIZONTAL = 0,
        VERTICAL,
        AXIS_COUNT
    }
    public class Player : GameObject
    {

        private Dictionary<string, Hero> _heroes;
        
        

        public bool _allHeroDead = false;
        public Dictionary<string, Hero> Heroes { get => _heroes; set => _heroes = value; }

        public Player(int[] position) : 
            base(
                position, 
                new Texture() {
                    Size = new int[2] { 2, 3 }, 
                    PixelsBuffer = new List<Pixel>() { 
                        new Pixel(160, 40), 
                        new Pixel(160, 40), 
                        new Pixel(160, 160), 
                        new Pixel(160, 160), 
                        new Pixel(160, 160), 
                        new Pixel(160, 160) 
                    }
                }
            )
        {
            CreateHero createHeroes = new CreateHero();
            _heroes = createHeroes.CreateDictionaryHero();
        }

        public void Move(int distance, Axis axis)
        {
            int[] newPosition = new int[2] { 
                Position[0] + distance * (1 - (int)axis), 
                Position[1] + distance * (int)axis 
            };
            if (GameManager.Instance.Renderer.IsInBuffer(newPosition, Texture.Size))
            {
                Position[0] = newPosition[0];
                Position[1] = newPosition[1];
                GameManager.Instance.Renderer.MoveTexture(Position, Texture, distance, axis);
            }
        }
    }
}

