# Tabular Measures Exporter

A small [Tabular Editor](https://tabulareditor.com/) C# script that exports
**every measure** in a Power BI / Analysis Services Tabular model to a
tab-separated (TSV) table you can paste straight into Excel, Google Sheets, or
save as a file.

## What it does

For each measure in the model it outputs one row with these columns:

| Column         | Source property         |
| -------------- | ----------------------- |
| `Table`        | owning table name       |
| `Measure`      | measure name            |
| `Folder`       | display folder          |
| `FormatString` | format string           |
| `Formula`      | DAX expression          |

The output is a clean TSV: every field has embedded tabs and line breaks
collapsed to spaces, and optional properties (folder, format string,
expression) are handled safely even when empty. A model with no measures simply
returns the header row.

## Requirements

- [Tabular Editor 2](https://github.com/TabularEditor/TabularEditor) (free) **or**
  [Tabular Editor 3](https://tabulareditor.com/) (commercial)
- An open connection to a Power BI / Analysis Services Tabular model

## How to run

1. Open your model in Tabular Editor.
2. Go to the **C# Script** tab.
3. Paste the contents of [`ExportMeasures.csx`](ExportMeasures.csx).
4. Run the script (**F5**).
5. Copy the result from the output window, or save it to a `.tsv` file.

## Example output

```
Table	Measure	Folder	FormatString	Formula
Sales	Total Sales	KPIs	#,##0	SUM(Sales[Amount])
Sales	Margin %	KPIs	0.0%	DIVIDE([Profit], [Total Sales])
```

> The example values above are illustrative only — running the script returns
> the measures from **your** model.

## Notes

- The script is read-only: it only reads model metadata and prints it. It does
  not modify the model.
- Exported `.tsv` / `.csv` / `.txt` files are git-ignored so model contents are
  never committed to the repository.

## License

Released under the MIT License — feel free to use, modify, and share.
