using FFS.BE.Data;
using GenericCore.Support;
using GenericCore.Support.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFS.DAL
{
    public static class FFSDao
    {
        private static IDictionary<string, string> columnsMapping =
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

        public static string FilePath { get; set; }

        private static DataSet _data = null;
        public static DataSet Data
        {
            get
            {
                return _data;
            }
        }

        private static string[] _sheets = null;
        public static string[] Sheets
        {
            get
            {
                return _sheets;
            }
        }

        private static IList<DataSheet> _dataSheetList = null;
        public static IList<DataSheet> DataSheetList
        {
            get
            {
                return _dataSheetList;
            }
        }

        public static void ReadFile(string filePath)
        {
            filePath.AssertNotNull("filePath");

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File {filePath} not found");
            }

            FilePath = filePath;

            OleDbExcelReader reader = new OleDbExcelReader(ExcelOleDbConnectionString.ToExcelV12(filePath));
            _sheets = ParseSheetNameList(reader.ExtractSheetNames());
            _data = reader.ReadData(_sheets);
            _dataSheetList = new List<DataSheet>();

            foreach (DataTable table in _data.Tables)
            {
                string name = ParseSheetName(table.TableName);
                IList<DataSheetRow> rows = table.ToEntityList<DataSheetRow>(true, StringComparison.InvariantCultureIgnoreCase, columnsMapping);
                _dataSheetList.Add(new DataSheet(name, rows));
            }
        }

        private static string ParseSheetName(string sheetName)
        {
            sheetName.AssertNotNull("sheetName");
            return sheetName.Replace("$", string.Empty);
        }

        private static string[] ParseSheetNameList(string[] sheetNameList)
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
