using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFS.BE.Data
{
    public class DataSheetRow
    {
        public long Id { get; set; }
        public char Role { get; set; } //R
        public string Name { get; set; } //Nome
        public string Team { get; set; } //Squadra
        public int GamesPlayed { get; set; } //Pg
        public decimal Ranking { get; set; } //Mv
        public decimal FantasyRanking { get; set; } //Mf
        public int ScoredGoals { get; set; } //Gf
        public int ConcededGoals { get; set; } //Gs
        public int SavedPenalties { get; set; } //Rp
        public int TotalPenalties { get; set; } //Rc
        public int ScoredPenalties { get; set; } //R+
        public int MissedPenalties { get; set; } //R-
        public int Assists { get; set; } //Ass
        public int StationaryAssists { get; set; } //Asf
        public int YellowCards { get; set; } //Amm
        public int RedCards { get; set; } //Esp
        public int OwnGoals { get; set; } //Au
    }
}
