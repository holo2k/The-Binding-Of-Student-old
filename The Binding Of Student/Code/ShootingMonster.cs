using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;

namespace The_Binding_Of_Student
{
    public class ShootingMonster : Monster
    {
        private bool isHitted = false;
        private double timeInvincible = 0d;
        private double timeToShoot = 0d;
        private double hittedTime = 0d;
        public static List<Shoot> bullets = new List<Shoot>();
        public static Vector2 currentPos = Vector2.Zero;
        static int currentTime = 0;
        public static Texture2D acid { get; set; }


        private static Vector2 GetPosForShoot => new Vector2(currentPos.X + 50, currentPos.Y+50);
        public ShootingMonster(Point _size, Vector2 position, Vector2 _mapPos)
        {
            monsterSize = _size;
            monsterPosition = position;
            mapPos = _mapPos;
            healthpoints = 70;
        }

        public override void Moving(GameTime gameTime, int speed)
        {
            if (IsInSameRoom())
            {
                if ((timeInvincible += gameTime.ElapsedGameTime.TotalSeconds) > 0.5d)
                {
                    for (int i = 0; i < Player.bullets.Count; i++)
                    {
                        Player.bullets[i].Update();
                        if (Collide(Player.bullets[i].Pos, Shoot.size))
                        {
                            Player.bullets.RemoveAt(i);
                            i--;
                            timeInvincible = 0.0d;
                            healthpoints -= 10;
                            isHitted = true;
                        }
                    }
                }

                currentPos = monsterPosition;

                if ((timeToShoot += gameTime.ElapsedGameTime.TotalSeconds) > 1.5d)
                {
                    timeToShoot = 0.0d;
                    MonsterFire();
                }

                for (int i = 0; i < bullets.Count; i++)
                {
                    bullets[i].Update();
                    if (bullets[i].Hidden)
                    {
                        bullets.RemoveAt(i);
                        i--;
                    }
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (isHitted)
            {
                DrawState(spriteBatch, hittedMonster2);
                if ((hittedTime += gameTime.ElapsedGameTime.TotalSeconds) > 0.5d)
                {
                    isHitted = false;
                    hittedTime = 0.0d;
                }
            }
            else
            {
                DrawState(spriteBatch, monster2);
            }

            foreach (Shoot bullet in bullets)
            {
                bullet.Draw();
                
            }
        }

        public void MonsterFire()
        {

            //if (IsPlayerInSection(0, 0, (int)currentPos.X, Game1.Height) && IsPlayerInSection(0, 0, Game1.Width, (int)currentPos.Y))
            //{
            //    bullets.Add(new Shoot(GetPosForShoot, Direction.Left, Direction.Up, acid, this));
            //}
            //else if (IsPlayerInSection((int)currentPos.X + 1, 0, Game1.Width, Game1.Height) && IsPlayerInSection(0, 0, Game1.Width, (int)currentPos.Y))
            //{
            //    bullets.Add(new Shoot(GetPosForShoot, Direction.Right, Direction.Up, acid, this));
            //}
            //else if (IsPlayerInSection(0, 0, (int)currentPos.X, Game1.Height) && IsPlayerInSection(0, (int)currentPos.Y + 1, Game1.Width, Game1.Height))
            //{
            //    bullets.Add(new Shoot(GetPosForShoot, Direction.Left, Direction.Down, acid, this));
            //}
            //else if (IsPlayerInSection((int)currentPos.X + 1, 0, Game1.Width, Game1.Height) && IsPlayerInSection(0, (int)currentPos.Y + 1, Game1.Width, Game1.Height))
            //{
            //    bullets.Add(new Shoot(GetPosForShoot, Direction.Right, Direction.Down, acid, this));
            
            bullets.Add(new Shoot(GetPosForShoot, Direction.None, acid, this));
            //if (IsPlayerInSection(0, 0, (int)currentPos.X, Game1.Height))
            //{
            //    bullets.Add(new Shoot(GetPosForShoot, Direction.Left, acid, this));
            //    Direction = Direction.Left;
            //}
            //else if (IsPlayerInSection((int)currentPos.X + 1, 0, Game1.Width, Game1.Height))
            //{
            //    bullets.Add(new Shoot(GetPosForShoot, Direction.Right, acid, this));
            //    Direction = Direction.Right;
            //}
            //
            //if (IsPlayerInSection(0, 0, Game1.Width, (int)currentPos.Y))
            //{
            //    bullets.Add(new Shoot(GetPosForShoot, Direction.Up, acid, this));
            //    Direction = Direction.Up;
            //}
            //else if (IsPlayerInSection(0, (int)currentPos.Y + 1, Game1.Width, Game1.Height))
            //{
            //    bullets.Add(new Shoot(GetPosForShoot, Direction.Down, acid, this));
            //    Direction = Direction.Down;
            //}

        }
    }
}
