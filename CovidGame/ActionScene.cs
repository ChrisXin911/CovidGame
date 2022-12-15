using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Animation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace CovidGame
{
    public class ActionScene : GameScene
    {

        //bat
        private Texture2D batTex;
        private Vector2 batSpeed;
        private Vector2 batInitPos;
        //ball
        private Texture2D ballTex;
        private Vector2 ballSpeed;
        private Vector2 ballInitPos;
        private Vector2 tempBallSpeed;

        //pause
        private bool pauseflag = true;
        //covid
        private List<Covid> covids;
        private Texture2D covidTex;
        private int covidCount = 48;

        // blackcovid for leve2
        private BlackCovid blackCovid;
        private Texture2D blackCovidTex;
        private Vector2 blackCovidSpeed;
        private Vector2 blackCovidPosition;
        private int level2 = 0;

        //backgroud
        private Texture2D backgroudTex;

        //boundary
        private Texture2D blankTex;
        private Rectangle boudary;

        //explosion
        private Texture2D explosionTex;

        //score
        private string score;
        private int scoreCount;
        private Vector2 scorePosition;
        private SpriteFont scoreFont;

        //the sounds
        private SoundEffect dingSound;
        private SoundEffect clickSound;
        private SoundEffect applause;

        public ActionScene(Game game) : base(game)
        {
            //this.g = (Game1)game;
            //this.spriteBatch = g._spriteBatch;
            //initial all game components
            //load high score history
            Shared.HighScores.Clear();
            Shared.LoadFile();

            //load sounds
            dingSound = g.Content.Load<SoundEffect>("sound/ding");
            clickSound = g.Content.Load<SoundEffect>("sound/click");
            applause = g.Content.Load<SoundEffect>("sound/applause1");
            //add score
            score = "Current Score: ";
           // scoreCount = 0;
            scorePosition = new Vector2(10, 10);
            //score += scoreCount.ToString();
            scoreFont = g.Content.Load<SpriteFont>("fonts/highlightFont");

            //add  ball
            ballTex = g.Content.Load<Texture2D>("images/Ball");
            ballSpeed = new Vector2(4, -3);
            ballInitPos = new Vector2(Shared.Stage.X / 2 - ballTex.Width / 2, Shared.Stage.Y / 2 - ballTex.Height / 2);



            //add bat
            batTex = g.Content.Load<Texture2D>("images/Bat");
            batSpeed = new Vector2(4, 0);
            batInitPos = new Vector2(Shared.Stage.X / 2 - batTex.Width / 2, Shared.Stage.Y - batTex.Height);
            //add background
            backgroudTex = g.Content.Load<Texture2D>("images/Backgroud");

            //add boundary
            blankTex = g.Content.Load<Texture2D>("images/Blank");
            boudary = new Rectangle(0, 45, (int)Shared.Stage.X, 5);


            //add covid
            covidTex = g.Content.Load<Texture2D>("images/GreenCovid");
            covids = new List<Covid>();
            for (int i = 0; i < 3; i++)//ROW 3
            {
                for (int j = 0; j < 16; j++)//col 5
                {
                    Covid c = new Covid(i, j);
                    c.Rectangle = new Rectangle(0 + j * 50, 50 + i * 50, 50, 50);
                    covids.Add(c);
                }
            }

            //add blackcoivd in level2
            blackCovid = new BlackCovid();
            blackCovidTex = g.Content.Load<Texture2D>("images/BombCovid");
            blackCovidPosition = new Vector2(0, Shared.Stage.Y / 2 - ballTex.Height / 2);
            blackCovidSpeed = new Vector2(2, 0);
           
            //add explosion
            explosionTex = g.Content.Load<Texture2D>("images/explosion");



            //add backgroud music
            Song backgroundMusic = g.Content.Load<Song>("sound/chimes");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(backgroundMusic);

        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            //draw background
            spriteBatch.Draw(backgroudTex, Vector2.Zero, Color.White);
            //draw score
            string current= score + scoreCount.ToString();
            spriteBatch.DrawString(scoreFont, current, scorePosition, Color.White);

            //draw current level
            string level = "Current Level: "+(level2+1).ToString();
            spriteBatch.DrawString(scoreFont, level, scorePosition+ new Vector2(540,0), Color.White);
            //draw boundary
            spriteBatch.Draw(blankTex, boudary, Color.White);


            //draw ball
            spriteBatch.Draw(ballTex, ballInitPos, Color.White);
            //draw bat
            spriteBatch.Draw(batTex, batInitPos, Color.White);
            //add covid
            foreach (var item in covids)
            {
                if (item.IsAlive)
                {
                    spriteBatch.Draw(covidTex, item.Rectangle, Color.White);                  
                }

            }
            //add blackCovid
            if (level2 ==1 && blackCovid.IsAlive == true)
            {
                spriteBatch.Draw(blackCovidTex, blackCovidPosition, Color.White);
                
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }

    

        public override async void Update(GameTime gameTime)
        {
            //Get the rectangle of each 
            Rectangle ballRect = new Rectangle((int)ballInitPos.X, (int)ballInitPos.Y, ballTex.Width, ballTex.Height);
            Rectangle batRect = new Rectangle((int)batInitPos.X, (int)batInitPos.Y, batTex.Width, batTex.Height);
            blackCovid.Rectangle = new Rectangle((int)blackCovidPosition.X, (int)blackCovidPosition.Y, blackCovidTex.Width, blackCovidTex.Height);
            //BALL move
            ballInitPos += ballSpeed;

            //top wall
            if (ballInitPos.Y <= 0 + 50)
            {
                ballSpeed = new Vector2(ballSpeed.X, -ballSpeed.Y);
                if (ballSpeed.Y >=-1 && ballSpeed.Y<=1)
                {
                    ballSpeed.Y = 3;
                }
                dingSound.Play();

            }
            //BOTTOM
            if (ballInitPos.Y > Shared.Stage.Y)
            {
                applause.Play();
                this.Enabled = false;
                NameScore nameScore = new NameScore()
                {
                    Name = $"The player {Shared.HighScores.Count+1}",
                    Score = scoreCount
                };
                Shared.AddHighScores(nameScore);
                string[] buttons = { "YES:)", "NO:(" };
               var resultTask =  MessageBox.Show($"play times: {Shared.HighScores.Count} ", "One More Try?", buttons);
                var result = await resultTask;
                if (result==0)
                {
                    initialize();
                    
                }else if (result == 1)
                {
                    
                    Shared.Status = 1;
                    
                }
               
                
            }
            //right wall
            if (ballInitPos.X > Shared.Stage.X - ballTex.Width)
            {
                ballSpeed = new Vector2(-ballSpeed.X, ballSpeed.Y);
                dingSound.Play();
            }
            //left wall
            if (ballInitPos.X <= 0)
            {
                ballSpeed = new Vector2(-ballSpeed.X, ballSpeed.Y);
                dingSound.Play();
            }



            //the ball hit covid
            foreach (var covid in covids)
            {
                if (covid.IsAlive)
                {

                    if (covid.Rectangle.Intersects(ballRect))
                    {

                        covid.IsAlive = false;
                        ballSpeed = new Vector2(ballSpeed.X, -ballSpeed.Y);
                        scoreCount++;
                        covidCount--;
                        dingSound.Play();
                        if (covidCount ==0)
                        {
                            applause.Play();
                            if (level2 == 1)
                            {//finish the whole game
                               
                                this.Enabled = false;
                                NameScore nameScore = new NameScore()
                                {
                                    Name = $"The player {Shared.HighScores.Count+1}",
                                    Score = scoreCount
                                };
                                Shared.AddHighScores(nameScore);
                                string[] buttons = { "YES:)", "NO:(" };
                                var resultTask = MessageBox.Show($"play times: {Shared.HighScores.Count} ", "Finish!\nOne More Try?", buttons);
                                var result = await resultTask;
                                if (result == 0)
                                {
                                    initialize();
                                }
                                else if (result == 1)
                                {

                                    Shared.Status = 1;

                                }

                            }
                            else
                            {                               
                                initialize();
                                //begin the level2
                                ballSpeed = new Vector2(4, -3);
                                level2 = 1;
                                scoreCount = 48;
                            }
                        }
                       
                    }
                }

            }
            //level2 black covid move

            if (level2 ==1)
            {
                blackCovidPosition += blackCovidSpeed;
                if (blackCovidPosition.X > Shared.Stage.X)
                {
                    blackCovidPosition.X = 0;
                }

                //ball hit the black covid
                if (blackCovid.IsAlive && blackCovid.Rectangle.Intersects(ballRect))
                {
                    ballSpeed = new Vector2(ballSpeed.X, -Math.Abs(ballSpeed.Y));
                    blackCovidSpeed = Vector2.Zero;
                    dingSound.Play();
                    scoreCount += 10;
                    blackCovid.Life--;
                    if (blackCovid.Life == 2)
                    {
                        blackCovidTex = g.Content.Load<Texture2D>("images/GoldenCovid");
                        scoreCount += 5;
                    }
                    if (blackCovid.Life == 0)
                    {
                        
                        Vector2 position = blackCovidPosition;

                        Explosion explosion = new Explosion(g, spriteBatch, explosionTex, position, 3);
                        this.Components.Add(explosion);
                        explosion.restart();
                        blackCovid.IsAlive = false;
                    }

                }
            }

         
            //bat move
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Left))
            {
                batInitPos -= batSpeed;
                if (batInitPos.X < 0)
                {


                    batInitPos = new Vector2(0, batInitPos.Y);
                }
            }
            if (ks.IsKeyDown(Keys.Right))
            {
                batInitPos += batSpeed;
                if (batInitPos.X >= Shared.Stage.X - batTex.Width)
                {

                    batInitPos = new Vector2(Shared.Stage.X - batTex.Width, batInitPos.Y);
                }
            }
            
            if (ks.IsKeyDown(Keys.Space))
            {
                if (pauseflag == true)
                {
                    tempBallSpeed = ballSpeed;
                    ballSpeed = Vector2.Zero;
                    pauseflag = false;
                }
                else
                {
                    ballSpeed = tempBallSpeed;
                    tempBallSpeed = Vector2.Zero;
                    pauseflag = true;
                }
                

              
            }


            //collision between bat and ball

            if (batRect.Intersects(ballRect))
            {
                ballSpeed = new Vector2(ballSpeed.X, -Math.Abs(ballSpeed.Y));
                clickSound.Play();
            }
            base.Update(gameTime);
        }
/// <summary>
/// initialize the game when failed or begin level2
/// </summary>
        private void initialize()
        {
            this.Enabled = true;
            covidCount = 48;
            for (int i = 0; i < covids.Count; i++)
            {
                covids[i].IsAlive = true;

            }
            ballInitPos= new Vector2(Shared.Stage.X / 2 - ballTex.Width / 2, Shared.Stage.Y / 2 - ballTex.Height / 2);
            ballSpeed = new Vector2(4, -3);
            scoreCount = 0;
            level2 = 0;
            blackCovid.IsAlive = true;
            
        }

       
    }
}
