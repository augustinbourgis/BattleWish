namespace BlazorApp.Data
{
    using Microsoft.AspNetCore.Components;
    using DataLibrary;
    using BlazorApp.Models;
    using BlazorApp.Pages;

    public class PersonalSpaceService
    {

        private IDataAccess? _data;
        private IConfiguration? _config;

        List<HistoryModel> history;
        List<HistoryModel> allHistory;
        public int MaxPageNumber { get; set; }

        private async Task GetHistory(string login, int page)
        {
            string sql = "select * from history where PlayerPseudo like'" + login + "' ORDER BY idHistory DESC LIMIT 10 OFFSET "+page*10;

            history = await _data.LoadData<HistoryModel, dynamic>(sql, new { }, _config.GetConnectionString("default"));

        }

        public async Task GetMaxPageNumber(string login)
        {
            string sql = "select * from history where PlayerPseudo like'" + login + "'";

            allHistory = await _data.LoadData<HistoryModel, dynamic>(sql, new { }, _config.GetConnectionString("default"));
            MaxPageNumber = (int) Math.Ceiling((float) allHistory.Count()/10);
        }

        public Task<PersonalSpaceModel[]> GetPersonalSpaceAsync(string login, IDataAccess data, IConfiguration config, int page)
        {
            page--;
            _data = data;
            _config = config;
            GetHistory(login, page);
            GetMaxPageNumber(login);
            if (history != null)
            {
                return Task.FromResult(Enumerable.Range(0, history.Count).Select(index => new PersonalSpaceModel
                {
                    IALevel = history[index].IALevel,
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
                return Task.FromResult(new PersonalSpaceModel[0]);
            }
            
        }
    }
}