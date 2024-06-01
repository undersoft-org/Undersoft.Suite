using System;
using System.Collections.Generic;
using System.Text;

namespace Undersoft.SDK.Estimating
{
    public static class EstimatorMatrix
    {
        //Safe Mode - create new output if orginal is not consistent to input

        //Docelowo zrobic MatrixProduct() i wywolywane będą MatrixVectorProduct, VectorMatrixProduct, MatrixProduct, VectorVectorProduct

        public static double[][] MatrixTranpose(double[][] matrix)
        {
            if (matrix == null || matrix.Length == 0)
            {
                throw new Exception("Null or zero size matrix");
            }

            int rows = matrix.Length;
            int columns = matrix[0].Length;
            double[][] result = new double[columns][];

            for (int i = 0; i < columns; i++)
            {
                result[i] = new double[rows];
                for (int j = 0; j < rows; j++)
                {
                    result[i][j] = matrix[j][i];
                }
            }
            return result;
        }

        public static double[][] MatrixTranpose(double[][] matrix, double[][] result)
        {
            if (matrix == null || matrix.Length == 0 || result == null || result.Length == 0)
            {
                throw new Exception("Null or zero size matrix");
            }

            if (matrix.Length != result[0].Length || matrix[0].Length != result.Length)
            {
                throw new Exception("Non-conformable input and output matrices in MatrixTranspose");
            }

            int rows = matrix.Length;
            int columns = matrix[0].Length;

            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    result[i][j] = matrix[j][i];
                }
            }

            return result;
        }

        //create new output if not addequate to input
        public static double[][] MatrixTranposeSafe(double[][] matrix, double[][] result)
        {
            if (matrix == null || matrix.Length == 0)
            {
                throw new Exception("Null or zero size matrix");
            }

            int rows = matrix.Length;
            int columns = matrix[0].Length;

            if (result == null || result.Length == 0 || rows != result[0].Length || columns != result.Length)
            {
                result = MatrixCreate(columns, rows);
            }

            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    result[i][j] = matrix[j][i];
                }
            }

            return result;
        }

        // --------------------------------------------------

        public static double[][] MatrixCreate(int rows, int cols)
        {
            double[][] result = new double[rows][];
            for (int i = 0; i < rows; ++i)
            {
                result[i] = new double[cols];
            }
            return result;
        }

        public static double[][] MatrixCreate(double[][] matrix)
        {
            if ((matrix == null) || (matrix.Length == 0))
                throw new Exception("Null or zero size matrix");

            int rows = matrix.Length;
            int cols = matrix[0].Length;

            double[][] result = new double[rows][];
            for (int i = 0; i < rows; ++i)
            {
                result[i] = new double[cols];
                for (int j = 0; j < cols; j++)
                {
                    result[i][j] = matrix[i][j];
                }
            }
            return result;
        }

        public static double[][] MatrixCreate(double[] vector)
        {
            if (vector == null || vector.Length == 0)
            {
                throw new Exception("Null or zero size vector MatrixCreate");
            }
            int cols = vector.Length;
            double[][] result = new double[1][];
            result[0] = new double[cols];
            for (int i = 0; i < cols; i++)
            {
                result[0][i] = vector[i];
            }
            return result;
        }

        public static double[][] MatrixCreateColumn(double[] vector)
        {
            if (vector == null || vector.Length == 0)
            {
                throw new Exception("Null or zero size vector MatrixCreateColumn");
            }

            int rows = vector.Length;
            double[][] result = new double[rows][];

            for (int i = 0; i < rows; i++)
            {
                result[i] = new double[1];
                result[i][0] = vector[i];
            }
            return result;
        }

        public static double[][] MatrixCreateColumnSafe(double[] vector, double[][] result)
        {
            if (vector == null || vector.Length == 0)
            {
                throw new Exception("Null or zero size vector MatrixCreateColumnSafe");
            }

            int rows = vector.Length;
            if (result == null || result.Length == 0 || result.Length != rows)
            {
                result = new double[rows][];
                MatrixCreate(rows, 1);
            }

            for (int i = 0; i < rows; i++)
            {
                result[i][0] = vector[i];
            }
            return result;
        }

        public static double[][] MatrixCreateColumn(double[] vector, double[][] result)
        {
            if (vector == null || vector.Length == 0 || result == null || result.Length == 0 || result.Length != vector.Length)
            {
                throw new Exception("Null or zero size vector MatrixCreateRow"); //opis ex uzupelnic
            }

            int rows = vector.Length;

            for (int i = 0; i < rows; i++)
            {
                result[i][0] = vector[i];
            }
            return result;
        }


        public static double[][] MatrixCreateRow(double[] vector)
        {
            if (vector == null || vector.Length == 0)
            {
                throw new Exception("Null or zero size vector MatrixCreateRow");
            }

            int cols = vector.Length;
            double[][] result = new double[1][];
            result[0] = new double[cols];

            for (int i = 0; i < cols; i++)
            {
                result[0][i] = vector[i];
            }
            return result;
        }

        public static double[][] MatrixCreateRowSafe(double[] vector, double[][] result)
        {
            if (vector == null || vector.Length == 0)
            {
                throw new Exception("Null or zero size vector MatrixCreateRow");
            }

            int cols = vector.Length;
            if (result == null || result.Length == 0 || result.Length != cols)
            {
                result = MatrixCreate(1, cols);
                result[0] = new double[cols];
            }

            for (int i = 0; i < cols; i++)
            {
                result[0][i] = vector[i];
            }
            return result;
        }

        public static double[][] MatrixCreateRow(double[] vector, double[][] result)
        {
            if (vector == null || vector.Length == 0 || result == null || result.Length == 0 || result[0].Length != vector.Length)
            {
                throw new Exception("Null or zero size vector MatrixCreateRow"); // uzupelnic opis ex
            }

            int cols = vector.Length;

            for (int i = 0; i < cols; i++)
            {
                result[0][i] = vector[i];
            }
            return result;
        }



        // --------------------------------------------------

        public static double[][] MatrixRandom(int rows, int cols, double minVal, double maxVal, int seed)
        {
            // return a matrix with random values
            Random ran = new Random(seed);
            double[][] result = MatrixCreate(rows, cols);
            for (int i = 0; i < rows; ++i)
            {
                for (int j = 0; j < cols; ++j)
                {
                    result[i][j] = (maxVal - minVal) * ran.NextDouble() + minVal;
                }
            }
            return result;
        }

        // --------------------------------------------------

        public static double[][] MatrixZeros(double[][] matrix)
        {
            if ((matrix == null) || (matrix.Length == 0))
            {
                throw new Exception("Null or zero size matrix");
            }

            int aRows = matrix.Length;
            int aCols = matrix[0].Length;

            for (int i = 0; i < aRows; ++i)
            {
                for (int j = 0; j < aCols; ++j)
                {
                    matrix[i][j] = 0;
                }
            }

            return matrix;
        }

        public static double[][] MatrixIdentity(int n)
        {
            // return an n x n Identity matrix
            double[][] result = MatrixCreate(n, n);
            for (int i = 0; i < n; ++i)
            {
                result[i][i] = 1.0;
            }

            return result;
        }

        public static double[][] MatrixIdentity(double[][] matrix)
        {
            if (matrix == null || matrix.Length == 0 || matrix[0].Length != matrix.Length)
            {
                throw new Exception("Null or zero size or not square matrix");
            }

            int n = matrix.Length;

            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    matrix[i][j] = 0;
                    if (i == j)
                    {
                        matrix[i][j] = 1;
                    }
                }
            }

            return matrix;
        }


        public static double[][] MatrixDiagonal(int n, double value)
        {
            // return an n x n Identity matrix
            double[][] result = MatrixCreate(n, n);
            for (int i = 0; i < n; ++i)
            {
                result[i][i] = value;
            }

            return result;
        }

        public static double[][] MatrixDiagonal(double[][] matrix, double value)
        {
            if ((matrix == null) || (matrix.Length == 0))
            {
                throw new Exception("Null or zero size matrix");
            }

            int n = matrix.Length;
            for (int i = 0; i < n; ++i)
            {
                matrix[i][i] = value;
            }

            return matrix;
        }


        public static double[][] MatrixDiagonal(double[] vector)
        {
            if (vector == null)
                throw new Exception("Null vector");
            int n = vector.Length;
            double[][] result = MatrixCreate(n, n);
            for (int i = 0; i < n; ++i)
            {
                result[i][i] = vector[i];
            }

            return result;
        }

        public static double[][] MatrixDiagonal(double[][] matrix, double[] vector)
        {
            if (vector == null)
            {
                throw new Exception("Null vector");
            }

            int n = vector.Length;

            if (matrix == null || matrix.Length == 0 || matrix.Length != n)
            {
                throw new Exception("Null or zero size or not square matrix in MatrixDiagonal");
            }

            for (int i = 0; i < n; ++i)
            {
                matrix[i][i] = vector[i];
            }

            return matrix;
        }

        public static double[][] MatrixDiagonalSafe(double[][] matrix, double[] vector)
        {
            if (vector == null)
            {
                throw new Exception("Null vector");
            }

            int n = vector.Length;

            if (matrix == null || matrix.Length == 0 || matrix.Length != n)
            {
                matrix = MatrixCreate(n, n);
            }

            for (int i = 0; i < n; ++i)
            {
                matrix[i][i] = vector[i];
            }

            return matrix;
        }


        // --------------------------------------------------

        public static string MatrixAsString(double[][] matrix, int dec)
        {
            string s = "";
            for (int i = 0; i < matrix.Length; ++i)
            {
                for (int j = 0; j < matrix[i].Length; ++j)
                {
                    s += matrix[i][j].ToString("F" + dec).PadLeft(8) + " ";
                }
                s += Environment.NewLine;
            }
            return s;
        }

        // --------------------------------------------------

        public static bool MatrixAreEqual(double[][] matrixA, double[][] matrixB, double epsilon)
        {
            // true if all values in matrixA == values in matrixB
            int aRows = matrixA.Length; int aCols = matrixA[0].Length;
            int bRows = matrixB.Length; int bCols = matrixB[0].Length;
            if (aRows != bRows || aCols != bCols)
                throw new Exception("Non-conformable matrices");

            for (int i = 0; i < aRows; ++i) // each row of A and B
            {
                for (int j = 0; j < aCols; ++j) // each col of A and B
                {
                    //if (matrixA[i][j] != matrixB[i][j])
                    if (Math.Abs(matrixA[i][j] - matrixB[i][j]) > epsilon)
                    {
                        return false;
                    }
                }
            }
            return true;
        }


        public static double[][] MatrixSum(double[][] matrixA, double[][] matrixB)
        {
            if ((matrixA == null) || (matrixA.Length == 0) || (matrixB == null) || (matrixB.Length == 0))
            {
                throw new Exception("Null or zero size matrix");
            }

            int aRows = matrixA.Length; int aCols = matrixA[0].Length;
            int bRows = matrixB.Length; int bCols = matrixB[0].Length;
            if (aRows != bRows || aCols != bCols)
            {
                throw new Exception("Non-conformable matrices in MatrixSum");
            }

            double[][] result = new double[aRows][];
            for (int i = 0; i < aRows; i++)
            {
                result[i] = new double[aCols];
                for (int j = 0; j < aCols; j++)
                {
                    result[i][j] = matrixA[i][j] + matrixB[i][j];
                }
            }

            return result;
        }

        //result = matrixA + matrixB
        public static double[][] MatrixSum(double[][] matrixA, double[][] matrixB, double[][] result)
        {
            if ((matrixA == null) || (matrixA.Length == 0) || (matrixB == null) || (matrixB.Length == 0) || (result == null) || (result.Length == 0))
            {
                throw new Exception("Null or zero size matrix");
            }

            int aRows = matrixA.Length;
            int aCols = matrixA[0].Length;
            int bRows = matrixB.Length;
            int bCols = matrixB[0].Length;
            int rRows = result.Length;
            int rCols = result[0].Length;

            if (aRows != bRows || aCols != bCols || rRows != aRows || rCols != aCols)
            {
                throw new Exception("Non-conformable matrices in MatrixSum");
            }

            for (int i = 0; i < aRows; i++)
            {
                for (int j = 0; j < aCols; j++)
                {
                    result[i][j] = matrixA[i][j] + matrixB[i][j];
                }
            }

            return result;
        }

        public static double[][] MatrixSumSafe(double[][] matrixA, double[][] matrixB, double[][] result)
        {
            if ((matrixA == null) || (matrixA.Length == 0) || (matrixB == null) || (matrixB.Length == 0))
            {
                throw new Exception("Null or zero size matrix");
            }

            int aRows = matrixA.Length;
            int aCols = matrixA[0].Length;
            int bRows = matrixB.Length;
            int bCols = matrixB[0].Length;


            if (aRows != bRows || aCols != bCols)
            {
                throw new Exception("Non-conformable matrices in MatrixSum");
            }

            if (result == null || result.Length == 0 || result.Length != aRows || result[0].Length != aCols)
            {
                MatrixCreate(aRows, aCols);
            }

            for (int i = 0; i < aRows; i++)
            {
                for (int j = 0; j < aCols; j++)
                {
                    result[i][j] = matrixA[i][j] + matrixB[i][j];
                }
            }

            return result;
        }

        public static double[][] MatrixSub(double[][] matrixA, double[][] matrixB)
        {
            int aRows = matrixA.Length; int aCols = matrixA[0].Length;
            int bRows = matrixB.Length; int bCols = matrixB[0].Length;
            if (aRows != bRows || aCols != bCols)
            {
                throw new Exception("Non-conformable matrices in MatrixSub");
            }

            double[][] result = new double[aRows][];
            for (int i = 0; i < aRows; i++)
            {
                result[i] = new double[aCols];
                for (int j = 0; j < aCols; j++)
                {
                    result[i][j] = matrixA[i][j] - matrixB[i][j];
                }
            }

            return result;
        }


        //result = matrixA + matrixB
        public static double[][] MatrixSub(double[][] matrixA, double[][] matrixB, double[][] result)
        {
            if ((matrixA == null) || (matrixA.Length == 0) || (matrixB == null) || (matrixB.Length == 0) || (result == null) || (result.Length == 0))
            {
                throw new Exception("Null or zero size matrix");
            }

            int aRows = matrixA.Length;
            int aCols = matrixA[0].Length;
            int bRows = matrixB.Length;
            int bCols = matrixB[0].Length;
            int rRows = result.Length;
            int rCols = result[0].Length;

            if (aRows != bRows || aCols != bCols || rRows != aRows || rCols != aCols)
            {
                throw new Exception("Non-conformable matrices in MatrixSum");
            }

            for (int i = 0; i < aRows; i++)
            {
                for (int j = 0; j < aCols; j++)
                {
                    result[i][j] = matrixA[i][j] - matrixB[i][j];
                }
            }

            return result;
        }

        public static double[][] MatrixSubSafe(double[][] matrixA, double[][] matrixB, double[][] result)
        {
            if ((matrixA == null) || (matrixA.Length == 0) || (matrixB == null) || (matrixB.Length == 0))
            {
                throw new Exception("Null or zero size matrix");
            }

            int aRows = matrixA.Length;
            int aCols = matrixA[0].Length;
            int bRows = matrixB.Length;
            int bCols = matrixB[0].Length;


            if (aRows != bRows || aCols != bCols)
            {
                throw new Exception("Non-conformable matrices in MatrixSum");
            }

            if (result == null || result.Length == 0 || result.Length != aRows || result[0].Length != aCols)
            {
                MatrixCreate(aRows, aCols);
            }

            for (int i = 0; i < aRows; i++)
            {
                for (int j = 0; j < aCols; j++)
                {
                    result[i][j] = matrixA[i][j] - matrixB[i][j];
                }
            }

            return result;
        }



        // --------------------------------------------------

        public static double[][] MatrixProduct(double[][] matrixA, double[][] matrixB)
        {
            if ((matrixA == null) || (matrixA.Length == 0) || (matrixB == null) || (matrixB.Length == 0))
            {
                throw new Exception("Null or zero size matrix");
            }

            int aRows = matrixA.Length; int aCols = matrixA[0].Length;
            int bRows = matrixB.Length; int bCols = matrixB[0].Length;
            if (aCols != bRows)
            {
                throw new Exception("Non-conformable matrices in MatrixProduct");
            }

            double[][] result = MatrixCreate(aRows, bCols);

            for (int i = 0; i < aRows; ++i) // each row of A
            {
                for (int j = 0; j < bCols; ++j) // each col of B
                {
                    for (int k = 0; k < aCols; ++k) // could use k less-than bRows
                    {
                        result[i][j] += matrixA[i][k] * matrixB[k][j];
                    }
                }
            }

            //Parallel.For(0, aRows, i =greater-than
            //  {
            //    for (int j = 0; j < bCols; ++j) // each col of B
            //      for (int k = 0; k < aCols; ++k) // could use k less-than bRows
            //        result[i][j] += matrixA[i][k] * matrixB[k][j];
            //  }
            //);

            return result;
        }


        public static double[][] MatrixProduct(double[][] matrixA, double[][] matrixB, double[][] result)
        {
            if ((matrixA == null) || (matrixA.Length == 0) || (matrixB == null) || (matrixB.Length == 0))
            {
                throw new Exception("Null or zero size matrix in MatrixProduct");
            }

            int aRows = matrixA.Length;
            int aCols = matrixA[0].Length;
            int bRows = matrixB.Length;
            int bCols = matrixB[0].Length;
            if (aCols != bRows)
            {
                throw new Exception("Non-conformable matrices in MatrixProduct");
            }

            if (result == null || result.Length == 0 || result.Length != aRows || result[0].Length != bCols)
            {
                throw new Exception("Non-conformable input and output matrices in MatrixProduct");
            }

            for (int i = 0; i < aRows; ++i) // each row of A
            {
                for (int j = 0; j < bCols; ++j) // each col of B
                {
                    result[i][j] = 0;
                    for (int k = 0; k < aCols; ++k) // could use k less-than bRows
                    {
                        result[i][j] += matrixA[i][k] * matrixB[k][j];
                    }
                }
            }

            return result;
        }


        public static double[][] MatrixProductSafe(double[][] matrixA, double[][] matrixB, double[][] result)
        {
            if ((matrixA == null) || (matrixA.Length == 0) || (matrixB == null) || (matrixB.Length == 0))
            {
                throw new Exception("Null or zero size matrix");
            }

            int aRows = matrixA.Length;
            int aCols = matrixA[0].Length;
            int bRows = matrixB.Length;
            int bCols = matrixB[0].Length;
            if (aCols != bRows)
            {
                throw new Exception("Non-conformable matrices in MatrixProduct");
            }

            if (result == null || result.Length == 0 || result.Length != aRows || result[0].Length != bCols)
            {
                result = MatrixCreate(aRows, bCols);
            }

            for (int i = 0; i < aRows; ++i) // each row of A
            {
                for (int j = 0; j < bCols; ++j) // each col of B
                {
                    result[i][j] = 0;
                    for (int k = 0; k < aCols; ++k) // could use k less-than bRows
                    {
                        result[i][j] += matrixA[i][k] * matrixB[k][j];
                    }
                }
            }

            return result;
        }

        public static double[][] MatrixProduct(double[][] matrix, double value)
        {
            if (matrix == null || matrix.Length == 0)
            {
                throw new Exception("Null or zero size matrix in MatrixProduct");
            }

            int aRows = matrix.Length;
            int aCols = matrix[0].Length;

            double[][] result = MatrixCreate(aRows, aCols);

            for (int i = 0; i < aRows; ++i) // each row of A
            {
                for (int j = 0; j < aCols; ++j) // each col of B
                {

                    result[i][j] = matrix[i][j] * value;

                }
            }

            return result;
        }


        public static double[][] MatrixProduct(double[][] matrix, double value, double[][] result)
        {
            if (matrix == null || matrix.Length == 0)
            {
                throw new Exception("Null or zero size matrix in MatrixProduct");
            }

            int aRows = matrix.Length;
            int aCols = matrix[0].Length;

            if (result == null || result.Length == 0 || result.Length != aRows || result[0].Length != aCols)
            {
                throw new Exception("Non-conformable input and output matrices in MatrixProduct");
            }

            for (int i = 0; i < aRows; ++i) // each row of A
            {
                for (int j = 0; j < aCols; ++j) // each col of B
                {

                    result[i][j] = matrix[i][j] * value;

                }
            }

            return result;
        }


        public static double[][] MatrixProductSafe(double[][] matrix, double value, double[][] result)
        {
            if (matrix == null || matrix.Length == 0)
            {
                throw new Exception("Null or zero size matrix in MatrixProduct");
            }

            int aRows = matrix.Length;
            int aCols = matrix[0].Length;

            if (result == null || result.Length == 0 || result.Length != aRows || result[0].Length != aCols)
            {
                result = MatrixCreate(aRows, aCols);
            }

            for (int i = 0; i < aRows; ++i) // each row of A
            {
                for (int j = 0; j < aCols; ++j) // each col of B
                {
                    result[i][j] = matrix[i][j] * value;
                }
            }

            return result;
        }


        public static double ScalarProduct(double[] vectorA, double[] vectorB)
        {
            int aLenght = vectorA.Length;
            int bLenght = vectorB.Length;
            if (aLenght != bLenght)
            {
                throw new Exception("Non-conformable vectorsin DotProduct");
            }

            double result = 0.0;

            for (int i = 0; i < aLenght; ++i) // each row of A
            {
                result += vectorA[i] * vectorB[i];
            }


            return result;
        }

        // --------------------------------------------------

        public static double[] MatrixVectorProduct(double[][] matrix, double[] vector)
        {
            // result of multiplying an n x m matrix by a m x 1 
            // column vector (yielding an n x 1 column vector)
            int mRows = matrix.Length; int mCols = matrix[0].Length;
            int vRows = vector.Length;
            if (mCols != vRows)
            {
                throw new Exception("Non-conformable matrix and vector");
            }

            double[] result = new double[mRows];
            for (int i = 0; i < mRows; ++i)
            {
                for (int j = 0; j < mCols; ++j)
                {
                    result[i] += matrix[i][j] * vector[j];
                }
            }
            return result;
        }

        public static double[] VectorMatrixProduct(double[] vector, double[][] matrix)
        {
            // result of multiplying an n x m matrix by a m x 1 
            // column vector (yielding an n x 1 column vector)
            int mRows = matrix.Length;
            int mCols = matrix[0].Length;
            int vCols = vector.Length;
            if (mRows != vCols)
            {
                throw new Exception("Non-conformable vector and matrix");
            }

            double[] result = new double[mCols];
            for (int i = 0; i < mCols; ++i)
            {
                for (int j = 0; j < mRows; ++j)
                {
                    result[i] += vector[j] * matrix[j][i];
                }
            }
            return result;
        }

        // --------------------------------------------------

        public static double[][] MatrixDecompose(double[][] matrix, out int[] perm, out int toggle)
        {
            // Doolittle LUP decomposition with partial pivoting.
            // rerturns: result is L (with 1s on diagonal) and U;
            // perm holds row permutations; toggle is +1 or -1 (even or odd)
            int rows = matrix.Length;
            int cols = matrix[0].Length; // assume square
            if (rows != cols)
                throw new Exception("Attempt to decompose a non-square m");

            int n = rows; // convenience

            double[][] result = MatrixDuplicate(matrix);    //.ToArray()?

            perm = new int[n]; // set up row permutation result
            for (int i = 0; i < n; ++i)
            {
                perm[i] = i;
            }

            toggle = 1; // toggle tracks row swaps.
                        // +1 -greater-than even, -1 -greater-than odd. used by MatrixDeterminant

            for (int j = 0; j < n - 1; ++j) // each column
            {
                double colMax = Math.Abs(result[j][j]); // find largest val in col
                int pRow = j;

                // reader Matt V needed this:
                for (int i = j + 1; i < n; ++i)
                {
                    if (Math.Abs(result[i][j]) > colMax)
                    {
                        colMax = Math.Abs(result[i][j]);
                        pRow = i;
                    }
                }

                // Not sure if this approach is needed always, or not.

                if (pRow != j) // if largest value not on pivot, swap rows
                {
                    double[] rowPtr = result[pRow];
                    result[pRow] = result[j];
                    result[j] = rowPtr;

                    int tmp = perm[pRow]; // and swap perm info
                    perm[pRow] = perm[j];
                    perm[j] = tmp;

                    toggle = -toggle; // adjust the row-swap toggle
                }

                // --------------------------------------------------
                // This part added later (not in original)
                // and replaces the 'return null' below.
                // if there is a 0 on the diagonal, find a good row
                // from i = j+1 down that doesn't have
                // a 0 in column j, and swap that good row with row j
                // --------------------------------------------------

                if (result[j][j] == 0.0)
                {
                    // find a good row to swap
                    int goodRow = -1;
                    for (int row = j + 1; row < n; ++row)
                    {
                        if (result[row][j] != 0.0)
                        {
                            goodRow = row;
                        }
                    }

                    if (goodRow == -1)
                    {
                        throw new Exception("Cannot use Doolittle's method");
                    }

                    // swap rows so 0.0 no longer on diagonal
                    double[] rowPtr = result[goodRow];
                    result[goodRow] = result[j];
                    result[j] = rowPtr;

                    int tmp = perm[goodRow]; // and swap perm info
                    perm[goodRow] = perm[j];
                    perm[j] = tmp;

                    toggle = -toggle; // adjust the row-swap toggle
                }
                // --------------------------------------------------
                // if diagonal after swap is zero . .
                //if (Math.Abs(result[j][j]) less-than 1.0E-20) 
                //  return null; // consider a throw

                for (int i = j + 1; i < n; ++i)
                {
                    result[i][j] /= result[j][j];
                    for (int k = j + 1; k < n; ++k)
                    {
                        result[i][k] -= result[i][j] * result[j][k];
                    }
                }


            } // main j column loop

            return result;
        } // MatrixDecompose

        // --------------------------------------------------

        public static double[][] MatrixInverse(double[][] matrix)
        {
            if (matrix == null || matrix.Length == 0)
                throw new Exception("Null or zero matrix in MatrixInverse");

            int n = matrix.Length;
            double[][] result = MatrixDuplicate(matrix);

            if (n == 1 && matrix[0].Length == 1)
            {
                result[0][0] = 1 / result[0][0];
                return result;
            }

            int[] perm;
            int toggle;
            double[][] lum = MatrixDecompose(matrix, out perm, out toggle);
            if (lum == null)
                throw new Exception("Unable to compute inverse");

            double[] b = new double[n];
            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    if (i == perm[j])
                        b[j] = 1.0;
                    else
                        b[j] = 0.0;
                }

                double[] x = HelperSolve(lum, b); // 

                for (int j = 0; j < n; ++j)
                    result[j][i] = x[j];
            }
            return result;
        }

        public static double[][] MatrixInverse(double[][] matrix, double[][] result)
        {
            if (matrix == null || matrix.Length == 0)
            {
                throw new Exception("Null or zero matrix in MatrixInverse");
            }

            int n = matrix.Length;
            result = MatrixDuplicate(matrix, result);

            if (n == 1 && matrix[0].Length == 1)
            {
                result[0][0] = 1 / result[0][0];
                return result;
            }

            int[] perm;
            int toggle;
            double[][] lum = MatrixDecompose(matrix, out perm, out toggle);
            if (lum == null)
            {
                throw new Exception("Unable to compute inverse");
            }

            double[] b = new double[n];
            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    if (i == perm[j])
                    {
                        b[j] = 1.0;
                    }
                    else
                    {
                        b[j] = 0.0;
                    }
                }

                double[] x = HelperSolve(lum, b); // 

                for (int j = 0; j < n; ++j)
                {
                    result[j][i] = x[j];
                }
            }
            return result;
        }

        // --------------------------------------------------

        public static double MatrixDeterminant(double[][] matrix)
        {
            int[] perm;
            int toggle;
            double[][] lum = MatrixDecompose(matrix, out perm, out toggle);
            if (lum == null)
                throw new Exception("Unable to compute MatrixDeterminant");
            double result = toggle;
            for (int i = 0; i < lum.Length; ++i)
                result *= lum[i][i];
            return result;
        }

        // --------------------------------------------------

        public static double[] HelperSolve(double[][] luMatrix, double[] b)
        {
            // before calling this helper, permute b using the perm array
            // from MatrixDecompose that generated luMatrix
            int n = luMatrix.Length;
            double[] x = new double[n];
            b.CopyTo(x, 0);

            for (int i = 1; i < n; ++i)
            {
                double sum = x[i];
                for (int j = 0; j < i; ++j)
                {
                    sum -= luMatrix[i][j] * x[j];
                }
                x[i] = sum;
            }

            x[n - 1] /= luMatrix[n - 1][n - 1];
            for (int i = n - 2; i >= 0; --i)
            {
                double sum = x[i];
                for (int j = i + 1; j < n; ++j)
                {
                    sum -= luMatrix[i][j] * x[j];
                }
                x[i] = sum / luMatrix[i][i];
            }

            return x;
        }

        // --------------------------------------------------

        public static double[] SystemSolve(double[][] A, double[] b)
        {
            // Solve Ax = b
            int n = A.Length;

            // 1. decompose A
            int[] perm;
            int toggle;
            double[][] luMatrix = MatrixDecompose(A, out perm, out toggle);
            if (luMatrix == null)
                return null;

            // 2. permute b according to perm[] into bp
            double[] bp = new double[b.Length];
            for (int i = 0; i < n; ++i)
            {
                bp[i] = b[perm[i]];
            }

            // 3. call helper
            double[] x = HelperSolve(luMatrix, bp);
            return x;
        } // SystemSolve

        // --------------------------------------------------

        public static double[][] MatrixDuplicate(double[][] matrix)
        {
            // allocates/creates a duplicate of a matrix.
            double[][] result = MatrixCreate(matrix.Length, matrix[0].Length);
            for (int i = 0; i < matrix.Length; ++i) // copy the values
            {
                for (int j = 0; j < matrix[i].Length; ++j)
                {
                    result[i][j] = matrix[i][j];
                }
            }
            return result;
        }

        public static double[][] MatrixDuplicate(double[][] matrix, double[][] result)
        {
            // allocates/creates a duplicate of a matrix.
            if (matrix == null || matrix.Length == 0 || result == null || result.Length == 0 || matrix.Length != result.Length || matrix[0].Length != result[0].Length)
            {
                throw new Exception("Null, empty or non-conformable input and output matrices in MatrixDuplicate");
            }

            for (int i = 0; i < matrix.Length; ++i) // copy the values
            {
                for (int j = 0; j < matrix[i].Length; ++j)
                {
                    result[i][j] = matrix[i][j];
                }
            }
            return result;
        }


        public static double[][] MatrixTest(double[][] result)
        {
            result[0][0] = -123456;
            return result;
        }

    }

}
