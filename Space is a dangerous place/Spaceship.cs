using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
    class Spaceship : ICollidable
    {
        
        public Vector2 position;
        public Vector2 PositionForRectangle { get; set; }
        public Size ObjectSize { get; set; }

        public float Speed { get; set; }
        public Vector2 direction;

        public Texture2D Skin { get; private set; }
        public Texture2D BulletSkin { get; private set; }

        public float startingAmmunition = 3;
        public float startingScore = 0;

        public float ammunition;
        public float score;
        public float previousScore;

        public bool diedBefore = false;

        public bool paused = false;
        public static int pauseMenuNumberOfOptions = 2;

        public float generalSpeedSaver;

        public Size standartBulletSize;

        public List<Bullet> BulletList = new List<Bullet>();
        public Bullet newBullet;

        public float attackSpeed;
        public DateTime nextAttackTime;

        public float ammunitionMultiplier;
        public float scoreMultiplier;
        public float speedMultiplier;
        public float attackSpeedMultiplier;

        public Microsoft.Xna.Framework.Rectangle destinationRectangle;

        public SpriteFont font;

        public List<Spaceship> spaceshipList;

        public MenuController meContr;


        public Spaceship(Texture2D skin, Size size, Vector2 position, Texture2D bulletSkin, List<Spaceship> spaceshipList, MenuController meContr, float ammMu, float scoMu, float speMu, float AtsMu)
        {

            this.meContr = meContr;
            ammunitionMultiplier = ammMu;
            scoreMultiplier = scoMu;
            speedMultiplier = speMu;
            attackSpeedMultiplier = AtsMu;

            Skin = skin;
            ObjectSize = size;
            this.position = position;
            BulletSkin = bulletSkin;

            PositionForRectangle = position;

            Speed = 5 * speedMultiplier;
            attackSpeed = 500 * attackSpeedMultiplier; //ms

            ammunition = startingAmmunition;
            score = startingScore;

            standartBulletSize = new Size(Convert.ToInt32(3 * CommonFunctions.aspectRatioMultiplierX), Convert.ToInt32(3 * CommonFunctions.aspectRatioMultiplierY));

            this.font = CommonFunctions.font;

            this.spaceshipList = spaceshipList;

            CommonFunctions.currentSpaceship = this;

        }

        #region Moving
        public void MoveLeftFast()
        {

            if (position.X > CommonFunctions.borders.Left + Speed)
                direction.X -= 1f * CommonFunctions.aspectRatioMultiplierX;

        }

        public void MoveRightFast()
        {

            if (position.X < CommonFunctions.borders.Right - Speed - ObjectSize.Width)
                direction.X += 1f * CommonFunctions.aspectRatioMultiplierX;

        }

        public void MoveLeft()
        {

            if (position.X > CommonFunctions.borders.Left + Speed)
                direction.X -= 0.4f * CommonFunctions.aspectRatioMultiplierX;

        }

        public void MoveRight()
        {

            if (position.X < CommonFunctions.borders.Right - Speed - ObjectSize.Width)
                direction.X += 0.4f * CommonFunctions.aspectRatioMultiplierX;

        }
        #endregion

        #region shooting etc
        public virtual void Shoot()
        {

            if (DateTime.Now > nextAttackTime && ammunition >= 1)
            {
                ammunition--;

                nextAttackTime = DateTime.Now.AddMilliseconds(attackSpeed);

                Vector2 bulletPosition = new Vector2(position.X + ObjectSize.Width / 2 - standartBulletSize.Width / 2, position.Y - standartBulletSize.Height - 1);

                newBullet = new Bullet(BulletSkin, bulletPosition, standartBulletSize, 0, this);
                BulletList.Add(newBullet);
            }

        }

        public void ShootTripleShot(AmmoDrop initialDrop)
        {

            float yValue = initialDrop.position.Y + initialDrop.ObjectSize.Height / 2 - standartBulletSize.Height / 2;

            Vector2 leftBulletPosition = new Vector2(initialDrop.position.X, yValue);
            Vector2 midleBulletPosition = new Vector2(initialDrop.position.X + initialDrop.ObjectSize.Width / 2 - standartBulletSize.Width / 2, yValue);
            Vector2 rightBulletPosition = new Vector2(initialDrop.position.X + initialDrop.ObjectSize.Width - standartBulletSize.Width, yValue);

            newBullet = new Bullet(BulletSkin, leftBulletPosition, standartBulletSize, 1, this);
            BulletList.Add(newBullet);
            newBullet = new Bullet(BulletSkin, midleBulletPosition, standartBulletSize, 0, this);
            BulletList.Add(newBullet);
            newBullet = new Bullet(BulletSkin, rightBulletPosition, standartBulletSize, 2, this);
            BulletList.Add(newBullet);

        }

        public void ScreenWipe()
        {

            foreach (ICollidable collidable in CommonFunctions.ICollidableList)
            {
                if (collidable is Terrain)
                    collidable.Destroy(collidable);
            }

        }
        #endregion

        public void CollisionsNConsequences()
        {

            ICollidable collision = CommonFunctions.CheckCollision(this, CommonFunctions.ICollidableList);

            if (collision is Terrain || collision is Ufo || collision is UfoBombDrop)
                Destroy(collision);

            else if (collision is AmmoDrop || collision is ScoreDrop || collision is UfoAmmoDrop || collision is UfoScoreDrop)
                collision.Destroy(this);

        }

        public void Destroy(ICollidable collidingObject)
        {
            
            CommonFunctions.terrainSpawning = false;
            CommonFunctions.ICollidableList.RemoveRange(0, CommonFunctions.ICollidableList.Count);
            BulletList.RemoveRange(0, BulletList.Count);
            ammunition = startingAmmunition;
            previousScore = score;
            if (score > Properties.Settings.Default.Highscore)
            {
                Properties.Settings.Default.Highscore = Convert.ToInt32(score);
                Properties.Settings.Default.Save();
            }
            score = startingScore;
            diedBefore = true;

        }
        //TODO: !!!Balancing!!!
        public virtual void InputChecking()
        {
            
            if (!paused)
            {
                if ((CommonFunctions.Input.IsKeyDown(Keys.A) || CommonFunctions.Input.IsKeyDown(Keys.Left)) && CommonFunctions.Input.IsKeyDown(Keys.LeftShift))
                    MoveLeftFast();

                else if (CommonFunctions.Input.IsKeyDown(Keys.A) || CommonFunctions.Input.IsKeyDown(Keys.Left))
                    MoveLeft();

                if ((CommonFunctions.Input.IsKeyDown(Keys.D) || CommonFunctions.Input.IsKeyDown(Keys.Right)) && CommonFunctions.Input.IsKeyDown(Keys.LeftShift))
                    MoveRightFast();

                else if (CommonFunctions.Input.IsKeyDown(Keys.D) || CommonFunctions.Input.IsKeyDown(Keys.Right))
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

        public void menuControlls()
        {

            #region Texture Lists
            List<Texture2D> activeButtonsList = new List<Texture2D>();
            List<Texture2D> passiveButtonsList = new List<Texture2D>();
            activeButtonsList.Add(CommonFunctions.ActiveButtonContinue);
            activeButtonsList.Add(CommonFunctions.ActiveButonBackToMenu);
            passiveButtonsList.Add(CommonFunctions.PassiveButtonContinue);
            passiveButtonsList.Add(CommonFunctions.PassiveButtonBackToMenu);
            #endregion
            
            switch (meContr.MenuControll(2, activeButtonsList, passiveButtonsList))
            {
                case 0:
                    paused = false;
                    CommonFunctions.generalGameSpeed = generalSpeedSaver;
                    CommonFunctions.generalColour = Microsoft.Xna.Framework.Color.White;
                    break;
                case 1:
                    Destroy(this);
                    spaceshipList.Remove(this);
                    CommonFunctions.currentGameStartController.gameStarted = false;
                    CommonFunctions.currentGameStartController.menuPage = 0;
                    meContr.pauseMenuNavigator = 0;
                    CommonFunctions.generalColour = Microsoft.Xna.Framework.Color.White;
                    break;
                default:
                    break;
            }

        }

        public void Update()
        {
            
            destinationRectangle = new Microsoft.Xna.Framework.Rectangle(Convert.ToInt32(position.X), Convert.ToInt32(position.Y), ObjectSize.Width, ObjectSize.Height);
            
            direction = Vector2.Zero;

            InputChecking();

            direction *= Speed* CommonFunctions.generalGameSpeed;
            position += direction;
            PositionForRectangle = position;

            CollisionsNConsequences();

            for (int i = 0; i < BulletList.Count; i++)
            {
                BulletList[i].Update();
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {

            if (!CommonFunctions.terrainSpawning)
            {
                if(!diedBefore)
                    spriteBatch.DrawString(font, "Raise your score as high as possible!", new Vector2(3, 3), Microsoft.Xna.Framework.Color.White, 0, new Vector2(0, 0), 0.7f * CommonFunctions.aspectRatioMultiplierY, 0, 0);
                else
                {
                    if (previousScore>Properties.Settings.Default.Highscore)
                        spriteBatch.DrawString(font, "You died! Reached score: " + previousScore + "\nNew Highscore!", new Vector2(3, 3), Microsoft.Xna.Framework.Color.White, 0, new Vector2(0, 0), 0.7f * CommonFunctions.aspectRatioMultiplierY, 0, 0);
                    else
                        spriteBatch.DrawString(font, "You died! Reached score: " + previousScore, new Vector2(3, 3), Microsoft.Xna.Framework.Color.White, 0, new Vector2(0, 0), 0.7f * CommonFunctions.aspectRatioMultiplierY, 0, 0);
                }
            }

            spriteBatch.Draw(Skin, destinationRectangle, CommonFunctions.generalColour);

            foreach (Bullet bullet in BulletList)
                bullet.Draw(spriteBatch);

            if (paused)
            {
                meContr.DrawMenu(spriteBatch);
            }
        }

    }
}
