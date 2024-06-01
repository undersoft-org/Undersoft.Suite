using System.Collections;

namespace Undersoft.SDK.Estimating
{
    using Uniques;

    public class EstimatorItem: Identifiable
    {
        public string Name;
        public double[] Vector;
        public EstimatorObjectMode Mode;

        public EstimatorItem()
        {
            Id = (long)Unique.NewId;
        }

        public EstimatorItem(long id, string name, object vector)
        {
            Id = id;
            Name = name;
            SetVector(vector);
        }

        public EstimatorItem(EstimatorItem item)
        {
            Vector = item.Vector;
            Mode = item.Mode;
            Name = item.Name;
            Id = item.Id;
        }

        public EstimatorItem(object vector) : this() 
        {            
           SetVector(vector);
        }

        public void SetVector(object vector)
        {
            var type = vector.GetType();
            if (type.IsValueType)
            {
                Vector = new double[] { Convert.ToDouble(vector) };
                Mode = EstimatorObjectMode.Single;
            }
            else if (type.IsArray)
            {
                Vector = ((Array)vector).Cast<object>().Select(o => Convert.ToDouble(o)).ToArray();
                Mode = EstimatorObjectMode.Multi;
            }
            else if (type.IsAssignableTo(typeof(IList)))
            {
                var vectorList = (IList)vector;
                if (vectorList.Count > 0 && vectorList[0] is ValueType)
                {
                    Vector = vectorList.Cast<object>().Select(o => Convert.ToDouble(o)).ToArray();
                    Mode = EstimatorObjectMode.Multi;
                }
                else
                {
                    throw new Exception("Wrong data type");
                }
            }
            else
            {
                throw new Exception("Wrong data type");
            }
        }
    }

    public enum EstimatorObjectMode
    {
        Multi,
        Single
    }
}
