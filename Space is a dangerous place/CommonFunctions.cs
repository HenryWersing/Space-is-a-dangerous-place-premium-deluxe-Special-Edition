﻿using Microsoft.Xna.Framework;
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
    static class CommonFunctions
    {
        
        public static KeyboardState Input;
        public static MouseState mState;

        public static List<ICollidable> ICollidableList = new List<ICollidable>();
        
        public static float aspectRatioMultiplierX;
        public static float aspectRatioMultiplierY;

        public static Spaceship currentSpaceship;
        public static TerrainController currentTerrainController;
        public static GameStartController currentGameStartController;
        public static TextInputController currentTextInputController;

        public static System.Drawing.Rectangle borders;
        public static int normalDownwardSpeed = 2;
        public static float generalGameSpeed;
        public static Microsoft.Xna.Framework.Color generalColour = Microsoft.Xna.Framework.Color.White;
        
        public static Random generalRandom = new Random();

        public static bool terrainSpawning = false;

        public static SpriteFont font;

        #region buttonskins
        public static Texture2D ActiveButtonContinue;
        public static Texture2D ActiveButonBackToMenu;
        public static Texture2D ActiveButtonStart;
        public static Texture2D ActiveButtonBack;
        public static Texture2D ActiveButtonQuitGame;
        public static Texture2D ActiveButtonTitan;
        public static Texture2D ActiveButtonNormal;
        public static Texture2D ActiveButtonRisky;
        public static Texture2D ActiveButtonResetScore;
        public static Texture2D ActiveButtonTutorial;
        public static Texture2D ActiveButtonChangeName;
        public static Texture2D ActiveButtonOptions;
        public static Texture2D ActiveButtonResolution;
        public static Texture2D ActiveButton500x500;
        public static Texture2D ActiveButton650x650;
        public static Texture2D ActiveButton800x800;
        public static Texture2D ActiveButton950x950;
        public static Texture2D ActiveButtonSubmitScore;
        public static Texture2D ActiveButtonHighscores;
        public static Texture2D PassiveButtonContinue;
        public static Texture2D PassiveButtonBackToMenu;
        public static Texture2D PassiveButtonStart;
        public static Texture2D PassiveButtonBack;
        public static Texture2D PassiveButtonQuitGame;
        public static Texture2D PassiveButtonTitan;
        public static Texture2D PassiveButtonNormal;
        public static Texture2D PassiveButtonRisky;
        public static Texture2D PassiveButtonResetScore;
        public static Texture2D PassiveButtonTutorial;
        public static Texture2D PassiveButtonChangeName;
        public static Texture2D PassiveButtonOptions;
        public static Texture2D PassiveButtonResolution;
        public static Texture2D PassiveButton500x500;
        public static Texture2D PassiveButton650x650;
        public static Texture2D PassiveButton800x800;
        public static Texture2D PassiveButton950x950;
        public static Texture2D PassiveButtonSubmitScore;
        public static Texture2D PassiveButtonHighscores;
        #endregion


        public static ICollidable CheckCollision(ICollidable objectToCheck, List<ICollidable> objectsToCollide)
        {

            ICollidable objectToReturn;
            objectToReturn = null; //bei keiner collision wird null zurückgegeben

            foreach (ICollidable objectToCollide in objectsToCollide)
            {
                System.Drawing.Rectangle rectangle1 = SizeToRectangle(objectToCheck.ObjectSize, objectToCheck.PositionForRectangle);
                System.Drawing.Rectangle rectangle2 = SizeToRectangle(objectToCollide.ObjectSize, objectToCollide.PositionForRectangle);

                if (rectangle1.IntersectsWith(rectangle2))
                    objectToReturn = objectToCollide;
            }

            return objectToReturn;

        }

        public static System.Drawing.Rectangle SizeToRectangle(Size size, Vector2 position)
        {

            System.Drawing.Rectangle rectangleToReturn;

            rectangleToReturn = new System.Drawing.Rectangle(Convert.ToInt32(position.X), Convert.ToInt32(position.Y), size.Width, size.Height);

            return rectangleToReturn;

        }

        public static Vector2 DetermineDropPosition(Terrain parentTerrain, Size dropSize)
        {

            Vector2 dropPosition;

            if (parentTerrain.position.X == borders.Left)
                dropPosition = new Vector2(borders.Left + parentTerrain.ObjectSize.Width + 5 * aspectRatioMultiplierX, parentTerrain.position.Y + parentTerrain.ObjectSize.Height / 2 - dropSize.Height / 2); //  0.1 / 13
            else
                dropPosition = new Vector2(parentTerrain.position.X - 5 * aspectRatioMultiplierX - dropSize.Width, parentTerrain.position.Y + parentTerrain.ObjectSize.Height / 2 - dropSize.Height / 2); //  0.1 / 13

            return dropPosition;

        }

    }
}
