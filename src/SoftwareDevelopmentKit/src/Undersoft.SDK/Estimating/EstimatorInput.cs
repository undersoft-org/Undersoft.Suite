using System;
using System.Collections.Generic;
using System.Text;

namespace Undersoft.SDK.Estimating
{
    public class EstimatorInput<A, B> where A : EstimatorSeries where B : EstimatorSeries
    {
        public EstimatorSeries X;
        public EstimatorSeries Y;


        //czy dawac pusty????
        public EstimatorInput()
        {
            X = new EstimatorSeries();
            Y = new EstimatorSeries();
        }

        public EstimatorInput(A x, B y)
        {
            if (x.Count != y.Count)
            {
                throw new EstimatingException(EstimatingExceptionList.DataTypeInconsistentXY);
            }

            X = x;
            Y = y;
        }
    }
}
