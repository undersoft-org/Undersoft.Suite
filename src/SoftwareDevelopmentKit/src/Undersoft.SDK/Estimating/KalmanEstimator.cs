using System;
using System.Collections.Generic;
using System.Text;

namespace Undersoft.SDK.Estimating
{
    using Undersoft.SDK.Series;

    //podobny do RLS - zdecydowanie inny jest dopiero Extended Kalman Filter
    public class KalmanEstimator : Estimator
    {
        private bool validParameters;
        private double[][] parameterK;
        private double[][] parameterP;
        private double[][] parameterTheta;

        private List<double> advancedParameters;

        //przyspieszyc estymatory - bez nieustannego alokowania, tylko operacje na juz istniejacych elementach !!!!

        public override void Prepare(EstimatorInput<EstimatorSeries, EstimatorSeries> input)
        {
            //verification etc....
            Input = input;
            validInput = true;
            validParameters = false;
        }

        public override void Prepare(EstimatorSeries x, EstimatorSeries y)
        {
            Prepare(new EstimatorInput<EstimatorSeries, EstimatorSeries>(x, y));
        }

        public override EstimatorItem Evaluate(object x)
        {
            return Evaluate(new EstimatorItem(x));
        }

        public override EstimatorItem Evaluate(EstimatorItem x)
        {
            if (validParameters == false) //to aviod recalculations of systemParameters
            {
                Create();
            }

            return new EstimatorItem(EstimatorMatrix.MatrixVectorProduct(EstimatorMatrix.MatrixTranpose(parameterTheta), x.Vector));
        }

        public override void Create()
        {
            // RLS Canonical form:
            // 
            // P initial values:
            //    a) P>>0 (10^3) small confidence in initial theta
            //    b) P~10 confidence in initial theta
            // theta = column vector
            // P = eye(nx,nx)*value
            // X = [[x1..xn];[x1,...,xn]; [x1,..., xn]]
            // Y =[
            // XX = [x1...xn]' column vector
            // prediction step: 
            // P = P + Rw; Rw - related with noise-error
            // correction step:
            // K = P*XX*inv(Rv+XX'*P*XX) //Rv - error-noise
            // theta = theta + K * (YY - XX'*theta)
            // P = P - K*XX'*P
            if (validInput == false)
            {
                throw new EstimatingException(EstimatingExceptionList.DataType);
            }

            int m = Input.X.Count;
            int nx = Input.X[0].Vector.Length;
            int ny = Input.Y[0].Vector.Length;
            double[][] xx = EstimatorMatrix.MatrixCreate(nx, 1);
            double[][] yy = EstimatorMatrix.MatrixCreate(1, ny);

            double[][] K = EstimatorMatrix.MatrixCreate(nx, 1);
            double[][] P = EstimatorMatrix.MatrixDiagonal(nx, 1000); //nx x nx small confidence in initial theta (which is 0 0 0 0)
            double[][] theta = EstimatorMatrix.MatrixCreate(nx, ny);

            double[][] Rw = EstimatorMatrix.MatrixDiagonal(nx, 1);
            double[][] Rv = EstimatorMatrix.MatrixDiagonal(1, 1);

            //auxuliary calculations
            double[][] xxT = EstimatorMatrix.MatrixCreate(1, nx);    //xx'
            double[][] P_XX = EstimatorMatrix.MatrixCreate(nx, 1);   //P*xx
            double[][] XXT_P = EstimatorMatrix.MatrixCreate(1, nx);
            double[][] XXT_P_XX = EstimatorMatrix.MatrixCreate(1, 1); //XX'*P*XX -> scalar, later + ff
            double[][] inv_XXT_P_XX = EstimatorMatrix.MatrixCreate(1, 1);
            double[][] XXT_theta = EstimatorMatrix.MatrixCreate(1, ny);
            double[][] YY_XXT_theta = EstimatorMatrix.MatrixCreate(1, ny);
            double[][] K_YY_XXT_theta = EstimatorMatrix.MatrixCreate(nx, ny);
            double[][] K_XXT_P = EstimatorMatrix.MatrixCreate(nx, nx);


            if (advancedParameters != null)
            {
                Rv[0][0] = advancedParameters[0];
                Rw = EstimatorMatrix.MatrixDiagonal(nx, advancedParameters[1]);
            }

            for (int i = 0; i < m; i++)
            {
                xx = EstimatorMatrix.MatrixCreateColumn(Input.X[i].Vector, xx);
                xxT = EstimatorMatrix.MatrixTranpose(xx, xxT);
                yy = EstimatorMatrix.MatrixCreateRow(Input.Y[i].Vector, yy);
                P = EstimatorMatrix.MatrixSum(P, Rw, P);
                P_XX = EstimatorMatrix.MatrixProduct(P, xx, P_XX);
                XXT_P = EstimatorMatrix.MatrixProduct(xxT, P, XXT_P);
                XXT_P_XX = EstimatorMatrix.MatrixProduct(XXT_P, xx, XXT_P_XX);
                XXT_P_XX = EstimatorMatrix.MatrixSum(XXT_P_XX, Rv, XXT_P_XX);
                inv_XXT_P_XX = EstimatorMatrix.MatrixInverse(XXT_P_XX, inv_XXT_P_XX);
                K = EstimatorMatrix.MatrixProduct(P_XX, inv_XXT_P_XX, K);
                XXT_theta = EstimatorMatrix.MatrixProduct(xxT, theta, XXT_theta);
                YY_XXT_theta = EstimatorMatrix.MatrixSub(yy, XXT_theta, YY_XXT_theta);
                K_YY_XXT_theta = EstimatorMatrix.MatrixProduct(K, YY_XXT_theta, K_YY_XXT_theta);
                theta = EstimatorMatrix.MatrixSum(theta, K_YY_XXT_theta, theta);
                K_XXT_P = EstimatorMatrix.MatrixProduct(K, XXT_P, K_XXT_P);
                P = EstimatorMatrix.MatrixSub(P, K_XXT_P, P);
            }

            parameterK = K;
            parameterP = P;
            parameterTheta = theta;

            validParameters = true;
        }

        public override void Update(EstimatorInput<EstimatorSeries, EstimatorSeries> input)
        {
            if ((input == null || input.X.Count == 0 || input.X.Count == 0)
                || (parameterTheta != null)
                    && (input.X[0].Vector.Length != parameterTheta.Length || input.Y[0].Vector.Length != parameterTheta[0].Length))
            {
                throw new EstimatingException(EstimatingExceptionList.InputParameterInconsistent);
            }

            int m = Input.X.Count;
            int nx = Input.X[0].Vector.Length;
            int ny = Input.Y[0].Vector.Length;
            double[][] xx = EstimatorMatrix.MatrixCreate(nx, 1);
            double[][] yy = EstimatorMatrix.MatrixCreate(1, ny);

            double[][] K = EstimatorMatrix.MatrixCreate(nx, 1);
            double[][] P = EstimatorMatrix.MatrixDiagonal(nx, 10000); //nx x nx small confidence in initial theta (which is 0 0 0 0)
            double[][] theta = EstimatorMatrix.MatrixCreate(nx, ny);

            double[][] Rw = EstimatorMatrix.MatrixDiagonal(nx, 1);
            double[][] Rv = EstimatorMatrix.MatrixDiagonal(1, 1);

            //auxuliary calculations
            double[][] xxT = EstimatorMatrix.MatrixCreate(1, nx);    //xx'
            double[][] P_XX = EstimatorMatrix.MatrixCreate(nx, 1);   //P*xx
            double[][] XXT_P = EstimatorMatrix.MatrixCreate(1, nx);
            double[][] XXT_P_XX = EstimatorMatrix.MatrixCreate(1, 1); //XX'*P*XX -> scalar, later + ff
            double[][] inv_XXT_P_XX = EstimatorMatrix.MatrixCreate(1, 1);
            double[][] XXT_theta = EstimatorMatrix.MatrixCreate(1, ny);
            double[][] YY_XXT_theta = EstimatorMatrix.MatrixCreate(1, ny);
            double[][] K_YY_XXT_theta = EstimatorMatrix.MatrixCreate(nx, ny);
            double[][] K_XXT_P = EstimatorMatrix.MatrixCreate(nx, nx);

            if (validParameters != false) //update run
            {
                K = parameterK;
                P = parameterP;
                theta = parameterTheta;
            }

            if (advancedParameters != null)
            {
                Rv[0][0] = advancedParameters[0];
                Rw = EstimatorMatrix.MatrixDiagonal(nx, advancedParameters[1]);
            }

            for (int i = 0; i < m; i++)
            {
                xx = EstimatorMatrix.MatrixCreateColumn(Input.X[i].Vector, xx);
                xxT = EstimatorMatrix.MatrixTranpose(xx, xxT);
                yy = EstimatorMatrix.MatrixCreateRow(Input.Y[i].Vector, yy);
                P = EstimatorMatrix.MatrixSum(P, Rw, P);
                P_XX = EstimatorMatrix.MatrixProduct(P, xx, P_XX);
                XXT_P = EstimatorMatrix.MatrixProduct(xxT, P, XXT_P);
                XXT_P_XX = EstimatorMatrix.MatrixProduct(XXT_P, xx, XXT_P_XX);
                XXT_P_XX = EstimatorMatrix.MatrixSum(XXT_P_XX, Rv, XXT_P_XX);
                inv_XXT_P_XX = EstimatorMatrix.MatrixInverse(XXT_P_XX, inv_XXT_P_XX);
                K = EstimatorMatrix.MatrixProduct(P_XX, inv_XXT_P_XX, K);
                XXT_theta = EstimatorMatrix.MatrixProduct(xxT, theta, XXT_theta);
                YY_XXT_theta = EstimatorMatrix.MatrixSub(yy, XXT_theta, YY_XXT_theta);
                K_YY_XXT_theta = EstimatorMatrix.MatrixProduct(K, YY_XXT_theta, K_YY_XXT_theta);
                theta = EstimatorMatrix.MatrixSum(theta, K_YY_XXT_theta, theta);
                K_XXT_P = EstimatorMatrix.MatrixProduct(K, XXT_P, K_XXT_P);
                P = EstimatorMatrix.MatrixSub(P, K_XXT_P, P);
            }

            parameterK = K;
            parameterP = P;
            parameterTheta = theta;
            validParameters = true;
        }

        public override void Update(EstimatorSeries x, EstimatorSeries y)
        {
            Update(new EstimatorInput<EstimatorSeries, EstimatorSeries>(x, y));
        }

        public override void SetAdvancedParameters(IList<object> advParameters = null)
        {
            //exception ... or not double
            if (advParameters == null || advParameters.Count < 2)
            {
                advancedParameters = null;
                return;
            }
            advancedParameters = new List<double>();
            advancedParameters.Add(Convert.ToDouble(advParameters[0]));
            advancedParameters.Add(Convert.ToDouble(advParameters[1]));
        }

        public override double[][] GetParameters()
        {
            return EstimatorMatrix.MatrixDuplicate(parameterTheta);
        }
    }

}
