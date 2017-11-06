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
    class SpaceshipController
    {

        private Texture2D SpaceshipSkin;
        private Texture2D BulletSkin;

        private Spaceship newSpaceship;

        private List<Spaceship> SpaceshipList = new List<Spaceship>();

        private Vector2 standartStartingPosition;
        private Size standartStartingSize;


        public SpaceshipController(Texture2D spaceshipSkin,Texture2D bulletSkin)
        {

            SpaceshipSkin = spaceshipSkin;
            BulletSkin = bulletSkin;

            standartStartingSize = new Size(CommonFunctions.borders.Right * 07 / 130, CommonFunctions.borders.Bottom * 14 / 130); //  0.7 / 13 ,  1.4 / 13

            standartStartingPosition = new Vector2(CommonFunctions.borders.Right / 2 - standartStartingSize.Width / 2, CommonFunctions.borders.Bottom - standartStartingSize.Height);

        }

        public void SpawnShip()
        {

            newSpaceship = new Spaceship(SpaceshipSkin, standartStartingSize, standartStartingPosition, BulletSkin, SpaceshipList);
            SpaceshipList.Add(newSpaceship);

        }

        public void Update()
        {

            foreach (Spaceship spaceship in SpaceshipList)
                spaceship.Update();

        }

        public void Draw(SpriteBatch spriteBatch)
        {

            foreach (Spaceship spaceship in SpaceshipList)
                spaceship.Draw(spriteBatch);

        }

    }
}
