namespace Api.Interfaces
{
    public interface IVoteService
    {
        string GetWinner();
        void AddVoteFor(string name);
        int TotalVotesProcessed();
    }
}
