﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace project_game_ver0._3
{
    class player
    {
        
        int direction = 1;
        projectgame.AnimatedTexture playerX;

        //List<lazer> lazerlist = new List<lazer>();
        SpriteBatch spriteBatch;

        Vector2 bgpos = new Vector2(0, 0);
        Vector2 cameraPos = Vector2.Zero;
        Vector2 scroll_factor = new Vector2(1.0f, 1);

        Vector2 posplayer = new Vector2(350, 100);
        Game1 game;
        SpriteFont font;
        GraphicsDeviceManager graphics;
        KeyboardState old_ks;
        private float Rotation = 0;
        private float Scale = 1.0f;
        private float Depth = 0.5f;
        float gravity=3.0f;
        float foce=0;
        bool jumpplayer = false;
        bool shoot = false;
        int count;
        public Vector2 speed;
        spawnmonster monster;
        lazer lazer;
        camera camera;
        background bg;

        public player(Game1 game, string asset)
        {
            playerX = new projectgame.AnimatedTexture(Vector2.Zero, Rotation, Scale, Depth);
            lazer = new lazer(posplayer);
            monster = new spawnmonster(1);
            this.game = game;
            bg = new background(game);
            

        }
        public void Load(ContentManager content, string asset)
        {
            playerX.Load(content, asset, 12, 3, 10);
            font = content.Load<SpriteFont>("font");
            lazer.Load(content);
            bg.Load(content);
            monster.Load(content);
        }

        public void Update(float elapsed)
        {
            playerX.UpdateFrame(elapsed);
            posplayer.X += 1;

            Rectangle playercol = new Rectangle((int)posplayer.X, (int)posplayer.Y, 85, 125);
            bg.getposplayer(posplayer);
            posplayer.Y += gravity;
            if(posplayer.Y> 380)
            {
                posplayer.Y = 380;
                foce = 7;


            }
            if (foce == 0)
            {
                jumpplayer = false;
            }
            if (jumpplayer == true)
            {
                count++;
                posplayer.Y -= foce;
                if (count == 15)
                {
                    foce -= 1;
                    count = 0;
                }
            }

            monster.Update(elapsed);
            lazer.Update(elapsed);
            bg.Update(elapsed);
            
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Begin();

            bg.Draw(spriteBatch);
            playerX.DrawFrame(spriteBatch, posplayer, direction);
            monster.Draw(spriteBatch);
           
            spriteBatch.DrawString(font, "jump : " + jumpplayer, new Vector2(200, 260), Color.Black);
            spriteBatch.DrawString(font, "g : " + gravity, new Vector2(200, 275), Color.Black);
            spriteBatch.DrawString(font, "foce : " + foce, new Vector2(200, 300), Color.Black);
            spriteBatch.DrawString(font, "count : " + count, new Vector2(200, 315), Color.Black);

            if (shoot == true)
            {
                lazer.Draw(spriteBatch);
            }
            
            //spriteBatch.End();

        }
        public void getposplayer(Vector2 getposplayer)
        {
            
        }


        public void move(int move,float move1)
        {
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Space))
            {
                lazer lazer1 = new lazer(posplayer);
                lazer.shoot(posplayer);
                shoot = true;
                //lazerlist.Add(lazer1);

            }
            if (ks.IsKeyDown(Keys.D))
            {
                
                playerX.Play();
                posplayer.X += 3;

                
                direction = 3;

            }
            else if (ks.IsKeyUp(Keys.D))
            {
                playerX.Pause();

            }



            if (ks.IsKeyDown(Keys.A))
            {
                playerX.Play();
                posplayer.X -= 3;
                
                direction = 2;



            }
            else if (ks.IsKeyUp(Keys.A))
            {
                playerX.Play();

            }
            if (ks.IsKeyUp(Keys.W)&&old_ks.IsKeyDown(Keys.W)&&jumpplayer==false)
            {
               
                playerX.Play();
                
                //power();



                jumpplayer = true;
                //direction = 4;


            }
            /*else if (ks.IsKeyUp(Keys.W))
            {
                foce += 25;

            }*/
            old_ks = ks;

        }
        
    }
}
