using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNA_test.Source.Entities
{
    class Ball
    {

        Vector2 motion;
        Vector2 position;
        Rectangle bounds;

        float speed;
        const float startSpeed = 4f;

        Texture2D texture;
        Rectangle screenBounds;

        bool collided;

        public Rectangle Bounds
        {
            get
            {
                bounds.X = (int) position.X;
                bounds.Y = (int) position.Y;
                return bounds;
            }
        }

        public Ball(Texture2D texture, Rectangle screenBounds)
        {
            this.texture = texture;
            this.screenBounds = screenBounds;

            bounds = new Rectangle(0, 0, texture.Width, texture.Height);
        }

        public void Update()
        {
            collided = false;
            position += motion * speed;
            speed += 0.01f;

            CheckWallCollision();
        }

        private void CheckWallCollision()
        {
            if (position.X < 0)
            {
                position.X = 0;
                motion.X *= -1;
            }
            if (position.X + texture.Width > screenBounds.Width)
            {
                position.X = screenBounds.Width - texture.Width;
                motion.X *= -1;
            }
            if (position.Y < 0)
            {
                position.Y = 0;
                motion.Y *= -1;
            }
        }

        public void SetInStartPosition(Rectangle paddleLocation)
        {
            Random r = new Random();
            motion = new Vector2(r.Next(2, 6), -r.Next(2, 6));
            motion.Normalize();

            speed = startSpeed;

            position.Y = paddleLocation.Y - texture.Height;
            position.X = paddleLocation.X + ((paddleLocation.Width - texture.Width) / 2);
        }

        public bool OffBottom()
        {
            return position.Y > screenBounds.Height;
        }

        public void PaddleCollision(Rectangle paddleLocation)
        {
            Rectangle ballLocation = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            if (paddleLocation.Intersects(ballLocation))
            {
                position.Y = paddleLocation.Y - texture.Height;
                motion.Y *= -1;
            }
        }

        public void Draw(SpriteBatch sprites)
        {
            sprites.Draw(texture, position, Color.White);
        }

        public void Deflect(Brick brick)
        {
            if (!collided)
            {
                motion.Y *= -1;

                /*
                 * TODO: cleverer collision detection here (intersecting line segments
                 * of the ball's movement between frames and each edge of the brick's
                 * bounding box)
                 */

                collided = true;
            }
        }
    }
}
