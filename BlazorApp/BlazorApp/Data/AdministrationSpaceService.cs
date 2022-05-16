namespace BlazorApp.Data
{
    using Microsoft.AspNetCore.Components;
    using DataLibrary;
    using BlazorApp.Models;
    using BlazorApp.Pages;

    public class AdministrationSpaceService
    {

        private IDataAccess? _data;
        private IConfiguration? _config;

        List<HistoryModel> history;
        List<HistoryModel> allHistory;
        public int MaxPageNumber { get; set; }

        private async Task GetHistory(int page)
        {
            string sql = "select * from history ORDER BY idHistory DESC LIMIT 10 OFFSET "+page*10;

            history = await _data.LoadData<HistoryModel, dynamic>(sql, new { }, _config.GetConnectionString("default"));

        }

        public async Task GetMaxPageNumber()
        {
            string sql = "select * from history";

            allHistory = await _data.LoadData<HistoryModel, dynamic>(sql, new { }, _config.GetConnectionString("default"));
            MaxPageNumber = (int) Math.Ceiling((float) allHistory.Count()/10);
        }

        public Task<HistoryModel[]> GetPersonalSpaceAsync(IDataAccess data, IConfiguration config, int page)
        {
            page--;
            _data = data;
            _config = config;
            GetHistory(page);
            GetMaxPageNumber();
            if (history != null)
            {
                return Task.FromResult(Enumerable.Range(0, history.Count).Select(index => new HistoryModel
                {
                    IALevel = history[index].IALevel,
                    PlayerPseudo = history[index].PlayerPseudo,
                    VictoryForPlayer = history[index].VictoryForPlayer,
                    IAShoot = history[index].IAShoot,
                    PlayerShoot = history[index].PlayerShoot,
                    Begin = history[index].Begin,
                    End = history[index].End,
                    GameTime = history[index].End.Subtract(history[index].Begin)
                    
                }).ToArray());
            }
            else
            {
                return Task.FromResult(new HistoryModel[0]);
            }
            
        }
    }
}