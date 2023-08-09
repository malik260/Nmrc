using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Text;

namespace Mortgage.Ecosystem.DataAccess.Layer.Helpers
{
    // Export List to Excel file
    // <typeparam name="T"></typeparam>
    public class ExcelHelper<T> where T : new()
    {
        #region List export to Excel file

        // Export List to Excel file
        // <param name="sFileName"></param>
        // <param name="sHeaderText"></param>
        // <param name="list"></param>
        public string ExportToExcel(string sFileName, string sHeaderText, List<T> list, string[] columns)
        {
            sFileName = string.Format("{0}_{1}", SecurityHelper.GetGuid(true), sFileName);
            var sRoot = GlobalContext.HostingEnvironment.ContentRootPath;
            var partDirectory = string.Format("Resource{0}Export{0}Excel", Path.DirectorySeparatorChar);
            var sDirectory = Path.Combine(sRoot, partDirectory);
            var sFilePath = Path.Combine(sDirectory, sFileName);
            if (!Directory.Exists(sDirectory)) Directory.CreateDirectory(sDirectory);

            using (var ms = CreateExportMemoryStream(list, sHeaderText, columns))
            {
                using (var fs = new FileStream(sFilePath, FileMode.Create, FileAccess.Write))
                {
                    var data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    fs.Flush();
                }
            }
            return partDirectory + Path.DirectorySeparatorChar + sFileName;
        }

        // List is exported to Excel's MemoryStream
        // <param name="list">Datasource</param>
        // <param name="sHeaderText">Header text</param>
        // <param name="columns">Attributes to be exported</param>
        private MemoryStream CreateExportMemoryStream(List<T> list, string sHeaderText, string[] columns)
        {
            var workbook = new HSSFWorkbook();
            var sheet = workbook.CreateSheet();

            var type = typeof(T);
            var properties = ReflectionHelper.GetProperties(type, columns);

            var dateStyle = workbook.CreateCellStyle();
            var format = workbook.CreateDataFormat();
            dateStyle.DataFormat = format.GetFormat("yyyy-MM-dd");
            //Set the cell format outside the cell filling loop to avoid 4000 row exceptions
            var contentStyle = workbook.CreateCellStyle();
            contentStyle.Alignment = HorizontalAlignment.Left;

            #region Get the column width (maximum width) of each column

            var arrColWidth = new int[properties.Length];
            for (var columnIndex = 0; columnIndex < properties.Length; columnIndex++)
            {
                //The code page corresponding to GBK is CP936
                arrColWidth[columnIndex] = properties[columnIndex].Name.Length;
            }

            #endregion

            for (var rowIndex = 0; rowIndex < list.Count; rowIndex++)
            {
                #region Create a new table, fill the header, fill the column header, style

                if (rowIndex == 65535 || rowIndex == 0)
                {
                    if (rowIndex != 0)
                    {
                        sheet = workbook.CreateSheet();
                    }

                    #region header and style

                    {
                        var headerRow = sheet.CreateRow(0);
                        headerRow.HeightInPoints = 25;
                        headerRow.CreateCell(0).SetCellValue(sHeaderText);

                        var headStyle = workbook.CreateCellStyle();
                        headStyle.Alignment = HorizontalAlignment.Center;
                        var font = workbook.CreateFont();
                        font.FontHeightInPoints = 20;
                        font.Boldweight = 700;
                        headStyle.SetFont(font);

                        headerRow.GetCell(0).CellStyle = headStyle;

                        sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, properties.Length - 1));
                    }

                    #endregion

                    #region Column headers and styles

                    {
                        var headerRow = sheet.CreateRow(1);
                        var headStyle = workbook.CreateCellStyle();
                        headStyle.Alignment = HorizontalAlignment.Center;
                        var font = workbook.CreateFont();
                        font.FontHeightInPoints = 10;
                        font.Boldweight = 700;
                        headStyle.SetFont(font);

                        for (int columnIndex = 0; columnIndex < properties.Length; columnIndex++)
                        {
                            // If the class attribute has a Description, use Description as the column name
                            var customAttribute = (DescriptionAttribute)Attribute.GetCustomAttribute(properties[columnIndex], typeof(DescriptionAttribute));
                            var description = properties[columnIndex].Name;
                            if (customAttribute != null)
                            {
                                description = customAttribute.Description;
                            }
                            headerRow.CreateCell(columnIndex).SetCellValue(description);
                            headerRow.GetCell(columnIndex).CellStyle = headStyle;
                            //Set the column width according to the header
                            sheet.SetColumnWidth(columnIndex, (arrColWidth[columnIndex] + 1) * 256);
                        }
                    }

                    #endregion
                }

                #endregion

                #region fill content

                var dataRow = sheet.CreateRow(rowIndex + 2); // The first 2 rows are occupied
                for (int columnIndex = 0; columnIndex < properties.Length; columnIndex++)
                {
                    var newCell = dataRow.CreateCell(columnIndex);
                    newCell.CellStyle = contentStyle;
                    var drValue = properties[columnIndex].GetValue(list[rowIndex], null).ToStr();
                    //Set the column width according to the cell content
                    var length = Math.Min(253, Encoding.UTF8.GetBytes(drValue).Length + 1) * 256;
                    if (sheet.GetColumnWidth(columnIndex) < length && !drValue.IsNull())
                    {
                        sheet.SetColumnWidth(columnIndex, length);
                    }

                    switch (properties[columnIndex].PropertyType.ToString())
                    {
                        case "System.String":
                            newCell.SetCellValue(drValue);
                            break;

                        case "System.DateTime":
                        case "System.Nullable`1[System.DateTime]":
                            newCell.SetCellValue(drValue.ToDate());
                            newCell.CellStyle = dateStyle; //Formatted display
                            break;

                        case "System.Boolean":
                        case "System.Nullable`1[System.Boolean]":
                            newCell.SetCellValue(drValue.ToStr());
                            break;

                        case "System.Byte":
                        case "System.Nullable`1[System.Byte]":
                        case "System.Int16":
                        case "System.Nullable`1[System.Int16]":
                        case "System.Int32":
                        case "System.Nullable`1[System.Int32]":
                            newCell.SetCellValue(drValue.ToInt());
                            break;

                        case "System.Int64":
                        case "System.Nullable`1[System.Int64]":
                            newCell.SetCellValue(drValue.ToStr());
                            break;

                        case "System.Double":
                        case "System.Nullable`1[System.Double]":
                            newCell.SetCellValue(drValue.ToDouble());
                            break;

                        case "System.Single":
                        case "System.Nullable`1[System.Single]":
                            newCell.SetCellValue(drValue.ToDouble());
                            break;

                        case "System.Decimal":
                        case "System.Nullable`1[System.Decimal]":
                            newCell.SetCellValue(drValue.ToDouble());
                            break;

                        case "System.DBNull":
                            newCell.SetCellValue(string.Empty);
                            break;

                        default:
                            newCell.SetCellValue(string.Empty);
                            break;
                    }
                }

                #endregion
            }

            using (var ms = new MemoryStream())
            {
                workbook.Write(ms);
                workbook.Close();
                ms.Flush();
                ms.Position = 0;
                return ms;
            }
        }

        #endregion

        #region Excel Import

        // Excel import
        // <param name="filePath"></param>
        // <returns></returns>
        public List<T> ImportFromExcel(string filePath)
        {
            var absoluteFilePath = GlobalContext.HostingEnvironment.ContentRootPath + filePath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
            var list = new List<T>();
            HSSFWorkbook hssfWorkbook = null;
            XSSFWorkbook xssWorkbook = null;
            ISheet sheet = null;
            using (var file = new FileStream(absoluteFilePath, FileMode.Open, FileAccess.Read))
            {
                switch (Path.GetExtension(filePath))
                {
                    case ".xls":
                        hssfWorkbook = new HSSFWorkbook(file);
                        sheet = hssfWorkbook.GetSheetAt(0);
                        break;

                    case ".xlsx":
                        xssWorkbook = new XSSFWorkbook(file);
                        sheet = xssWorkbook.GetSheetAt(0);
                        break;

                    default:
                        throw new Exception("Unsupported file format");
                }
            }
            var columnRow = sheet.GetRow(1); // The second row is the field name
            var mapPropertyInfoDict = new Dictionary<int, PropertyInfo>();
            for (var j = 0; j < columnRow.LastCellNum; j++)
            {
                var cell = columnRow.GetCell(j);
                var propertyInfo = MapPropertyInfo(cell.ToStr());
                if (propertyInfo != null)
                {
                    mapPropertyInfoDict.Add(j, propertyInfo);
                }
            }

            for (var i = (sheet.FirstRowNum + 2); i <= sheet.LastRowNum; i++)
            {
                var row = sheet.GetRow(i);
                var entity = new T();
                for (var j = row.FirstCellNum; j < columnRow.LastCellNum; j++)
                {
                    if (mapPropertyInfoDict.ContainsKey(j))
                    {
                        if (row.GetCell(j) != null)
                        {
                            var propertyInfo = mapPropertyInfoDict[j];
                            switch (propertyInfo.PropertyType.ToString())
                            {
                                case "System.DateTime":
                                case "System.Nullable`1[System.DateTime]":
                                    mapPropertyInfoDict[j].SetValue(entity, row.GetCell(j).ToStr().ToDate());
                                    break;

                                case "System.Boolean":
                                case "System.Nullable`1[System.Boolean]":
                                    mapPropertyInfoDict[j].SetValue(entity, row.GetCell(j).ToStr().ToStr());
                                    break;

                                case "System.Byte":
                                case "System.Nullable`1[System.Byte]":
                                    mapPropertyInfoDict[j].SetValue(entity, Byte.Parse(row.GetCell(j).ToStr()));
                                    break;
                                case "System.Int16":
                                case "System.Nullable`1[System.Int16]":
                                    mapPropertyInfoDict[j].SetValue(entity, Int16.Parse(row.GetCell(j).ToStr()));
                                    break;
                                case "System.Int32":
                                case "System.Nullable`1[System.Int32]":
                                    mapPropertyInfoDict[j].SetValue(entity, row.GetCell(j).ToStr().ToInt());
                                    break;

                                case "System.Int64":
                                case "System.Nullable`1[System.Int64]":
                                    mapPropertyInfoDict[j].SetValue(entity, row.GetCell(j).ToStr().ToLong());
                                    break;

                                case "System.Double":
                                case "System.Nullable`1[System.Double]":
                                    mapPropertyInfoDict[j].SetValue(entity, row.GetCell(j).ToStr().ToDouble());
                                    break;

                                case "System.Single":
                                case "System.Nullable`1[System.Single]":
                                    mapPropertyInfoDict[j].SetValue(entity, row.GetCell(j).ToStr().ToDouble());
                                    break;

                                case "System.Decimal":
                                case "System.Nullable`1[System.Decimal]":
                                    mapPropertyInfoDict[j].SetValue(entity, row.GetCell(j).ToStr().ToDecimal());
                                    break;

                                default:
                                case "System.String":
                                    mapPropertyInfoDict[j].SetValue(entity, row.GetCell(j).ToStr());
                                    break;
                            }
                        }
                    }
                }
                list.Add(entity);
            }
            hssfWorkbook?.Close();
            xssWorkbook?.Close();
            return list;
        }

        // Find the entity attribute corresponding to the Excel column name
        // <param name="columnName"></param>
        // <returns></returns>
        private PropertyInfo MapPropertyInfo(string columnName)
        {
            var propertyList = ReflectionHelper.GetProperties(typeof(T));
            var propertyInfo = propertyList.Where(p => p.Name == columnName).FirstOrDefault();
            if (propertyInfo != null)
            {
                return propertyInfo;
            }
            else
            {
                foreach (var tempPropertyInfo in propertyList)
                {
                    var attributes = (DescriptionAttribute[])tempPropertyInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                    if (attributes.Length > 0)
                    {
                        if (attributes[0].Description == columnName)
                        {
                            return tempPropertyInfo;
                        }
                    }
                }
            }
            return null;
        }
        #endregion
    }
}