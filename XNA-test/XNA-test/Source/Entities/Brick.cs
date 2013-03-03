using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNA_test.Source.Entities
{
    class Brick
    {

        Texture2D texture;
        Rectangle location;
        Color tint;
        bool alive;

        public Rectangle Location
        {
            get { return location; }
        }

        public Brick(Texture2D texture, Rectangle location, Color tint)
        {
            this.texture = texture;
            this.location = location;
            this.tint = tint;
            this.alive = true;
        }

        public void CheckCollision(Ball ball)
        {
            if (alive && ball.Bounds.Intersects(location))
            {
                alive = false;
                ball.Deflect(this);
            }
        }

        public void Draw(SpriteBatch sprites)
        {
            if (alive)
            {
                sprites.Draw(texture, location, tint);
            }
        }
    }
}
