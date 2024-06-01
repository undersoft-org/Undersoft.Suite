using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Undersoft.SDK.Estimating
{
    using Undersoft.SDK.Series;

    public class LinearRegressionEstimator : Estimator
    {
        //parameters can be done as a matrix

        private bool validParameters;
        private double parameterSumX = 0;
        private double parameterSumXX = 0;
        private double parameterSumY = 0;
        private double parameterSumYY = 0;
        private double parameterSumXY = 0;
        private double parameterN = 0;
        private double parameterA = 0;
        private double parameterB = 0;


        public override void Prepare(EstimatorInput<EstimatorSeries, EstimatorSeries> input)
        {
            validInput = false;

            if (input.X.Count > 0 && input.X[0].Mode != EstimatorObjectMode.Single ||
                input.Y.Count > 0 && input.Y[0].Mode != EstimatorObjectMode.Single)
            {
                throw new EstimatingException(EstimatingExceptionList.DataTypeSingle);
            }
            Input = input;
            validParameters = false;
            validInput = true;
        }

        public override void Prepare(EstimatorSeries x, EstimatorSeries y)
        {
            Prepare(new EstimatorInput<EstimatorSeries, EstimatorSeries>(x, y));
        }

        public override void Create()
        {
            if (!validInput) throw new EstimatingException(EstimatingExceptionList.DataType);

            parameterN = Input.X.Count;
            parameterSumX = Input.X.Sum(v => v.Vector[0]);
            parameterSumXX = Input.X.Sum(v => v.Vector[0] * v.Vector[0]);
            parameterSumY = Input.Y.Sum(v => v.Vector[0]);
            parameterSumYY = Input.Y.Sum(v => v.Vector[0] * v.Vector[0]);
            parameterSumXY = Input.X.Select((v, j) => v.Vector[0] * Input.Y[j].Vector[0]).Sum();

            double delta = parameterN * parameterSumXX - parameterSumX * parameterSumX;

            parameterA = (parameterN * parameterSumXY - parameterSumX * parameterSumY) / delta;
            parameterB = (parameterSumXX * parameterSumY - parameterSumX * parameterSumXY) / delta;
            validParameters = true;
        }

        public override EstimatorItem Evaluate(object x)
        {
            return Evaluate(new EstimatorItem(x));
        }

        public override EstimatorItem Evaluate(EstimatorItem x)
        {
            if (validParameters == false)
            {
                Create();

                validParameters = true;
            }

            return new EstimatorItem(parameterB + parameterA * x.Vector[0]);
        }

        public override void Update(EstimatorInput<EstimatorSeries, EstimatorSeries> input)
        {
            if (input.X.Count > 0 && input.X[0].Mode != EstimatorObjectMode.Single ||
                input.Y.Count > 0 && input.Y[0].Mode != EstimatorObjectMode.Single)
            {
                throw new EstimatingException(EstimatingExceptionList.DataTypeSingle);
            }

            parameterN += input.X.Count;
            parameterSumX += input.X.Sum(v => v.Vector[0]);
            parameterSumXX += input.X.Sum(v => v.Vector[0] * v.Vector[0]);
            parameterSumY = input.Y.Sum(v => v.Vector[0]);
            parameterSumYY += input.Y.Sum(v => v.Vector[0] * v.Vector[0]);
            parameterSumXY += input.X.Select((v, j) => v.Vector[0] * Input.Y[j].Vector[0]).Sum();

            double delta = parameterN * parameterSumXX - parameterSumX * parameterSumX;

            parameterA = (parameterN * parameterSumXY - parameterSumX * parameterSumY) / delta;
            parameterB = (parameterSumXX * parameterSumY - parameterSumX * parameterSumXY) / delta;
            validParameters = true;
        }

        public override void Update(EstimatorSeries x, EstimatorSeries y)
        {
            Update(new EstimatorInput<EstimatorSeries, EstimatorSeries>(x, y));
        }

        public override double[][] GetParameters()
        {
            //can be done as a matrix

            return null;
        }


    }

}
