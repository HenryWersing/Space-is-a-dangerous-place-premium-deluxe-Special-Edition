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
        
        int numberOfButtons;
        List<Texture2D> activeButtonTextureList = new List<Texture2D>();
        List<Texture2D> passiveButtonTextureList = new List<Texture2D>();

        Keys lastClickedKey;
        int counter = 0;
        public DateTime nextButtonTime;
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

            buttonDestinationRectangles.Clear();
            distanceFromButtonsToTop = (CommonFunctions.borders.Bottom - numberOfButtons * buttonHeight - (numberOfButtons - 1) * distanceBetweenButtons) / 2; //(gesamthöhe - die höhen der buttons - die abstande zwischen buttons) / 2  =  abstand zu oben und unten

            for (int i = 0; i < numberOfButtons; i++)
            {
                Rectangle newDestRect = new Rectangle(Convert.ToInt32(distanceFromButtonsToLeft), Convert.ToInt32(distanceFromButtonsToTop + i * (buttonHeight + distanceBetweenButtons)), Convert.ToInt32(buttonWidth), Convert.ToInt32(buttonHeight));
                buttonDestinationRectangles.Add(newDestRect);
            }


            #region Keyboardinput
            if (DateTime.Now > nextButtonTime)
            {

                //Button wird gedrückt gehalten (oder sehr schnell nacheinander gedrückt)
                if (CommonFunctions.Input.IsKeyDown(lastClickedKey) && lastClickedKey != Keys.None)
                {
                    nextButtonTime = DateTime.Now.AddMilliseconds(80);
                    counter++;

                    if (counter > 6 && (CommonFunctions.Input.IsKeyDown(Keys.S) || CommonFunctions.Input.IsKeyDown(Keys.Down)))
                        pauseMenuNavigator++;

                    else if (counter > 6 && (CommonFunctions.Input.IsKeyDown(Keys.W) || CommonFunctions.Input.IsKeyDown(Keys.Up)))
                        pauseMenuNavigator--;
                }
                else
                {
                    counter = 0;
                    if (CommonFunctions.Input.IsKeyDown(Keys.S) || CommonFunctions.Input.IsKeyDown(Keys.Down))
                    {
                        if (CommonFunctions.Input.IsKeyDown(Keys.S))
                            lastClickedKey = Keys.S;

                        else if (CommonFunctions.Input.IsKeyDown(Keys.Down))
                            lastClickedKey = Keys.Down;

                        pauseMenuNavigator++;
                    }

                    else if (CommonFunctions.Input.IsKeyDown(Keys.W) || CommonFunctions.Input.IsKeyDown(Keys.Up))
                    {
                        if (CommonFunctions.Input.IsKeyDown(Keys.W))
                            lastClickedKey = Keys.W;

                        else if (CommonFunctions.Input.IsKeyDown(Keys.Up))
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
                    if (CommonFunctions.Input.IsKeyDown(Keys.Enter) && pauseMenuNavigator == i && DateTime.Now > nextButtonTime)
                    {
                        lastClickedKey = Keys.Enter;
                        return i;
                    }

            }

            #endregion

            #region Mouseinput
            
            for (int i = 0; i < numberOfButtons; i++)
            {
                if (buttonDestinationRectangles[i].Intersects(new Rectangle(CommonFunctions.mState.Position, new Point(1, 1))))
                    pauseMenuNavigator = i;

                if (buttonDestinationRectangles[i].Intersects(new Rectangle(CommonFunctions.mState.Position, new Point(1, 1))) && CommonFunctions.mState.LeftButton == ButtonState.Pressed && pauseMenuNavigator == i && DateTime.Now > nextClickTime)
                {
                    nextClickTime = DateTime.Now.AddMilliseconds(200);
                    return i;
                }
            }

            #endregion

            return -1;
            
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

            spriteBatch.Draw(pointerTexture, new Rectangle(CommonFunctions.mState.Position, new Point(17, 24)), Color.White);

        }
        
    }
}
