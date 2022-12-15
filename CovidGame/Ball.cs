using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CovidGame
{
    public class Ball : DrawableGameComponent
    {
        public SoundEffect wallSound { get; set; }

        public SpriteBatch spriteBatch { get; set; }
        public Vector2 position { get; set; }
        public Texture2D tex { get; set; }

        public Vector2 speed { get; set; }
        private SoundEffect missSound;



        public Ball(Game game, SpriteBatch spriteBatch, Vector2 position, Texture2D tex, Vector2 speed, SoundEffect dingSound, SoundEffect missSound) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.position = position;
            this.tex = tex;
            this.speed = speed;
            this.wallSound = dingSound;
            this.missSound = missSound;
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, position, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            position += speed;
      
            //top wall
            if (position.Y <= 0)
            {
                speed = new Vector2(speed.X, -speed.Y);
                wallSound.Play();

            }
            //BOTTOM
            if (position.Y > Shared.Stage.Y)
            {
                missSound.Play();
                this.Enabled = false;
            }
            //right wall
            if (position.X > Shared.Stage.X - tex.Width)
            {
                speed = new Vector2(-speed.X, speed.Y);
                wallSound.Play();
            }
            //left wall
            if (position.X <= 0)
            {
                speed = new Vector2(-speed.X, speed.Y);
                wallSound.Play();
            }

            base.Update(gameTime);
        }
        public Rectangle getBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, tex.Width, tex.Height);
        }
    }
}
