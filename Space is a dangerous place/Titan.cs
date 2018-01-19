using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_is_a_dangerous_place
{
    class Titan : Spaceship
    {
        public Titan(Texture2D skin, Size size, Vector2 position, Texture2D bulletSkin, List<Spaceship> spaceshipList, MenuController meContr, float ammMu, float scoMu, float speMu, float AtsMu) : base(skin, size, position, bulletSkin, spaceshipList, meContr, ammMu, scoMu, speMu, AtsMu)
        {
        }

        public override void Shoot()
        {

            if (DateTime.Now > nextAttackTime && ammunition > 0)
            {
                ammunition--;

                nextAttackTime = DateTime.Now.AddMilliseconds(attackSpeed);


                float yValue = position.Y + ObjectSize.Height / 2 - standartBulletSize.Height / 2;

                Vector2 leftBulletPosition = new Vector2(position.X, yValue);
                Vector2 midleBulletPosition = new Vector2(position.X + ObjectSize.Width / 2 - standartBulletSize.Width / 2, yValue);
                Vector2 rightBulletPosition = new Vector2(position.X + ObjectSize.Width - standartBulletSize.Width, yValue);

                newBullet = new Bullet(BulletSkin, leftBulletPosition, standartBulletSize, 1, this);
                BulletList.Add(newBullet);
                newBullet = new Bullet(BulletSkin, midleBulletPosition, standartBulletSize, 0, this);
                BulletList.Add(newBullet);
                newBullet = new Bullet(BulletSkin, rightBulletPosition, standartBulletSize, 2, this);
                BulletList.Add(newBullet);

            }

        }

        public override void InputChecking()
        {
            
            if (!paused)
            {
                if (CommonFunctions.Input.IsKeyDown(Keys.A) || CommonFunctions.Input.IsKeyDown(Keys.Left))
                    MoveLeft();
                
                if (CommonFunctions.Input.IsKeyDown(Keys.D) || CommonFunctions.Input.IsKeyDown(Keys.Right))
                    MoveRight();

                if (CommonFunctions.Input.IsKeyDown(Keys.S) || CommonFunctions.Input.IsKeyDown(Keys.Down))
                    Shoot();

                if (CommonFunctions.Input.IsKeyDown(Keys.Space) && !CommonFunctions.terrainSpawning)
                    CommonFunctions.currentTerrainController.StartRoutine();

                if (CommonFunctions.Input.IsKeyDown(Keys.Escape))
                {
                    paused = true;
                    meContr.pauseMenuNavigator = 0;
                    generalSpeedSaver = CommonFunctions.generalGameSpeed;
                    CommonFunctions.generalGameSpeed = 0;
                    CommonFunctions.generalColour = Microsoft.Xna.Framework.Color.Gray;
                }
            }

            if (paused)
            {
                menuControlls();
            }
        }
        
    }
}
