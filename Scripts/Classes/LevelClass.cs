using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelClass : MonoBehaviour {

    public string[,] Level;
    int NumberOfLevels,GridSize;


    /// <summary>
    ///0 represent a blank space, 1 represent a grass block,
    ///3 represents the player
    ///2 represents a wall
    ///4 represents a Normal Enemy which begins moving left, 5 represents a Normal Enemy which begins moving right, 6 represents a Normal Enemy which begins moving up, , 7 represents a Normal Enemy which begins moving down
    ///8 represents a Sniper Enemy which begins moving left, 9 represents a Sniper Enemy which begins moving right, 10 represents a Sniper Enemy which begins moving up, , 11 represents a Sniper Enemy which begins moving down
    ///12 represents a Turret Enemy which begins moving left, 13 represents a Turret Enemy which begins moving right, 14 represents a Turret Enemy which begins moving up, , 15 represents a Turret Enemy which begins moving down
    /// </summary>






    void Awake()
    {
        GridSize = 10;
        NumberOfLevels = 7;
        Level = new string[NumberOfLevels, GridSize];


        for(int i=0;i< NumberOfLevels;i++)
        {
            for(int j=0; j<GridSize;j++)
            {
                if(i!=1 || i!=2 || i != 3 || i != 4 || i != 5)
                {
                    switch (j)
                    {
                        case 0: Level[i, j] = "1200000011"; break;
                        case 1: Level[i, j] = "1100000011"; break;
                        case 2: Level[i, j] = "1200000011"; break;
                        case 3: Level[i, j] = "1311161111"; break;
                        case 4: Level[i, j] = "1112111511"; break;
                        case 5: Level[i, j] = "1111121111"; break;
                        case 6: Level[i, j] = "1111711141"; break;
                        case 7: Level[i, j] = "1200000011"; break;
                        case 8: Level[i, j] = "1100000011"; break;
                        case 9: Level[i, j] = "1200000011"; break;
                    }
                }
               if (i == 1)
                {
                    switch (j)
                    {
                        case 0: Level[i, j] = "1111111111"; break;
                        case 1: Level[i, j] = "1111111111"; break;
                        case 2: Level[i, j] = "1110000011"; break;
                        case 3: Level[i, j] = "1112202211"; break;
                        case 4: Level[i, j] = "1111111114"; break;
                        case 5: Level[i, j] = "1111111111"; break;
                        case 6: Level[i, j] = "1112202211"; break;
                        case 7: Level[i, j] = "1310000011"; break;
                        case 8: Level[i, j] = "1111111111"; break;
                        case 9: Level[i, j] = "1111111111"; break;
                    }
                }

                if (i == 2)
                {
                    switch (j)
                    {
                        case 0: Level[i, j] = "1100000011"; break;
                        case 1: Level[i, j] = "1111111111"; break;
                        case 2: Level[i, j] = "1011221101"; break;
                        case 3: Level[i, j] = "1001111001"; break;
                        case 4: Level[i, j] = "1200110024"; break;
                        case 5: Level[i, j] = "1200110021"; break;
                        case 6: Level[i, j] = "1001111001"; break;
                        case 7: Level[i, j] = "1013221101"; break;
                        case 8: Level[i, j] = "1111111111"; break;
                        case 9: Level[i, j] = "1100000011"; break;
                    }
                }

                if (i == 3)
                {
                    switch (j)
                    {
                        case 0: Level[i, j] = "1111111111"; break;
                        case 1: Level[i, j] = "1311111111"; break;
                        case 2: Level[i, j] = "1111111111"; break;
                        case 3: Level[i, j] = "1111111111"; break;
                        case 4: Level[i, j] = "1111111114"; break;
                        case 5: Level[i, j] = "1111111111"; break;
                        case 6: Level[i, j] = "1111111111"; break;
                        case 7: Level[i, j] = "1111111111"; break;
                        case 8: Level[i, j] = "1111111111"; break;
                        case 9: Level[i, j] = "1111111111"; break;
                    }
                }

                if (i == 4)
                {
                    switch (j)
                    {
                        case 0: Level[i, j] = "101111 1101"; break;
                        case 1: Level[i, j] = "111200 2111"; break;
                        case 2: Level[i, j] = "111200 2111"; break;
                        case 3: Level[i, j] = "111111 1141"; break;
                        case 4: Level[i, j] = "301200 2101"; break;
                        case 5: Level[i, j] = "101200 2101"; break;
                        case 6: Level[i, j] = "111111 1111"; break;
                        case 7: Level[i, j] = "111200 2111"; break;
                        case 8: Level[i, j] = "111200 2111"; break;
                        case 9: Level[i, j] = "101111 1101"; break;
                    }
                }


                if (i == 5)
                {
                    switch (j)
                    {
                        case 0: Level[i, j] = "0000000000"; break;
                        case 1: Level[i, j] = "0311111110"; break;
                        case 2: Level[i, j] = "0101001010"; break;
                        case 3: Level[i, j] = "0101221010"; break;
                        case 4: Level[i, j] = "0111111110"; break;
                        case 5: Level[i, j] = "0111111110"; break;
                        case 6: Level[i, j] = "0101221010"; break;
                        case 7: Level[i, j] = "0101001010"; break;
                        case 8: Level[i, j] = "0111111140"; break;
                        case 9: Level[i, j] = "0000000000"; break;
                    }
                }

            }
        }
    }
}
