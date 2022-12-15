using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CovidGame
{
    //global class to store the stage 
    public  struct NameScore
    {
       public string Name;
       public int Score;
    }
    public class Shared
    {
        public static Vector2 Stage;
        
        
        public static List<NameScore> HighScores = new List<NameScore>();
       

        public static void LoadFile()
        {
            string fileName = "highscore.txt";
            if (File.Exists(fileName))
            {
                // Read file using StreamReader. Reads file line by line  
                using (StreamReader file = new StreamReader(fileName))
                {
                    int count=0;
                    string ln;

                    while ((ln = file.ReadLine()) != null)
                    {
                        count++;
                        NameScore nameScore = new NameScore()
                        {
                            Name = $"The player {count}",
                            Score = int.Parse(ln)
                        };
                        HighScores.Add(nameScore);
                    }
                    file.Close();
                    
                }
            }

        }
        public static void AddHighScores(NameScore nameScore)
        {
           
            HighScores.Add(nameScore);
            string fileName = "highscore.txt";
            using (StreamWriter file = new StreamWriter(fileName, append: true))
            {
                file.WriteLine(nameScore.Score.ToString());
                file.Close();
            }
            HighScores.Sort((s1, s2) => s1.Score.CompareTo(s2.Score));
            HighScores.Reverse();
           
        }

        public static int Status = 0;
    }
}
