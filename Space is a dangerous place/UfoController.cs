using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_is_a_dangerous_place
{
    class UfoController
    {

        Texture2D ufoSkin;
        Texture2D ufoAmmoDropSkin;
        Texture2D ufoScoreDropSkin;
        Texture2D ufoBombDropSkin;

        Ufo newUfo;

        Random rdm;

        int randomSpawnSeconds = 15;


        public UfoController(Texture2D ufoSkin, Texture2D ufoAmmoDropSkin,Texture2D ufoScoreDropSkin,Texture2D ufoBombDropSkin)
        {

            this.ufoSkin = ufoSkin;
            this.ufoAmmoDropSkin = ufoAmmoDropSkin;
            this.ufoScoreDropSkin = ufoScoreDropSkin;
            this.ufoBombDropSkin = ufoBombDropSkin;

            rdm = new Random();

        }

        public void Update()
        {

            if (rdm.Next(1, randomSpawnSeconds * 60) == 1)
            {
                newUfo = new Ufo(new Vector2(100, 100), new Size(100, 80), ufoSkin, ufoAmmoDropSkin, ufoScoreDropSkin, ufoBombDropSkin);
            }

            foreach (Ufo ufo in CommonFunctions.ICollidableList.OfType<Ufo>().ToList())
                ufo.Update();

        }

        public void Draw(SpriteBatch spriteBatch)
        {

            foreach (Ufo ufo in CommonFunctions.ICollidableList.OfType<Ufo>().ToList())
                ufo.Draw(spriteBatch);

        }

    }
}
