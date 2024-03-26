using BigSwordRPG.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BigSwordRPG_C_
{
    public class Save
    {
        /* a mettre dans le csv pour la save
         
        //hero
        name;
        health;
        level;
        _isDead;

        //player
        position
        texture

        //inventory
        list inventory

        */
        Player player;
        Inventory inventory;



        public Save() 
        {
            player = new Player();
            inventory = new Inventory();
        }
    }
}
