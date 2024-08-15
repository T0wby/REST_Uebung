using ErrorOr;
using Microsoft.EntityFrameworkCore;
using TowbyJobs.Data;
using TowbyJobs.Models;
using TowbyJobs.ServiceErrors;

namespace TowbyJobs.Services.States
{
    public class StateService : IStateService
    {
        private readonly AppDbContext _context;

        public StateService(AppDbContext context)
        {
            _context = context;
        }

        public ErrorOr<Created> CreateState(State state)
        {
            _context.States.Add(state);
            _context.SaveChanges();
            return Result.Created;
        }

        public ErrorOr<Deleted> DeletState(int id)
        {
            var state = _context.States.Find(id);
            if (state == null)
            {
                return Errors.State.NotFound;
            }
            _context.States.Remove(state);
            _context.SaveChanges();
            return Result.Deleted;
        }

        public ErrorOr<State> GetState(int id)
        {
            var state = _context.States.Find(id);
            return state == null ? Errors.State.NotFound : state;
        }

        public ErrorOr<List<State>> GetStates(int number)
        {
            if (number <= 0)
            {
                return Errors.State.OutOfScope;
            }

            if (number > _context.States.Count()) number = _context.States.Count();

            var states = _context.States.Take(number).ToList();
            return states;
        }

        public ErrorOr<UpsertedStateResult> UpsertState(State state)
        {
            var tmp = _context.States.Find(state.State_Id);
            if (tmp == null)
            {
                CreateState(state);
                return new UpsertedStateResult(false);
            }
            else
            {
                _context.States.Update(state);
                _context.SaveChanges();
                return new UpsertedStateResult(true);
            }
        }
    }
}
