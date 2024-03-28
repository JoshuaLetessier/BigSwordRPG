using BigSwordRPG.Game;
using BigSwordRPG.Utils;
using BigSwordRPG.Utils.Graphics;
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
        private CreateHero _DicoHeros;

        public bool _allHeroDead = false;
        public Dictionary<string, Hero> Heroes { get => _heroes; set => _heroes = value; }

        public Player(int[] position, Texture texture) : 
            base(
                position, 
                texture
            )
        {
            _DicoHeros = new CreateHero();
            _heroes = _DicoHeros.CreateDictionaryHero();


            //Draw();
            GameManager.Instance.InputManager.RegisterAction(
                ConsoleKey.D,
                new Action(
                    () => Move(1, Axis.HORIZONTAL)
                )
            );
            GameManager.Instance.InputManager.RegisterAction(
                 ConsoleKey.Q,
                 new Action(
                     () => Move(-1, Axis.HORIZONTAL)
                 )
             );
            GameManager.Instance.InputManager.RegisterAction(
                 ConsoleKey.Z,
                 new Action(
                     () => Move(-1, Axis.VERTICAL)
                 )
             );
            GameManager.Instance.InputManager.RegisterAction(
                 ConsoleKey.S,
                 new Action(
                     () => Move(1, Axis.VERTICAL)
                 )
             );
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

