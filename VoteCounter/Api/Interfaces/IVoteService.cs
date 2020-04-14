namespace Api.Interfaces
{
    public interface IVoteService
    {
        string GetWinner();
        void AddVoteFor(int id, int votes);
        void AddVoteFor(int id);
    }
}
