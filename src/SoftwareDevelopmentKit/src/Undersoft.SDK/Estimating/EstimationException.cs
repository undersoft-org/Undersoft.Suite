using System;
using System.Collections.Generic;
using System.Text;

namespace Undersoft.SDK.Estimating
{
    public class EstimatingException : Exception
    {
        public EstimatingException(EstimatingExceptionList exceptionList)
           : base(EstimatingExceptionRegistry.Registry[exceptionList])
        {

        }
    }

    public static class EstimatingExceptionRegistry
    {
        public static Dictionary<EstimatingExceptionList, string> Registry =
            new Dictionary<EstimatingExceptionList, string>()
        {
            {EstimatingExceptionList.DataType, "Wrong input data type" },
            {EstimatingExceptionList.DataTypeSingle, "Wrong data type: input X or Y is not a single column" },
            {EstimatingExceptionList.DataTypeConvertDouble, "Wrong data type: input cannot be converted to double" },
            {EstimatingExceptionList.DataTypeInconsistentXY, "Wrong data type: input X inconsistent with Y" },
            {EstimatingExceptionList.InputParameterInconsistent, "Input inconsistent estimator parameter size" },
            {EstimatingExceptionList.MethodCannotBeProceeded, "InvokeMethod cannot be proceeded for this estimator" },
            {EstimatingExceptionList.Error, "Error - System Crash" }
        };

    }

    public enum EstimatingExceptionList
    {
        DataType,
        DataTypeSingle,
        DataTypeConvertDouble,
        DataTypeInconsistentXY,
        InputParameterInconsistent,
        MethodCannotBeProceeded,
        Error
    }
}
