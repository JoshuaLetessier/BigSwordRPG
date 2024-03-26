using BigSwordRPG.Game;
using BigSwordRPG.Utils.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;
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
        private Player player;
        private Inventory inventory;
        private Character charactere;

        private string name;
        private int health;
        private int level;
        private bool isDead;

        private int[] position;
        private Texture texture;

        private Inventory inventaire;

        public Save() 
        {
            player = new Player();
            inventaire = new Inventory();
            charactere = new Character();
        }

        public void LaunchSave()
        {
            name = charactere.Name;
            health = charactere.Health;
            level = charactere.Level;
            isDead = charactere.IsDead;

            position = player.Position;
            texture = player.Texture;

        }
    }
}
