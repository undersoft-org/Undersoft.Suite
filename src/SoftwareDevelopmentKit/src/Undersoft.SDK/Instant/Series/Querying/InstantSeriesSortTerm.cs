namespace Undersoft.SDK.Instant.Series.Querying;

using Rubrics;
using System;
using System.Linq;

[Serializable]
public class InstantSeriesSortTerm : IInstantSeriesSortTerm
{
    public string dataTypeName;

    [NonSerialized]
    private Type dataType;

    [NonSerialized]
    private IInstantSeries series;

    private string rubricName;

    private MemberRubric sortedRubric;

    public InstantSeriesSortTerm() { }

    public InstantSeriesSortTerm(IInstantSeries table)
    {
        InstantSeriesGenerator = table;
    }

    public InstantSeriesSortTerm(
        MemberRubric sortedRubric,
        SortDirection direction = SortDirection.Ascending,
        int ordinal = 0
    )
    {
        Direction = direction;
        SortedRubric = sortedRubric;
        RubricId = ordinal;
    }

    public InstantSeriesSortTerm(string rubricName, string direction = "Ascending", int ordinal = 0)
    {
        RubricName = rubricName;
        SortDirection sortDirection;
        Enum.TryParse(direction, true, out sortDirection);
        Direction = sortDirection;
        RubricId = ordinal;
    }

    public SortDirection Direction { get; set; }

    public IInstantSeries InstantSeriesGenerator
    {
        get { return series; }
        set
        {
            if (value != null)
            {
                series = value;
                if (rubricName != null)
                    if (value.Rubrics.ContainsKey(rubricName))
                    {
                        MemberRubric pyl = value.Rubrics
                            .AsValues()
                            .Where(c => c.RubricName == rubricName)
                            .First();
                        if (pyl != null)
                        {
                            if (sortedRubric == null)
                                sortedRubric = pyl;
                            if (RubricType == null)
                                RubricType = pyl.RubricType;
                            if (TypeString == null)
                                TypeString = GetTypeString(pyl.RubricType);
                        }
                    }
            }
        }
    }

    public int Index { get; set; }

    public int RubricId { get; set; }

    public string RubricName
    {
        get { return rubricName; }
        set
        {
            rubricName = value;
            if (InstantSeriesGenerator != null)
            {
                if (InstantSeriesGenerator.Rubrics.ContainsKey(rubricName))
                {
                    if (sortedRubric == null)
                        SortedRubric = InstantSeriesGenerator.Rubrics
                            .AsValues()
                            .Where(c => c.RubricName == RubricName)
                            .First();
                    if (RubricType == null)
                        RubricType = SortedRubric.RubricType;
                    if (TypeString == null)
                        TypeString = GetTypeString(RubricType);
                }
            }
        }
    }

    public Type RubricType
    {
        get
        {
            if (dataType == null && dataTypeName != null)
                dataType = Type.GetType(dataTypeName);
            return dataType;
        }
        set
        {
            dataType = value;
            dataTypeName = value.FullName;
        }
    }

    public MemberRubric SortedRubric
    {
        get { return sortedRubric; }
        set
        {
            if (value != null)
            {
                sortedRubric = value;
                rubricName = sortedRubric.RubricName;
                RubricType = sortedRubric.RubricType;
                TypeString = GetTypeString(RubricType);
            }
        }
    }

    public string TypeString { get; set; }

    public bool Compare(InstantSeriesSortTerm term)
    {
        if (RubricName != term.RubricName || Direction != term.Direction)
            return false;

        return true;
    }

    private string GetTypeString(MemberRubric column)
    {
        Type dataType = column.RubricType;
        string type = "string";
        if (dataType == typeof(string))
            type = "string";
        else if (dataType == typeof(int))
            type = "int";
        else if (dataType == typeof(decimal))
            type = "decimal";
        else if (dataType == typeof(DateTime))
            type = "DateTime";
        else if (dataType == typeof(Single))
            type = "Single";
        else if (dataType == typeof(float))
            type = "float";
        else
            type = "string";
        return type;
    }

    private string GetTypeString(Type RubricType)
    {
        Type dataType = RubricType;
        string type = "string";
        if (dataType == typeof(string))
            type = "string";
        else if (dataType == typeof(int))
            type = "int";
        else if (dataType == typeof(decimal))
            type = "decimal";
        else if (dataType == typeof(DateTime))
            type = "DateTime";
        else if (dataType == typeof(Single))
            type = "Single";
        else if (dataType == typeof(float))
            type = "float";
        else
            type = "string";
        return type;
    }
}
