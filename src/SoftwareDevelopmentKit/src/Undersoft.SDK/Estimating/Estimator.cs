using System;
using System.Collections.Generic;
using System.Text;

namespace Undersoft.SDK.Estimating
{
    using Undersoft.SDK.Series;

    public abstract class Estimator
    {
        protected bool validInput;

        //sprawdzic raz jeszcz uzycie tego
        protected static double[][] CreateMatrix(EstimatorSeries input)
        {
            double[][] result;

            result = new double[input.Count][];
            for (int i = 0; i < input.Count; i++)
            {
                result[i] = new double[input[0].Vector.Length];
                for (int j = 0; j < input[0].Vector.Length; j++)
                {
                    result[i][j] = input[i].Vector[j];
                }
            }
            return result;
        }

        public EstimatorInput<EstimatorSeries, EstimatorSeries> Input;

        public abstract void Prepare(EstimatorInput<EstimatorSeries, EstimatorSeries> input);

        public abstract void Prepare(EstimatorSeries x, EstimatorSeries y);

        public abstract void Update(EstimatorInput<EstimatorSeries, EstimatorSeries> input);

        public abstract void Update(EstimatorSeries x, EstimatorSeries y);

        public abstract void Create();

        public abstract EstimatorItem Evaluate(EstimatorItem x);

        public abstract EstimatorItem Evaluate(object x);

        public virtual void SetAdvancedParameters(IList<object> advParameters = null)
        {

        }

        public abstract double[][] GetParameters();
    }
}
