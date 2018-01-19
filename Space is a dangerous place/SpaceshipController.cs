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
        private Texture2D TitanSkin;
        private Texture2D BulletSkin;

        private Spaceship newSpaceship;

        private List<Spaceship> SpaceshipList = new List<Spaceship>();

        private MenuController meContr;

        private Size standartStartingSize;
        private Size titanStartingSize;
        private Vector2 standartStartingPosition;
        private Vector2 titanStartingPosition;


        public SpaceshipController(Texture2D spaceshipSkin, Texture2D titanSkin, Texture2D bulletSkin, MenuController meContr)
        {

            SpaceshipSkin = spaceshipSkin;
            TitanSkin = titanSkin;
            BulletSkin = bulletSkin;
            this.meContr = meContr;

            standartStartingSize = new Size(Convert.ToInt32(35 * CommonFunctions.aspectRatioMultiplierX), Convert.ToInt32(70 * CommonFunctions.aspectRatioMultiplierY));
            titanStartingSize = new Size(Convert.ToInt32(60 * CommonFunctions.aspectRatioMultiplierX), Convert.ToInt32(70 * CommonFunctions.aspectRatioMultiplierY));

            standartStartingPosition = new Vector2(CommonFunctions.borders.Right / 2 - standartStartingSize.Width / 2, CommonFunctions.borders.Bottom - standartStartingSize.Height);
            titanStartingPosition = new Vector2(CommonFunctions.borders.Right / 2 - titanStartingSize.Width / 2, CommonFunctions.borders.Bottom - titanStartingSize.Height);

        }

        public void SpawnShip(int spawnMode)
        {

            switch(spawnMode)
            {
                case 0:
                    newSpaceship = new Spaceship(SpaceshipSkin, standartStartingSize, standartStartingPosition, BulletSkin, SpaceshipList, meContr, 1, 2, 1, 1);
                    CommonFunctions.generalGameSpeed = 1;
                    SpaceshipList.Add(newSpaceship);
                    break;
                case 1:
                    newSpaceship = new Spaceship(SpaceshipSkin, standartStartingSize, standartStartingPosition, BulletSkin, SpaceshipList, meContr, 1, 4, 1, 0.66f);
                    CommonFunctions.generalGameSpeed = 1.5f;
                    SpaceshipList.Add(newSpaceship);
                    break;
                case 2:
                    newSpaceship = new Titan(TitanSkin, titanStartingSize, titanStartingPosition, BulletSkin, SpaceshipList, meContr, 1.1f, 1, 0.4f, 2);
                    CommonFunctions.generalGameSpeed = 1;
                    SpaceshipList.Add(newSpaceship);
                    break;
                case 3:
                    newSpaceship = new Titan(TitanSkin, titanStartingSize, titanStartingPosition, BulletSkin, SpaceshipList, meContr, 1.1f, 2, 0.4f, 1.33f);
                    CommonFunctions.generalGameSpeed = 1.5f;
                    SpaceshipList.Add(newSpaceship);
                    break;
                default:
                    break;
            }

        }

        public void Update()
        {

            for (int i = 0; i < SpaceshipList.Count; i++)
            {
                SpaceshipList[i].Update();
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {

            foreach (Spaceship spaceship in SpaceshipList)
                spaceship.Draw(spriteBatch);

        }

    }
}
