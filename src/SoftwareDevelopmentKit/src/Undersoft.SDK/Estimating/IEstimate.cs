namespace Undersoft.SDK.Estimating
{
    //usage:
    // Estimate -> imply calling constrtor Staticsics(input, default_method) -> defining Input (initial)
    // Estimate.Prepage(input) -> redefinition of Input
    // Estimate.Evaluate(x) -> Estimate y for given x and inner Input for default_method (inner)
    // Estimate.DefaultMethod(method) -> changes DefaultMethod for esitmation
    // Additional use: Update(input) - update estimator parameters for input data
    public interface IEstimate
    {
        Estimate CreateEstimator(EstimatorMethod method);
    }
}
