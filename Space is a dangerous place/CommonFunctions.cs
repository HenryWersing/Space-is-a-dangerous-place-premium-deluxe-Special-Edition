using Microsoft.Xna.Framework;
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

        public static Spaceship currentSpaceship;
        public static GameStartController currentGameStartController;

        public static System.Drawing.Rectangle borders;
        public static int terrainSpeed = 2;

        public static bool gameRunning = false;
        //todo: aspectRatio einbauen? und mit allen relatieven werten multioplizieren
        

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
                dropPosition = new Vector2(borders.Left + parentTerrain.ObjectSize.Width + borders.Right * 01 / 130, parentTerrain.position.Y + parentTerrain.ObjectSize.Height / 2 - dropSize.Height / 2); //  0.1 / 13
            else
                dropPosition = new Vector2(parentTerrain.position.X - borders.Right * 01 / 130 - dropSize.Width, parentTerrain.position.Y + parentTerrain.ObjectSize.Height / 2 - dropSize.Height / 2); //  0.1 / 13

            return dropPosition;

        }

    }
}
