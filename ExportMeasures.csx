var result = "Table\tMeasure\tFolder\tFormatString\tFormula\r\n";
foreach(var m in Model.AllMeasures)
{
    result += m.Table.Name 
        + "\t" + m.Name 
        + "\t" + m.DisplayFolder 
        + "\t" + m.FormatString 
        + "\t\"" + m.Expression.Replace("\"", "\"\"").Replace("\r\n", " ").Replace("\n", " ") 
        + "\"\r\n";
}
result.Output();