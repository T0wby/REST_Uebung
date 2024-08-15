using ErrorOr;
using TowbyJobs.Models;

namespace TowbyJobs.Services.States
{
    public interface IStateService
    {
        ErrorOr<Created> CreateState(State state);
        ErrorOr<State> GetState(int id);
        ErrorOr<List<State>> GetStates(int number);
        ErrorOr<UpsertedStateResult> UpsertState(State state);
        ErrorOr<Deleted> DeletState(int id);
    }
}
