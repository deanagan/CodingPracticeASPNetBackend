namespace Api.Interfaces
{
    public interface IVoteService
    {
        void AddVoteFor(int id, int votes);
        void AddVoteFor(int id);
    }
}
