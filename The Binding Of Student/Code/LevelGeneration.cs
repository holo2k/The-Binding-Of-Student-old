using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.Intrinsics;
using System.Text;
using System.Threading.Tasks;

namespace The_Binding_Of_Student.Code
{
    public static class LevelGeneration
    {
        static Vector2 worldSize = new Vector2(4, 4);
        public static Room[,] rooms = new Room[9, 9];
        static List<Vector2> takenPositions = new List<Vector2>();
        static int gridSizeX, gridSizeY, numberOfRooms = 10;
        static Random rnd = new Random();
        static Direction dir;
        static int numberOfDoors = rnd.Next(1, 4);
        static int mapHeight = rooms.GetLength(1);
        static int mapWidth = rooms.GetLength(0);

        static public void Start()
        {
            rooms[4, 4] = new Room(new Vector2(4, 4), 1, rnd.Next(1,3));
            for (int i = 0; i < numberOfDoors; i++)
            {
                dir = (Direction)rnd.Next(0, 3);
                rooms[4, 4].doors.Add(new Door(dir));
            }
            rooms[4, 4].UpdateState();
            while (numberOfRooms > 0)
            {
                for (int i = 0; i < mapWidth; i++)
                {
                    if (numberOfRooms <= 0) break;

                    for (int j = 0; j < mapHeight; j++)
                    {
                        if (numberOfRooms <= 0) break;

                        if (rooms[i, j] != null && rooms[i, j].doorTop)
                        {
                            SetRoomDoors(i + 1, j, 0, 1, 2, Direction.Bottom);
                        }
                        if (rooms[i, j] != null && rooms[i, j].doorLeft)
                        {
                            SetRoomDoors(i, j - 1, 0, 1, 3, Direction.Right);
                        }
                        if (rooms[i, j] != null && rooms[i, j].doorRight)
                        {
                            SetRoomDoors(i, j + 1, 1, 2, 3, Direction.Left);
                        }
                        if (rooms[i, j] != null && rooms[i, j].doorBot)
                        {
                            SetRoomDoors(i - 1, j, 0, 2, 3, Direction.Top);
                        }
                    }
                }
            }
        }

        private static void SetRoomDoors(int i, int j, int firstDir, int secondDir, int thirdDir, Direction direction)
        {
            if (rooms[i , j] == null && mapWidth - 1 < i && mapHeight -1 < j)
            {
                rooms[i, j] = new Room(new Vector2(i, j), 2, rnd.Next(1, 3));
                rooms[i, j].doors.Add(new Door(direction));
                numberOfDoors = rnd.Next(1, 3);
                for (int t = 0; t < numberOfDoors; t++)
                {
                    dir = (Direction)new int[3] { firstDir, secondDir, thirdDir }[rnd.Next(0, 2)];
                    if(!rooms[i,j].doors.Contains(new Door(dir)))
                        rooms[i, j].doors.Add(new Door(dir)); 
                }
                rooms[i, j].UpdateState();
                numberOfRooms -= numberOfDoors;
            }
        }
    }
}
