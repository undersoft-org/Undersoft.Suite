using System;
using System.Collections.Generic;
using System.Text;

namespace Undersoft.SDK.Estimating
{
    using Undersoft.SDK.Series;

    public class LinearLastSquareEstimator : Estimator
    {
        private bool validParameters;
        private double[][] parameterTheta;

        public override void Prepare(EstimatorInput<EstimatorSeries, EstimatorSeries> input)
        {
            //verify
            Input = input;
            validInput = true;
            validParameters = false;
        }

        public override void Prepare(EstimatorSeries x, EstimatorSeries y)
        {
            Prepare(new EstimatorInput<EstimatorSeries, EstimatorSeries>(x, y));
        }

        public override void Create()
        {
            // cov = inv(X'*X)
            // M = covA*X' ==> inv(X'*X)*X'
            // theta = M*Y
            // y = x*theta
            //in other words:
            //X_k = X[k].Item - multi D
            //y_k = Y[k].Item[0] - 1D or multi D
            //theta = (sum_{k=1}^{N}X_k*X_k')^{-1} (sum_{k=1}^{N}X_k*y_k)

            if (validInput == false)
            {
                throw new EstimatingException(EstimatingExceptionList.DataType);
            }

            double[][] X = CreateMatrix(Input.X);
            double[][] Y = CreateMatrix(Input.Y);
            double[][] XT = EstimatorMatrix.MatrixTranpose(X);
            double[][] XTX = EstimatorMatrix.MatrixProduct(XT, X);
            double[][] invXTX = EstimatorMatrix.MatrixInverse(XTX);

            //throw exception if covariance matrix is invertible //try catch na inverse matrix

            double[][] invXTX_X = EstimatorMatrix.MatrixProduct(invXTX, XT);
            double[][] theta = EstimatorMatrix.MatrixProduct(invXTX_X, Y);
            parameterTheta = theta;
        }

        public override EstimatorItem Evaluate(object x)
        {
            return Evaluate(new EstimatorItem(x));
        }

        public override EstimatorItem Evaluate(EstimatorItem x) //nazwe dac inna niz xValue
        {
            if (validParameters == false) //to aviod recalculations of systemParameters
            {
                Create();
            }

            return new EstimatorItem(EstimatorMatrix.MatrixVectorProduct(EstimatorMatrix.MatrixTranpose(parameterTheta), x.Vector));
        }

        public override void Update(EstimatorInput<EstimatorSeries, EstimatorSeries> input)
        {
            throw new EstimatingException(EstimatingExceptionList.MethodCannotBeProceeded);
        }

        public override void Update(EstimatorSeries x, EstimatorSeries y)
        {
            throw new EstimatingException(EstimatingExceptionList.MethodCannotBeProceeded);
        }

        public override double[][] GetParameters()
        {
            return EstimatorMatrix.MatrixDuplicate(parameterTheta);
        }

    }
}
