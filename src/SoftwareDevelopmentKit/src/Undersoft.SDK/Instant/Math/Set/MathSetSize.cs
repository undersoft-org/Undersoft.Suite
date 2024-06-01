namespace Undersoft.SDK.Instant.Math.Set
{
    using System;

    [Serializable]
    public class MathSetSize
    {
        public static MathSetSize Scalar = new MathSetSize(1, 1);
        public int cols;
        public int rows;

        public MathSetSize(int i, int j)
        {
            rows = i;
            cols = j;
        }

        public override bool Equals(object o)
        {
            if (o is MathSetSize)
                return (MathSetSize)o == this;
            return false;
        }

        public override int GetHashCode()
        {
            return rows * cols;
        }

        public override string ToString()
        {
            return "" + rows + " " + cols;
        }

        public static bool operator !=(MathSetSize o1, MathSetSize o2)
        {
            return o1.rows != o2.rows || o1.cols != o2.cols;
        }

        public static bool operator ==(MathSetSize o1, MathSetSize o2)
        {
            return o1.rows == o2.rows && o1.cols == o2.cols;
        }
    }
}
