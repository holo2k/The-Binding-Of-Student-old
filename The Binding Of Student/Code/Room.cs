using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using The_Binding_Of_Student.Code;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace The_Binding_Of_Student
{
    public class Room
    {

        public static Texture2D RoomBackground { get; set; }
        public Vector2 mapPos;
        public int type;
        public bool doorTop = false, doorBot = false, doorLeft = false, doorRight  = false;
          
        static Vector2 playerPos = new Vector2(550, 200);
        static Vector2 monsterPos;
        static Vector2 tearPos = new Vector2(370, 400);
        static Vector2 acidPos = new Vector2(1920 / 3, 900);

        public static Point btnPlaySize { get; set; }
        public static Point btnExitSize { get; set; }
        public static Point btnSettingsSize { get; set; }

        public List<Door> doors = new List<Door>();
        public List<Monster> monsters = new List<Monster>();
        Random rnd = new Random();

        public Room(Vector2 _mapPos, int _type, int monstersCount)
        {
            
            for (int i = 0; i < monstersCount; i++)
            {

            }
            //monsters.AddRange(new[]
            //{ new Monster (new Point(76, 100),
            //               new Vector2(100,200),
            //               _mapPos),
            //  new ShootingMonster(
            //               new Point(100, 90),
            //               new Vector2(650,500),
            //               _mapPos),
            //  new Monster()
            //});
            mapPos = _mapPos;
            type = _type;
        }

        //public Room(int numberOfDoors, Direction[] dir, Door lastDoor)
        //{
        //    for (int i = 0; i < numberOfDoors; i++)
        //    {
        //        doors.Add(new Door(dir[i]));
        //    }
        //}

        public void Update()
        {
            foreach (var door in doors)
            {
                door.Update();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(RoomBackground, Vector2.Zero, Color.FromNonPremultiplied(255, 255, 255, 256));
            foreach (var door in doors)
            {
                door.Draw(spriteBatch);
            }
        }  
        
        public void UpdateState()
        {
            foreach (var door in doors)
            {
                switch (door.Dir)
                {
                    case Direction.Top:
                        doorTop = true;
                        break;
                    case Direction.Bottom:
                        doorBot = true;
                        break;
                    case Direction.Left:
                        doorLeft = true;
                        break;
                    case Direction.Right:
                        doorRight = true;
                        break;
                }
            }
        }
    }

    public class Door
    {
        bool IsOpen() => Game1.monsters.Count == 0;
        public Vector2 Pos;
        public Point size;
        public Direction Dir;
        public Texture2D texture;
        public static Texture2D DoorUp { get; set; }
        public static Texture2D DoorDown { get; set; }
        public static Texture2D DoorLeft { get; set; } 
        public static Texture2D DoorRight { get; set; }

        KeyboardState keyboardState, oldKeyboardState;

        Random rnd = new Random();
        public Door(Direction dir)
        {
            Dir = dir;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
            switch (Dir)
            {
                case Direction.Top:
                    Pos = new Vector2(850, 0);
                    size = new Point(166, 186);
                    spriteBatch.Draw(DoorUp, Pos, Color.FromNonPremultiplied(255, 255, 255, 256));
                    break;
                case Direction.Bottom:
                    Pos = new Vector2(850, 900);
                    size = new Point(166, 180);
                    spriteBatch.Draw(DoorDown, Pos, Color.FromNonPremultiplied(255, 255, 255, 256));
                    break;
                case Direction.Left:
                    Pos = new Vector2(0, 450);
                    size = new Point(179, 165);
                    spriteBatch.Draw(DoorLeft, Pos, Color.FromNonPremultiplied(255, 255, 255, 256));
                    break;
                case Direction.Right:
                    Pos = new Vector2(1730, 450);
                    size = new Point(181, 167);
                    spriteBatch.Draw(DoorRight, Pos, Color.FromNonPremultiplied(255, 255, 255, 256));
                    break;
            }
        }

        public void Update()
        {
            keyboardState = Keyboard.GetState();
            if (IsOpen() && 
               (Player.Collide(Pos, size) || 
               (Player.Collide(new Vector2(Pos.X, Pos.Y - 100), size) && Dir == Direction.Bottom)) &&
               keyboardState.IsKeyDown(Keys.Space) && 
               oldKeyboardState.IsKeyUp(Keys.Space))
            {
                int x;
                int y;
                switch (Dir)
                {
                    case Direction.Top:
                        x = (int)Game1.currentRoom.mapPos.X + 1;
                        y = (int)Game1.currentRoom.mapPos.Y;
                        Game1.currentRoom = LevelGeneration.rooms[x,y];
                        ChangePlayerPos();
                        Player.playerMapPos[x, y] = 1;
                        break;
                    case Direction.Bottom:
                        x = (int)Game1.currentRoom.mapPos.X - 1;
                        y = (int)Game1.currentRoom.mapPos.Y;
                        Game1.currentRoom = LevelGeneration.rooms[x, y];
                        ChangePlayerPos();
                        Player.playerMapPos[x, y] = 1;
                        break;
                    case Direction.Left:
                        x = (int)Game1.currentRoom.mapPos.X;
                        y = (int)Game1.currentRoom.mapPos.Y - 1;
                        Game1.currentRoom = LevelGeneration.rooms[x, y];
                        ChangePlayerPos();
                        Player.playerMapPos[x, y] = 1;
                        break;
                    case Direction.Right:
                        x = (int)Game1.currentRoom.mapPos.X;
                        y = (int)Game1.currentRoom.mapPos.Y + 1;
                        Game1.currentRoom = LevelGeneration.rooms[x, y];
                        ChangePlayerPos();
                        Player.playerMapPos[x, y] = 1;
                        break;
                }
            }
            oldKeyboardState = keyboardState;
        }

        public void ChangePlayerPos()
        {
            float x = Player.personPosition.X + 200;
            if (Dir != Direction.Top && Dir != Direction.Bottom) x *= 2;
            float y = Player.personPosition.Y * 2;
            Player.personPosition = new Vector2(Game1.Width - x, Game1.Height - y);
            Array.Clear(Player.playerMapPos, 0, Player.playerMapPos.Length);
        }

    }
}
