using BigSwordRPG.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigSwordRPG_C_
{
    public enum Axis
    {
        HORIZONTAL,
        VERTICAL
    }

    public class Player
    {
        public int Initialize()
        {
            GameManager.Instance.InputManager.RegisterAction(
                ConsoleKey.Z, 
                new Action(
                    () => Move(10, Axis.HORIZONTAL)
                )
            );
            return 0;
        }

        public void Move(int distance, Axis axis)
        {

        }


    }

}