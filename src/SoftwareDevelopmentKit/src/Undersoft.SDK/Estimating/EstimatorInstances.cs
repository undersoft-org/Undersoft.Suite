using System;
using System.Collections.Generic;
using System.Text;

namespace Undersoft.SDK.Estimating
{
    public static class EstimatorInstances
    {
        public static object New(string strFullyQualifiedName)
        {
            return Instances.New(strFullyQualifiedName);
        }
    }
}
