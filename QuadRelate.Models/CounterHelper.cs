using QuadRelate.Types;

namespace QuadRelate.Models
{
    public static class CounterHelper
    {
        public static Counter Invert(this Counter colour)
        {
            if (colour == Counter.Empty)
                return colour;

            return (colour == Counter.Yellow) ? Counter.Red : Counter.Yellow;
        }
    }
}