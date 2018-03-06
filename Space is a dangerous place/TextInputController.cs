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
    class TextInputController
    {

        KeyboardState Input;
        Keys lastClickedKey;
        DateTime nextButtonTime;

        string inputPromt = "";
        
        string inputString = "";
        int msToAdd = 140;

        MenuController menuController;
        SpriteFont font;


        public TextInputController(MenuController menuController)
        {

            CommonFunctions.currentTextInputController = this;
            font = CommonFunctions.font;

            this.menuController = menuController;

        }

        public void Update(string inputPromt)
        {

            this.inputPromt = inputPromt;

            Input = CommonFunctions.Input;

            if (!Input.IsKeyDown(lastClickedKey) || DateTime.Now > nextButtonTime)
            {

                #region abomination
                if (Input.IsKeyDown(Keys.A))
                {
                    inputString += 'A';
                    lastClickedKey = Keys.A;
                    nextButtonTime = DateTime.Now.AddMilliseconds(msToAdd);
                }

                else if (Input.IsKeyDown(Keys.B))
                {
                    inputString += 'B';
                    lastClickedKey = Keys.B;
                    nextButtonTime = DateTime.Now.AddMilliseconds(msToAdd);
                }

                else if (Input.IsKeyDown(Keys.C))
                {
                    inputString += 'C';
                    lastClickedKey = Keys.C;
                    nextButtonTime = DateTime.Now.AddMilliseconds(msToAdd);
                }

                else if (Input.IsKeyDown(Keys.D))
                {
                    inputString += 'D';
                    lastClickedKey = Keys.D;
                    nextButtonTime = DateTime.Now.AddMilliseconds(msToAdd);
                }

                else if (Input.IsKeyDown(Keys.E))
                {
                    inputString += 'E';
                    lastClickedKey = Keys.E;
                    nextButtonTime = DateTime.Now.AddMilliseconds(msToAdd);
                }

                else if (Input.IsKeyDown(Keys.F))
                {
                    inputString += 'F';
                    lastClickedKey = Keys.F;
                    nextButtonTime = DateTime.Now.AddMilliseconds(msToAdd);
                }

                else if (Input.IsKeyDown(Keys.G))
                {
                    inputString += 'G';
                    lastClickedKey = Keys.G;
                    nextButtonTime = DateTime.Now.AddMilliseconds(msToAdd);
                }

                else if (Input.IsKeyDown(Keys.H))
                {
                    inputString += 'H';
                    lastClickedKey = Keys.H;
                    nextButtonTime = DateTime.Now.AddMilliseconds(msToAdd);
                }

                else if (Input.IsKeyDown(Keys.I))
                {
                    inputString += 'I';
                    lastClickedKey = Keys.I;
                    nextButtonTime = DateTime.Now.AddMilliseconds(msToAdd);
                }

                else if (Input.IsKeyDown(Keys.J))
                {
                    inputString += 'J';
                    lastClickedKey = Keys.J;
                    nextButtonTime = DateTime.Now.AddMilliseconds(msToAdd);
                }

                else if (Input.IsKeyDown(Keys.K))
                {
                    inputString += 'K';
                    lastClickedKey = Keys.K;
                    nextButtonTime = DateTime.Now.AddMilliseconds(msToAdd);
                }

                else if (Input.IsKeyDown(Keys.L))
                {
                    inputString += 'L';
                    lastClickedKey = Keys.L;
                    nextButtonTime = DateTime.Now.AddMilliseconds(msToAdd);
                }

                else if (Input.IsKeyDown(Keys.M))
                {
                    inputString += 'M';
                    lastClickedKey = Keys.M;
                    nextButtonTime = DateTime.Now.AddMilliseconds(msToAdd);
                }

                else if (Input.IsKeyDown(Keys.N))
                {
                    inputString += 'N';
                    lastClickedKey = Keys.N;
                    nextButtonTime = DateTime.Now.AddMilliseconds(msToAdd);
                }

                else if (Input.IsKeyDown(Keys.O))
                {
                    inputString += 'O';
                    lastClickedKey = Keys.O;
                    nextButtonTime = DateTime.Now.AddMilliseconds(msToAdd);
                }

                else if (Input.IsKeyDown(Keys.P))
                {
                    inputString += 'P';
                    lastClickedKey = Keys.P;
                    nextButtonTime = DateTime.Now.AddMilliseconds(msToAdd);
                }

                else if (Input.IsKeyDown(Keys.Q))
                {
                    inputString += 'Q';
                    lastClickedKey = Keys.Q;
                    nextButtonTime = DateTime.Now.AddMilliseconds(msToAdd);
                }

                else if (Input.IsKeyDown(Keys.R))
                {
                    inputString += 'R';
                    lastClickedKey = Keys.R;
                    nextButtonTime = DateTime.Now.AddMilliseconds(msToAdd);
                }

                else if (Input.IsKeyDown(Keys.S))
                {
                    inputString += 'S';
                    lastClickedKey = Keys.S;
                    nextButtonTime = DateTime.Now.AddMilliseconds(msToAdd);
                }

                else if (Input.IsKeyDown(Keys.T))
                {
                    inputString += 'T';
                    lastClickedKey = Keys.T;
                    nextButtonTime = DateTime.Now.AddMilliseconds(msToAdd);
                }

                else if (Input.IsKeyDown(Keys.U))
                {
                    inputString += 'U';
                    lastClickedKey = Keys.U;
                    nextButtonTime = DateTime.Now.AddMilliseconds(msToAdd);
                }

                else if (Input.IsKeyDown(Keys.V))
                {
                    inputString += 'V';
                    lastClickedKey = Keys.V;
                    nextButtonTime = DateTime.Now.AddMilliseconds(msToAdd);
                }

                else if (Input.IsKeyDown(Keys.W))
                {
                    inputString += 'W';
                    lastClickedKey = Keys.W;
                    nextButtonTime = DateTime.Now.AddMilliseconds(msToAdd);
                }

                else if (Input.IsKeyDown(Keys.X))
                {
                    inputString += 'X';
                    lastClickedKey = Keys.X;
                    nextButtonTime = DateTime.Now.AddMilliseconds(msToAdd);
                }

                else if (Input.IsKeyDown(Keys.Y))
                {
                    inputString += 'Y';
                    lastClickedKey = Keys.Y;
                    nextButtonTime = DateTime.Now.AddMilliseconds(msToAdd);
                }

                else if (Input.IsKeyDown(Keys.Z))
                {
                    inputString += 'Z';
                    lastClickedKey = Keys.Z;
                    nextButtonTime = DateTime.Now.AddMilliseconds(msToAdd);
                }
                #endregion

                else if (Input.IsKeyDown(Keys.Back))
                {
                    try
                    {
                        inputString = inputString.Remove(inputString.Length - 1);
                    }
                    catch (ArgumentOutOfRangeException)
                    {

                    }
                    lastClickedKey = Keys.Back;
                    nextButtonTime = DateTime.Now.AddMilliseconds(msToAdd);
                }

                else if (Input.IsKeyDown(Keys.Enter))
                {
                    Properties.Settings.Default.Name = inputString;
                    Properties.Settings.Default.Save();
                    menuController.lastClickedKey = Keys.Enter;
                    menuController.nextButtonTime = DateTime.Now.AddMilliseconds(menuController.msToAddButton);
                }
                
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, inputPromt, new Vector2(3 * CommonFunctions.aspectRatioMultiplierX, 3 * CommonFunctions.aspectRatioMultiplierY), Color.White, 0, new Vector2(0, 0), 0.7f * CommonFunctions.aspectRatioMultiplierY, 0, 0);
            spriteBatch.DrawString(font, inputString, new Vector2(3 * CommonFunctions.aspectRatioMultiplierX, 26 * CommonFunctions.aspectRatioMultiplierY), Color.White, 0, new Vector2(0, 0), 0.7f * CommonFunctions.aspectRatioMultiplierY, 0, 0);
            spriteBatch.DrawString(font, "Press Enter to continue.", new Vector2(3 * CommonFunctions.aspectRatioMultiplierX, 49 * CommonFunctions.aspectRatioMultiplierY), Color.White, 0, new Vector2(0, 0), 0.7f * CommonFunctions.aspectRatioMultiplierY, 0, 0);
        }

    }
}
