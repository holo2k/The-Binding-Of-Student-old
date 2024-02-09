using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace The_Binding_Of_Student
{
    public class Shoot
    {
        public Vector2 Pos;
        Direction Dir;
        Direction Dir1;
        public static Point size = new Point(50,50);
        int speed = 10;
        Color color = Color.White;
        private object entity;
        public Vector2 playerPos;
        Vector2 direction;
        public Texture2D Texture2D;

        public Shoot(Vector2 Pos, Direction Dir, Texture2D texture, object _entity)
        {
            this.Pos = Pos;
            this.Dir = Dir;
            this.Texture2D = texture;
            entity = _entity;
            playerPos = Player.personPosition;
            direction = playerPos - Pos;
            direction.Normalize();
        }

        public bool Hidden
        {
            get
            {
                return (((Pos.X >= Game1.Width || Pos.Y >= Game1.Height) || Pos.X <= 0 || Pos.Y <= 0) || Pos == playerPos);
            }
        }

        public void Update()
        {
            if ((Pos.X <= Game1.Width || Pos.Y <= Game1.Height) && Pos.X >= 0 && Pos.Y >= 0 && entity is Player)
            {
                switch(Dir)
                {
                    case Direction.Left:
                        Pos.X -= speed;
                        break;
                    case Direction.Right:
                        Pos.X += speed;
                        break;
                    case Direction.Top:
                        Pos.Y -= speed;
                        break;
                    case Direction.Bottom:
                        Pos.Y += speed;
                        break;

                }
            }
            else if ((Pos.X <= Game1.Width || Pos.Y <= Game1.Height) && Pos.X >= 0 && Pos.Y >= 0 && entity is Monster)
            {
                
                Pos += direction * 4;           

            }
        }

        public void Draw()
        {
            if (entity is Player)
            {
                Player.SpriteBatch.Draw(Texture2D, Pos, color);
            }
            if(entity is Monster)
            {
               Monster.SpriteBatch.Draw(Texture2D, Pos, color);
            }        
        }
    }
}
