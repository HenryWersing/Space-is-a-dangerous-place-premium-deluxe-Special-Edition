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
    static class CommonFunctions
    {
        

        public static List<ICollidable> ICollidableList = new List<ICollidable>();

        public static float aspectRatioMultiplierX;
        public static float aspectRatioMultiplierY;

        public static Spaceship currentSpaceship;
        public static TerrainController currentTerrainController;
        public static GameStartController currentGameStartController;

        public static System.Drawing.Rectangle borders;
        public static int normalDownwardSpeed = 2;
        public static float generalGameSpeed;
        public static Microsoft.Xna.Framework.Color generalColour = Microsoft.Xna.Framework.Color.White;
        
        public static Random generalRandom = new Random();

        public static bool gameRunning = false;

        //Hier werden die ButtonSkins zwischengelagert, da es keinen Sinn macht, sie über den Spaceship-Constructor zu schicken
        public static Texture2D ActiveButtonContinue;
        public static Texture2D ActiveButonBackToMenu;
        public static Texture2D PassiveButtonContinue;
        public static Texture2D PassiveButtonBackToMenu;


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
