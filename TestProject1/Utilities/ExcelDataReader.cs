using OfficeOpenXml;

namespace TestCompact.Utilities
{
    public class ExcelDataReader
    {
        public static IEnumerable<object[]> GetDataExcel(string urlFichero, int inicioRow, int inicioCol, int finalRow, int finalCol)
        {
            if (!File.Exists(urlFichero))
            {
                throw new FileNotFoundException("El archivo Excel no existe.", urlFichero);
            }

            List<object[]> empleadosData = new List<object[]>();

            using (var package = new ExcelPackage(new FileInfo(urlFichero)))
            {
                var worksheet = package.Workbook.Worksheets[0]; // Suponiendo que los datos están en la primera hoja

                for (int row = inicioRow; row <= finalRow; row++)
                {
                    List<string> rowData = new List<string>();

                    for (int col = inicioCol; col <= finalCol; col++)
                    {
                        string cellValue = worksheet.Cells[row, col].Value?.ToString() ?? string.Empty;
                        rowData.Add(cellValue);
                    }

                    empleadosData.Add(rowData.ToArray());
                }
            }

            return empleadosData;
        }
    }
}
