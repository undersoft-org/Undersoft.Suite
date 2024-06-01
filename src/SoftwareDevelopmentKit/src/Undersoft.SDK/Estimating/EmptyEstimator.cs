using System;
using System.Collections.Generic;
using System.Text;

namespace Undersoft.SDK.Estimating
{
    using Undersoft.SDK.Series;

    public class EmptyEstimator : Estimator
    {
        public override void Prepare(EstimatorInput<EstimatorSeries, EstimatorSeries> input)
        {
            Input = input;
        }

        public override void Prepare(EstimatorSeries x, EstimatorSeries y)
        {
            Input = new EstimatorInput<EstimatorSeries, EstimatorSeries>(x, y);
        }

        public override void Create()
        {

        }

        public override EstimatorItem Evaluate(object x)
        {
            return Evaluate(new EstimatorItem(x));
        }

        public override EstimatorItem Evaluate(EstimatorItem x)
        {
            return new EstimatorItem(x);
        }

        public override void Update(EstimatorInput<EstimatorSeries, EstimatorSeries> input)
        {
            return;
        }

        public override void Update(EstimatorSeries x, EstimatorSeries y)
        {
            return;
        }

        public override double[][] GetParameters()
        {
            return null;
        }
    }

}
