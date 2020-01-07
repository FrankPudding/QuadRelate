namespace QuadRelate.Types
{
    public class Score
    {
        public float PlayerOne { get; set; }
        public float PlayerTwo { get; set; }

        public Score ReverseScore()
        {
            return new Score
            {
                PlayerOne = PlayerTwo,
                PlayerTwo = PlayerOne
            };
        }

        public static Score operator+ (Score score1, Score score2)
        {
            return new Score
            {
                PlayerOne = score1.PlayerOne + score2.PlayerOne,
                PlayerTwo = score1.PlayerTwo + score2.PlayerTwo
            };
        }
    }
}
