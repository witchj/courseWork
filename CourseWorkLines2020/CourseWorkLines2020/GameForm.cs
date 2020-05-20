using System;

namespace CourseWorkLines2020
{
    [Serializable] // Serialization allows to save the state of an object and recreate it as needed
    public class GameForm
    {
        public int[,] Field;
        public int Scores;
        public int HH, MM, SS;

        public GameForm(int[,] field, int scores, int hh, int mm, int ss)
        {
            Field = field;
            Scores = scores;
            HH = hh;
            MM = mm;
            SS = ss;
        }

        public static GameForm GetDefault()
        {
            return new GameForm(Algorithms.CreateNewFieldMatrix(), 0, 0, 0, 0);
        }
    }
}