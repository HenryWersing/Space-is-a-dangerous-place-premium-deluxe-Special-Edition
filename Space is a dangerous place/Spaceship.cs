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
    class Spaceship : ICollidable //erinnerung: dieses spaceship ist die spitze der ship erbstrucktur, also wenn andere arten der schiffe, erben diese von hier
    {

        private KeyboardState Input;

        private Vector2 position;
        public Vector2 PositionForRectangle { get; set; }
        public Size ObjectSize { get; set; }

        public float Speed { get; private set; }
        private Vector2 direction;

        public Texture2D Skin { get; private set; }
        public Texture2D BulletSkin { get; private set; }

        public float startingAmmunition = 3;
        public float startingScore = 0;

        public float ammunition;
        public float score;

        private bool paused = false;
        private int pauseMenuNavigator;
        private int pauseMenuNumberOfOptions = 2;
        private DateTime nextButtonTime;
        private float generalSpeedSaver;

        private Size standartBulletSize;

        public List<Bullet> BulletList = new List<Bullet>();
        private Bullet newBullet;

        private float attackSpeed;
        private DateTime nextAttackTime;

        public float ammunitionMultiplier;
        public float scoreMultiplier;
        public float speedMultiplier;
        public float attackSpeedMultiplier;

        private Microsoft.Xna.Framework.Rectangle destinationRectangle;
        private Microsoft.Xna.Framework.Rectangle destinationRectangleButton0;
        private Microsoft.Xna.Framework.Rectangle destinationRectangleButton1;

        private List<Spaceship> spaceshipList;


        public Spaceship(Texture2D skin, Size size, Vector2 position, Texture2D bulletSkin, List<Spaceship> spaceshipList, float ammMu, float scoMu, float speMu, float AtsMu)
        {

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


            this.spaceshipList = spaceshipList;

            CommonFunctions.currentSpaceship = this;

        }

        private void MoveLeft()
        {

            if (position.X > CommonFunctions.borders.Left + Speed)
                direction.X -= 1f * CommonFunctions.aspectRatioMultiplierX;

        }

        private void MoveRight()
        {

            if (position.X < CommonFunctions.borders.Right - Speed - ObjectSize.Width)
                direction.X += 1f * CommonFunctions.aspectRatioMultiplierX;

        }

        private void MoveLeftSlow()
        {

            if (position.X > CommonFunctions.borders.Left + Speed)
                direction.X -= 0.4f * CommonFunctions.aspectRatioMultiplierX;

        }

        private void MoveRightSlow()
        {

            if (position.X < CommonFunctions.borders.Right - Speed - ObjectSize.Width)
                direction.X += 0.4f * CommonFunctions.aspectRatioMultiplierX;

        }

        public void Shoot()
        {

            if (DateTime.Now > nextAttackTime && ammunition > 0)
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

            //spaceshipList.Remove(this);
            CommonFunctions.gameRunning = false;
            CommonFunctions.ICollidableList.RemoveRange(0, CommonFunctions.ICollidableList.Count);
            BulletList.RemoveRange(0, BulletList.Count);
            ammunition = startingAmmunition;
            if (score > Properties.Settings.Default.Highscore)
            {
                Properties.Settings.Default.Highscore = Convert.ToInt32(score);
                Properties.Settings.Default.Save();
            }
            score = startingScore;

        }

        public void Update()
        {

            #region destinationRectangles
            destinationRectangle = new Microsoft.Xna.Framework.Rectangle(Convert.ToInt32(position.X), Convert.ToInt32(position.Y), ObjectSize.Width, ObjectSize.Height);

            float buttonWidth = CommonFunctions.ActiveButtonContinue.Bounds.Right * 0.65f * CommonFunctions.aspectRatioMultiplierX;
            float buttonHeight= CommonFunctions.ActiveButtonContinue.Bounds.Bottom * 0.65f * CommonFunctions.aspectRatioMultiplierY;
            float distanceBetweenButtons = buttonHeight * 0.8f;
            float distanceFromButtonsToTop = (CommonFunctions.borders.Bottom - pauseMenuNumberOfOptions * buttonHeight - (pauseMenuNumberOfOptions - 1) * distanceBetweenButtons) / 2; //(gesamthöhe - die höhen der buttons - die abstande zwischen buttons) / 2  =  abstand zu oben und unten
            float distanceFromButtonsToLeft = (CommonFunctions.borders.Right - buttonWidth) / 2;
            destinationRectangleButton0 = new Microsoft.Xna.Framework.Rectangle(Convert.ToInt32(distanceFromButtonsToLeft), Convert.ToInt32(distanceFromButtonsToTop), Convert.ToInt32(buttonWidth), Convert.ToInt32(buttonHeight));
            destinationRectangleButton1 = new Microsoft.Xna.Framework.Rectangle(Convert.ToInt32(distanceFromButtonsToLeft), Convert.ToInt32(distanceFromButtonsToTop + buttonHeight + distanceBetweenButtons), Convert.ToInt32(buttonWidth), Convert.ToInt32(buttonHeight));

            #endregion

            direction = Vector2.Zero;

            Input = Keyboard.GetState();

            if (!paused)
            {
                if ((Input.IsKeyDown(Keys.A) || Input.IsKeyDown(Keys.Left)) && Input.IsKeyDown(Keys.LeftShift))
                    MoveLeft();

                else if (Input.IsKeyDown(Keys.A) || Input.IsKeyDown(Keys.Left))
                    MoveLeftSlow();

                if ((Input.IsKeyDown(Keys.D) || Input.IsKeyDown(Keys.Right)) && Input.IsKeyDown(Keys.LeftShift))
                    MoveRight();

                else if (Input.IsKeyDown(Keys.D) || Input.IsKeyDown(Keys.Right))
                    MoveRightSlow();

                if (Input.IsKeyDown(Keys.S) || Input.IsKeyDown(Keys.Down))
                    Shoot();

                if (Input.IsKeyDown(Keys.Enter) && !CommonFunctions.gameRunning)
                    CommonFunctions.currentTerrainController.StartRoutine();

                if (Input.IsKeyDown(Keys.Escape))
                {
                    paused = true;
                    pauseMenuNavigator = 0;
                    generalSpeedSaver = CommonFunctions.generalGameSpeed;
                    CommonFunctions.generalGameSpeed = 0;
                    CommonFunctions.generalColour = Microsoft.Xna.Framework.Color.Gray;
                }
            }

            if (paused)
            {
                if (DateTime.Now > nextButtonTime)
                {
                    nextButtonTime = DateTime.Now.AddMilliseconds(100); //todo: smoother machen?

                    if (Input.IsKeyDown(Keys.S) || Input.IsKeyDown(Keys.Down))
                        pauseMenuNavigator++;
                    if (Input.IsKeyDown(Keys.W) || Input.IsKeyDown(Keys.Up))
                        pauseMenuNavigator--;

                    if (pauseMenuNavigator > pauseMenuNumberOfOptions - 1)
                        pauseMenuNavigator = 0;
                    if (pauseMenuNavigator < 0)
                        pauseMenuNavigator = pauseMenuNumberOfOptions - 1;

                    if (Input.IsKeyDown(Keys.Enter) && pauseMenuNavigator == 0)
                    {
                        paused = false;
                        CommonFunctions.generalGameSpeed = generalSpeedSaver;
                        CommonFunctions.generalColour = Microsoft.Xna.Framework.Color.White;
                    }

                    if (Input.IsKeyDown(Keys.Enter) && pauseMenuNavigator == 1)
                    {
                        Destroy(this);
                        spaceshipList.Remove(this);
                        CommonFunctions.currentGameStartController.gameStarted = false;
                        CommonFunctions.generalColour = Microsoft.Xna.Framework.Color.White;
                    }
                }
            }
            

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

            spriteBatch.Draw(Skin, destinationRectangle, CommonFunctions.generalColour);

            foreach (Bullet bullet in BulletList)
                bullet.Draw(spriteBatch);

            if (paused)
            {
                if (pauseMenuNavigator == 0)
                {
                    spriteBatch.Draw(CommonFunctions.ActiveButtonContinue, destinationRectangleButton0, Microsoft.Xna.Framework.Color.White);
                    spriteBatch.Draw(CommonFunctions.PassiveButtonBackToMenu, destinationRectangleButton1, Microsoft.Xna.Framework.Color.White);
                }
                else if (pauseMenuNavigator == 1)
                {
                    spriteBatch.Draw(CommonFunctions.PassiveButtonContinue, destinationRectangleButton0, Microsoft.Xna.Framework.Color.White);
                    spriteBatch.Draw(CommonFunctions.ActiveButonBackToMenu, destinationRectangleButton1, Microsoft.Xna.Framework.Color.White);
                }
            }
        }

    }
}
