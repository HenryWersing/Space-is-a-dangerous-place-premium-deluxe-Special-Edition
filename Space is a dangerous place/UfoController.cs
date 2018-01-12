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
        
        int randomSpawnSeconds = 15;


        public UfoController(Texture2D ufoSkin, Texture2D ufoAmmoDropSkin,Texture2D ufoScoreDropSkin,Texture2D ufoBombDropSkin)
        {

            this.ufoSkin = ufoSkin;
            this.ufoAmmoDropSkin = ufoAmmoDropSkin;
            this.ufoScoreDropSkin = ufoScoreDropSkin;
            this.ufoBombDropSkin = ufoBombDropSkin;
            
        }

        public void Update()
        {
            if (CommonFunctions.terrainSpawning)
            {
                if (CommonFunctions.generalRandom.Next(1, randomSpawnSeconds * 60) == 1)
                {
                    newUfo = new Ufo(new Vector2(-100 * CommonFunctions.aspectRatioMultiplierX, -100 * CommonFunctions.aspectRatioMultiplierY), new Size(Convert.ToInt32(60 * CommonFunctions.aspectRatioMultiplierX), Convert.ToInt32(48 * CommonFunctions.aspectRatioMultiplierY)), ufoSkin, ufoAmmoDropSkin, ufoScoreDropSkin, ufoBombDropSkin);

                    CommonFunctions.ICollidableList.Add(newUfo);
                }
            }

            foreach (Ufo ufo in CommonFunctions.ICollidableList.OfType<Ufo>().ToList())
                ufo.Update();

            foreach (UfoDrop ufoDrop in CommonFunctions.ICollidableList.OfType<UfoDrop>().ToList())
                ufoDrop.Update();

        }

        public void Draw(SpriteBatch spriteBatch)
        {

            foreach (Ufo ufo in CommonFunctions.ICollidableList.OfType<Ufo>().ToList())
                ufo.Draw(spriteBatch);

            foreach (UfoDrop ufoDrop in CommonFunctions.ICollidableList.OfType<UfoDrop>().ToList())
                ufoDrop.Draw(spriteBatch);

        }

    }
}
