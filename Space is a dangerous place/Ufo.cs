﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_is_a_dangerous_place
{
    class Ufo
    {

        private Vector2 position;
        public Vector2 PositionForRectangle { get; set; }
        public Size ObjectSize { get; set; }

        public int Speed { get; set; } = 3;
        private Vector2 direction;

        private Microsoft.Xna.Framework.Rectangle destinationRectangle;

        private Texture2D skin;
        private Texture2D ufoAmmoDropSkin;
        private Texture2D ufoScoreDropSkin;
        private Texture2D ufoBombDropSkin;

        private UfoAmmoDrop newUfoAmmoDrop;
        private UfoScoreDrop newUfoScoreDrop;
        private UfoBombDrop newUfoBombDrop;

        //todo: Lootbomben in die ufo collisionliste, ammodrops in ammoliste...


        public Ufo(Vector2 position,Size size, Texture2D skin, Texture2D ufoAmmoDropSkin, Texture2D ufoScoreDropSkin, Texture2D ufoBombDropSkin)
        {

            this.position = position;
            ObjectSize = size;
            this.skin = skin;
            this.ufoAmmoDropSkin = ufoAmmoDropSkin;
            this.ufoScoreDropSkin = ufoScoreDropSkin;
            this.ufoBombDropSkin = ufoBombDropSkin;

        }

        public void Update()
        {

            destinationRectangle = new Microsoft.Xna.Framework.Rectangle(Convert.ToInt32(position.X), Convert.ToInt32(position.Y), ObjectSize.Width, ObjectSize.Height);

            direction = Vector2.Zero;

            //todo: direction funktionen hier

            direction *= Speed;
            position += direction;
            PositionForRectangle = position;

        }

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(skin, destinationRectangle, Microsoft.Xna.Framework.Color.White);

        }

    }
}
