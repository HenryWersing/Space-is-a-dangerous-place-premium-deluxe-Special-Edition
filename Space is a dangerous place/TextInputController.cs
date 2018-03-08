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
        string InputString
        {
            get
            {
                return inputString;
            }
            set
            {
                if (value.Length <= 10)
                    inputString = value;
            }
        }

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
                //TODO: verschönern?
                #region abomination
                if (Input.IsKeyDown(Keys.A))
                {
                    InputString += 'A';
                    lastClickedKey = Keys.A;
                    nextButtonTime = DateTime.Now.AddMilliseconds(msToAdd);
                }

                else if (Input.IsKeyDown(Keys.B))
                {
                    InputString += 'B';
                    lastClickedKey = Keys.B;
                    nextButtonTime = DateTime.Now.AddMilliseconds(msToAdd);
                }

                else if (Input.IsKeyDown(Keys.C))
                {
                    InputString += 'C';
                    lastClickedKey = Keys.C;
                    nextButtonTime = DateTime.Now.AddMilliseconds(msToAdd);
                }

                else if (Input.IsKeyDown(Keys.D))
                {
                    InputString += 'D';
                    lastClickedKey = Keys.D;
                    nextButtonTime = DateTime.Now.AddMilliseconds(msToAdd);
                }

                else if (Input.IsKeyDown(Keys.E))
                {
                    InputString += 'E';
                    lastClickedKey = Keys.E;
                    nextButtonTime = DateTime.Now.AddMilliseconds(msToAdd);
                }

                else if (Input.IsKeyDown(Keys.F))
                {
                    InputString += 'F';
                    lastClickedKey = Keys.F;
                    nextButtonTime = DateTime.Now.AddMilliseconds(msToAdd);
                }

                else if (Input.IsKeyDown(Keys.G))
                {
                    InputString += 'G';
                    lastClickedKey = Keys.G;
                    nextButtonTime = DateTime.Now.AddMilliseconds(msToAdd);
                }

                else if (Input.IsKeyDown(Keys.H))
                {
                    InputString += 'H';
                    lastClickedKey = Keys.H;
                    nextButtonTime = DateTime.Now.AddMilliseconds(msToAdd);
                }

                else if (Input.IsKeyDown(Keys.I))
                {
                    InputString += 'I';
                    lastClickedKey = Keys.I;
                    nextButtonTime = DateTime.Now.AddMilliseconds(msToAdd);
                }

                else if (Input.IsKeyDown(Keys.J))
                {
                    InputString += 'J';
                    lastClickedKey = Keys.J;
                    nextButtonTime = DateTime.Now.AddMilliseconds(msToAdd);
                }

                else if (Input.IsKeyDown(Keys.K))
                {
                    InputString += 'K';
                    lastClickedKey = Keys.K;
                    nextButtonTime = DateTime.Now.AddMilliseconds(msToAdd);
                }

                else if (Input.IsKeyDown(Keys.L))
                {
                    InputString += 'L';
                    lastClickedKey = Keys.L;
                    nextButtonTime = DateTime.Now.AddMilliseconds(msToAdd);
                }

                else if (Input.IsKeyDown(Keys.M))
                {
                    InputString += 'M';
                    lastClickedKey = Keys.M;
                    nextButtonTime = DateTime.Now.AddMilliseconds(msToAdd);
                }

                else if (Input.IsKeyDown(Keys.N))
                {
                    InputString += 'N';
                    lastClickedKey = Keys.N;
                    nextButtonTime = DateTime.Now.AddMilliseconds(msToAdd);
                }

                else if (Input.IsKeyDown(Keys.O))
                {
                    InputString += 'O';
                    lastClickedKey = Keys.O;
                    nextButtonTime = DateTime.Now.AddMilliseconds(msToAdd);
                }

                else if (Input.IsKeyDown(Keys.P))
                {
                    InputString += 'P';
                    lastClickedKey = Keys.P;
                    nextButtonTime = DateTime.Now.AddMilliseconds(msToAdd);
                }

                else if (Input.IsKeyDown(Keys.Q))
                {
                    InputString += 'Q';
                    lastClickedKey = Keys.Q;
                    nextButtonTime = DateTime.Now.AddMilliseconds(msToAdd);
                }

                else if (Input.IsKeyDown(Keys.R))
                {
                    InputString += 'R';
                    lastClickedKey = Keys.R;
                    nextButtonTime = DateTime.Now.AddMilliseconds(msToAdd);
                }

                else if (Input.IsKeyDown(Keys.S))
                {
                    InputString += 'S';
                    lastClickedKey = Keys.S;
                    nextButtonTime = DateTime.Now.AddMilliseconds(msToAdd);
                }

                else if (Input.IsKeyDown(Keys.T))
                {
                    InputString += 'T';
                    lastClickedKey = Keys.T;
                    nextButtonTime = DateTime.Now.AddMilliseconds(msToAdd);
                }

                else if (Input.IsKeyDown(Keys.U))
                {
                    InputString += 'U';
                    lastClickedKey = Keys.U;
                    nextButtonTime = DateTime.Now.AddMilliseconds(msToAdd);
                }

                else if (Input.IsKeyDown(Keys.V))
                {
                    InputString += 'V';
                    lastClickedKey = Keys.V;
                    nextButtonTime = DateTime.Now.AddMilliseconds(msToAdd);
                }

                else if (Input.IsKeyDown(Keys.W))
                {
                    InputString += 'W';
                    lastClickedKey = Keys.W;
                    nextButtonTime = DateTime.Now.AddMilliseconds(msToAdd);
                }

                else if (Input.IsKeyDown(Keys.X))
                {
                    InputString += 'X';
                    lastClickedKey = Keys.X;
                    nextButtonTime = DateTime.Now.AddMilliseconds(msToAdd);
                }

                else if (Input.IsKeyDown(Keys.Y))
                {
                    InputString += 'Y';
                    lastClickedKey = Keys.Y;
                    nextButtonTime = DateTime.Now.AddMilliseconds(msToAdd);
                }

                else if (Input.IsKeyDown(Keys.Z))
                {
                    InputString += 'Z';
                    lastClickedKey = Keys.Z;
                    nextButtonTime = DateTime.Now.AddMilliseconds(msToAdd);
                }
                #endregion

                else if (Input.IsKeyDown(Keys.Back))
                {
                    try
                    {
                        InputString = InputString.Remove(InputString.Length - 1);
                    }
                    catch (ArgumentOutOfRangeException)
                    {

                    }
                    lastClickedKey = Keys.Back;
                    nextButtonTime = DateTime.Now.AddMilliseconds(msToAdd);
                }

                else if (Input.IsKeyDown(Keys.Enter))
                {
                    Properties.Settings.Default.Name = InputString;
                    Properties.Settings.Default.Save();
                    menuController.lastClickedKey = Keys.Enter;
                    menuController.nextButtonTime = DateTime.Now.AddMilliseconds(menuController.msToAddButton);
                }
                
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, inputPromt, new Vector2(3 * CommonFunctions.aspectRatioMultiplierX, 3 * CommonFunctions.aspectRatioMultiplierY), Color.White, 0, new Vector2(0, 0), 0.7f * CommonFunctions.aspectRatioMultiplierY, 0, 0);
            spriteBatch.DrawString(font, InputString, new Vector2(3 * CommonFunctions.aspectRatioMultiplierX, 26 * CommonFunctions.aspectRatioMultiplierY), Color.White, 0, new Vector2(0, 0), 0.7f * CommonFunctions.aspectRatioMultiplierY, 0, 0);
            spriteBatch.DrawString(font, "Press Enter to continue.", new Vector2(3 * CommonFunctions.aspectRatioMultiplierX, 49 * CommonFunctions.aspectRatioMultiplierY), Color.White, 0, new Vector2(0, 0), 0.7f * CommonFunctions.aspectRatioMultiplierY, 0, 0);
        }

    }
}
