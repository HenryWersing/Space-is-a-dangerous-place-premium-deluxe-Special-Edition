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
    class Drop: ICollidable
    {

        public Vector2 position;
        public Vector2 PositionForRectangle { get; set; }
        public Size ObjectSize { get; set; }

        public Vector2 direction;

        public Texture2D Skin { get; private set; }

        public Terrain parentTerrain;

        public Microsoft.Xna.Framework.Rectangle destinationRectangle;
        
        public int rdmInt;

        
        public Drop(Vector2 position, Size size, Texture2D skin, Terrain parentTerrain )
        {

            this.position = position;
            ObjectSize = size;
            Skin = skin;

            this.parentTerrain = parentTerrain;

            PositionForRectangle = position;

        }

        public virtual void Destroy(ICollidable collidingObject)
        {
            
            CommonFunctions.ICollidableList.Remove(this);

        }

        public virtual void Update()
        {

            if (parentTerrain.broken == false)
                position = CommonFunctions.DetermineDropPosition(parentTerrain, ObjectSize);
            else
                position = new Vector2(position.X, CommonFunctions.DetermineDropPosition(parentTerrain, ObjectSize).Y);

            destinationRectangle = new Microsoft.Xna.Framework.Rectangle(Convert.ToInt32(position.X), Convert.ToInt32(position.Y), ObjectSize.Width, ObjectSize.Height);

            PositionForRectangle = position;

        }

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(Skin, destinationRectangle, CommonFunctions.generalColour);

        }

    }
}
