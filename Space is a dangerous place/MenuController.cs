using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_is_a_dangerous_place
{
    class MenuController
    {
        
        KeyboardState Input;
        MouseState mState;

        int numberOfButtons;
        List<Texture2D> activeButtonTextureList = new List<Texture2D>();
        List<Texture2D> passiveButtonTextureList = new List<Texture2D>();

        Keys lastClickedKey;
        int counter = 0;
        DateTime nextButtonTime;
        DateTime nextClickTime;
        public int pauseMenuNavigator;

        float buttonWidth;
        float buttonHeight;
        float distanceBetweenButtons;
        float distanceFromButtonsToTop;
        float distanceFromButtonsToLeft;

        public List<Rectangle> buttonDestinationRectangles = new List<Rectangle>();

        Texture2D pointerTexture;


        public MenuController(Texture2D pointerTexture)
        {
            this.pointerTexture = pointerTexture;

            buttonWidth = CommonFunctions.ActiveButtonContinue.Bounds.Right * 0.65f * CommonFunctions.aspectRatioMultiplierX;
            buttonHeight = CommonFunctions.ActiveButtonContinue.Bounds.Bottom * 0.65f * CommonFunctions.aspectRatioMultiplierY;
            distanceBetweenButtons = buttonHeight * 0.8f;
            distanceFromButtonsToLeft = (CommonFunctions.borders.Right - buttonWidth) / 2;
        }

        public int MenuControll(int numberOfButtons, List<Texture2D> activeButtonTextureList, List<Texture2D> passiveButtonTextureList)
        {

            this.numberOfButtons = numberOfButtons;
            this.activeButtonTextureList = activeButtonTextureList;
            this.passiveButtonTextureList = passiveButtonTextureList;
            //todo: ein globales input in commons, dann in der game update funktion input=keyboard.getstate
            Input = Keyboard.GetState();

            buttonDestinationRectangles.Clear();
            distanceFromButtonsToTop = (CommonFunctions.borders.Bottom - numberOfButtons * buttonHeight - (numberOfButtons - 1) * distanceBetweenButtons) / 2; //(gesamthöhe - die höhen der buttons - die abstande zwischen buttons) / 2  =  abstand zu oben und unten

            for (int i = 0; i < numberOfButtons; i++)
            {
                Rectangle newDestRect = new Rectangle(Convert.ToInt32(distanceFromButtonsToLeft), Convert.ToInt32(distanceFromButtonsToTop + i * (buttonHeight + distanceBetweenButtons)), Convert.ToInt32(buttonWidth), Convert.ToInt32(buttonHeight));
                buttonDestinationRectangles.Add(newDestRect);
            }


            #region Keyboardinput
            if (DateTime.Now > nextButtonTime)//todo: enter skipt durch menü
            {

                //Button wird gedrückt gehalten (oder sehr schnell nacheinander gedrückt)
                if (Input.IsKeyDown(lastClickedKey) && lastClickedKey != Keys.None)
                {
                    nextButtonTime = DateTime.Now.AddMilliseconds(100);
                    counter++;

                    if (counter % 8 == 0 && (Input.IsKeyDown(Keys.S) || Input.IsKeyDown(Keys.Down)))
                        pauseMenuNavigator++;

                    else if (counter % 8 == 0 && (Input.IsKeyDown(Keys.W) || Input.IsKeyDown(Keys.Up)))
                        pauseMenuNavigator--;
                }
                else
                {
                    counter = 0;
                    if (Input.IsKeyDown(Keys.S) || Input.IsKeyDown(Keys.Down))
                    {
                        if (Input.IsKeyDown(Keys.S))
                            lastClickedKey = Keys.S;

                        else if (Input.IsKeyDown(Keys.Down))
                            lastClickedKey = Keys.Down;

                        pauseMenuNavigator++;
                    }

                    else if (Input.IsKeyDown(Keys.W) || Input.IsKeyDown(Keys.Up))
                    {
                        if (Input.IsKeyDown(Keys.W))
                            lastClickedKey = Keys.W;

                        else if (Input.IsKeyDown(Keys.Up))
                            lastClickedKey = Keys.Up;

                        pauseMenuNavigator--;
                    }
                    else
                        lastClickedKey = Keys.None;

                }

                if (pauseMenuNavigator > numberOfButtons - 1)
                    pauseMenuNavigator = 0;
                if (pauseMenuNavigator < 0)
                    pauseMenuNavigator = numberOfButtons - 1;

                for (int i = 0; i < numberOfButtons; i++)
                    if (Input.IsKeyDown(Keys.Enter) && pauseMenuNavigator == i)
                        return i;

            }

            #endregion

            #region Mouseinput

            mState = Mouse.GetState();

            for (int i = 0; i < numberOfButtons; i++)
            {
                if (buttonDestinationRectangles[i].Intersects(new Rectangle(mState.Position, new Point(1, 1))))
                    pauseMenuNavigator = i;

                if (buttonDestinationRectangles[i].Intersects(new Rectangle(mState.Position, new Point(1, 1))) && mState.LeftButton == ButtonState.Pressed && pauseMenuNavigator == i && DateTime.Now > nextClickTime)
                {
                    nextClickTime = DateTime.Now.AddMilliseconds(200);
                    return i;
                }
            }

            #endregion

            return -1;

            #region comments
            /*
            #region Keyboardinput
            if (DateTime.Now > nextButtonTime)
            {

                //Button wird gedrückt gehalten (oder sehr schnell nacheinander gedrückt)
                if (Input.IsKeyDown(lastClickedKey) && lastClickedKey != Keys.None)
                {
                    nextButtonTime = DateTime.Now.AddMilliseconds(100);
                    counter++;

                    if (counter % 8 == 0 && (Input.IsKeyDown(Keys.S) || Input.IsKeyDown(Keys.Down)))
                    {
                        pauseMenuNavigator++;
                    }
                    else if (counter % 8 == 0 && (Input.IsKeyDown(Keys.W) || Input.IsKeyDown(Keys.Up)))
                    {
                        pauseMenuNavigator--;
                    }

                }
                else
                {
                    counter = 0;
                    if (Input.IsKeyDown(Keys.S) || Input.IsKeyDown(Keys.Down))
                    {
                        if (Input.IsKeyDown(Keys.S))
                        {
                            lastClickedKey = Keys.S;
                        }
                        else if (Input.IsKeyDown(Keys.Down))
                        {
                            lastClickedKey = Keys.Down;
                        }
                        pauseMenuNavigator++;
                    }

                    else if (Input.IsKeyDown(Keys.W) || Input.IsKeyDown(Keys.Up))
                    {
                        if (Input.IsKeyDown(Keys.W))
                        {
                            lastClickedKey = Keys.W;
                        }
                        else if (Input.IsKeyDown(Keys.Up))
                        {
                            lastClickedKey = Keys.Up;
                        }
                        pauseMenuNavigator--;
                    }
                    else
                    {
                        lastClickedKey = Keys.None;
                    }
                }


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
            #endregion

            #region Mouseinput
            MouseState mState = Mouse.GetState();

            if (destinationRectangleButton0.Intersects(new Microsoft.Xna.Framework.Rectangle(mState.Position, new Microsoft.Xna.Framework.Point(1, 1))))
            {
                pauseMenuNavigator = 0;
            }
            if (destinationRectangleButton1.Intersects(new Microsoft.Xna.Framework.Rectangle(mState.Position, new Microsoft.Xna.Framework.Point(1, 1))))
            {
                pauseMenuNavigator = 1;
            }
            if (destinationRectangleButton0.Intersects(new Microsoft.Xna.Framework.Rectangle(mState.Position, new Microsoft.Xna.Framework.Point(1, 1))) && mState.LeftButton == ButtonState.Pressed && pauseMenuNavigator == 0)
            {
                paused = false;
                CommonFunctions.generalGameSpeed = generalSpeedSaver;
                CommonFunctions.generalColour = Microsoft.Xna.Framework.Color.White;
            }

            if (destinationRectangleButton1.Intersects(new Microsoft.Xna.Framework.Rectangle(mState.Position, new Microsoft.Xna.Framework.Point(1, 1))) && mState.LeftButton == ButtonState.Pressed && pauseMenuNavigator == 1)
            {
                Destroy(this);
                spaceshipList.Remove(this);
                CommonFunctions.currentGameStartController.gameStarted = false;
                CommonFunctions.generalColour = Microsoft.Xna.Framework.Color.White;
            }
            #endregion

            //old menu
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
            */
            #endregion
        }

        public void DrawMenu(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < numberOfButtons; i++)
            {
                if (i == pauseMenuNavigator)
                    spriteBatch.Draw(activeButtonTextureList[i], buttonDestinationRectangles[i], Color.White);
                else
                    spriteBatch.Draw(passiveButtonTextureList[i], buttonDestinationRectangles[i], Color.White);
            }

            spriteBatch.Draw(pointerTexture, new Rectangle(mState.Position, new Point(17, 24)), Color.White);

        }
        
    }
}
