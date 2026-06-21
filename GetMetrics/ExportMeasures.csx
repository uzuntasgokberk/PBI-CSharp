// =============================================================================
// Tabular Measures Exporter
// -----------------------------------------------------------------------------
// Exports every measure in the active Tabular model as a tab-separated (TSV)
// table with the following columns:
//
//     Table | Measure | Folder | FormatString | Formula
//
// The result is printed with .Output() so it can be copied straight into a
// spreadsheet or pasted into a .tsv file.
//
// Tool : Tabular Editor 2 or 3  (run from the "C# Script" tab)
// Usage: open your model, paste this script, and run it.
// =============================================================================

// Makes a single value safe to place inside a TSV cell:
//   * null / empty becomes an empty string (avoids NullReferenceException on
//     optional properties such as DisplayFolder, FormatString or Expression),
//   * tabs and line breaks are collapsed to single spaces so they can never
//     break the column or row structure of the output.
string Clean(string value)
{
    if (string.IsNullOrEmpty(value))
        return "";

    return value
        .Replace("\t", " ")
        .Replace("\r\n", " ")
        .Replace("\r", " ")
        .Replace("\n", " ");
}

// Build the output with a StringBuilder instead of repeated string
// concatenation, which would allocate a new string on every iteration and
// scale poorly on large models.
var sb = new System.Text.StringBuilder();

// Header row. Keep these column names stable so any downstream tool or
// spreadsheet that expects them keeps working.
sb.Append("Table\tMeasure\tFolder\tFormatString\tFormula\r\n");

// Iterate over every measure across all tables in the model. If the model has
// no measures this loop simply does nothing and only the header is returned,
// which is still valid output.
foreach (var measure in Model.AllMeasures)
{
    // Each field is cleaned independently. DisplayFolder, FormatString and
    // Expression are all optional and may be null on a given measure.
    sb.Append(Clean(measure.Table.Name)).Append('\t');
    sb.Append(Clean(measure.Name)).Append('\t');
    sb.Append(Clean(measure.DisplayFolder)).Append('\t');
    sb.Append(Clean(measure.FormatString)).Append('\t');
    sb.Append(Clean(measure.Expression)).Append("\r\n");
}

// Print the assembled TSV to the Tabular Editor output window.
sb.ToString().Output();
