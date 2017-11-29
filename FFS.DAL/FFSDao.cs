using ExcelDataReader;
using FFS.BE.DataSheet;
using GenericCore.Support;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFS.DAL
{
    public class FFSDao
    {
        private readonly IDictionary<string, string> columnsMapping =
            new Dictionary<string, string>
            {
                { "R", "Role" },
                { "Nome", "Name" },
                { "Squadra", "Team" },
                { "Pg", "GamesPlayed" },
                { "Mv", "Ranking" },
                { "Mf", "FantasyRanking" },
                { "Gf", "ScoredGoals" },
                { "Gs", "ConcededGoals" },
                { "Rp", "SavedPenalties" },
                { "Rc", "TotalPenalties" },
                { "R+", "ScoredPenalties" },
                { "R-", "MissedPenalties" },
                { "Ass", "Assists" },
                { "Asf", "StationaryAssists" },
                { "Amm", "YellowCards" },
                { "Esp", "RedCards" },
                { "Au", "OwnGoals" },
            };

        public string FilePath { get; set; }

        private DataSet _data = null;
        public DataSet Data
        {
            get
            {
                return _data;
            }
        }

        private string[] _sheets = null;
        public string[] Sheets
        {
            get
            {
                return _sheets;
            }
        }

        private IList<DataSheet> _dataSheetList = null;
        public IList<DataSheet> DataSheetList
        {
            get
            {
                return _dataSheetList;
            }
        }

        public FFSDao(string filePath)
        {
            filePath.AssertNotNull("filePath");

            FilePath = filePath;
            _dataSheetList = new List<DataSheet>();

            ReadFile(filePath);
        }

        private void ReadFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File {filePath} not found");
            }

            using (FileStream stream = File.Open(FilePath, FileMode.Open, FileAccess.Read))
            {
                using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                {
                    _data = reader.AsDataSet(new ExcelDataSetConfiguration
                    {
                        UseColumnDataType = true,
                        ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration
                        {
                            EmptyColumnNamePrefix = "Column",
                            UseHeaderRow = true,
                            ReadHeaderRow = (rowReader) => { rowReader.Read(); }
                        }
                    });
                }
            }

            foreach (DataTable table in _data.Tables)
            {
                string name = ParseSheetName(table.TableName);
                IList<DataSheetRow> rows = table.ToEntityList<DataSheetRow>(true, StringComparison.InvariantCultureIgnoreCase, columnsMapping);
                _dataSheetList.Add(new DataSheet(name, rows));
            }
        }

        private string ParseSheetName(string sheetName)
        {
            sheetName.AssertNotNull("sheetName");
            return sheetName.Replace("$", string.Empty);
        }

        private string[] ParseSheetNameList(string[] sheetNameList)
        {
            sheetNameList.AssertNotNull("sheetNameList");

            return 
                sheetNameList
                    .Where(x => x.EndsWith("$"))
                    .Select(x => x.Replace("$", string.Empty))
                    .ToArray();
        }
    }
}
