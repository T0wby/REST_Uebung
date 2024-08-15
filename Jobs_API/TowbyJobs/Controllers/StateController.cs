using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using TowbyJobs.Contracts.Job;
using TowbyJobs.Contracts.State;
using TowbyJobs.Models;
using TowbyJobs.Services.States;

namespace TowbyJobs.Controllers
{
    public class StateController : ApiController
    {
        private readonly IStateService _stateService;

        public StateController(IStateService stateService)
        {
            _stateService = stateService;
        }

        [HttpPost()]
        public IActionResult CreateState(CreateStateRequest request)
        {
            var state = MapState(request, 0);

            if (state.IsError)
            {
                return Problem(state.Errors);
            }

            var createCountryResult = _stateService.CreateState(state.Value);

            return createCountryResult.Match(
               created => CreatedAtGetState(state.Value),
               errors => Problem(errors));
        }

        [HttpGet("{id:int}")]
        public IActionResult GetState(int id)
        {
            ErrorOr<State> getStateResult = _stateService.GetState(id);

            return getStateResult.Match(
                state => Ok(MapStateResponse(state)),
                errors => Problem(errors));
        }

        [HttpGet("getStates/{count:int}")]
        public IActionResult GetStateList(int count)
        {
            var getStateResult = _stateService.GetStates(count);

            return getStateResult.Match(
                states => Ok(MapStateListResponse(states)),
                errors => Problem(errors));
        }


        [HttpPut("{id:int}")]
        public IActionResult UpsertState(int id, UpsertStateRequest request)
        {
            var state = MapState(request, id);

            if (state.IsError)
            {
                return Problem(state.Errors);
            }

            var updateStateResult = _stateService.UpsertState(state.Value);


            return updateStateResult.Match(
                updated => updated.IsNewlyCreated ? CreatedAtGetState(state.Value) : NoContent(),
                errors => Problem(errors)
                );
        }


        [HttpDelete("{id:int}")]
        public IActionResult DeleteState(int id)
        {
            var deleteStateResult = _stateService.DeletState(id);


            return deleteStateResult.Match(
                deleted => NoContent(),
                errors => Problem(errors)
                );
        }

        #region Mappings
        private static StateResponse MapStateResponse(State state)
        {
            return new StateResponse(
                            state.State_Id,
                            state.Name,
                            state.LastTimeUpdated
                            );
        }
        private static List<StateResponse> MapStateListResponse(List<State> states)
        {
            List<StateResponse> responses = new List<StateResponse>();

            foreach (var state in states)
            {
                responses.Add(
                    new StateResponse(
                            state.State_Id,
                            state.Name,
                            state.LastTimeUpdated
                            ));
            }

            return responses;
        }

        private static ErrorOr<State> MapState(CreateStateRequest request, int id)
        {
            var state = State.Create(
                            request.Name,
                            id
                            );

            return state;
        }
        private static ErrorOr<State> MapState(UpsertStateRequest request, int id)
        {
            var state = State.Create(
                            request.Name,
                            id
                            );

            return state;
        }
        #endregion

        private CreatedAtActionResult CreatedAtGetState(State state)
        {
            return CreatedAtAction(
                   actionName: nameof(GetState),
                   routeValues: new { id = state.State_Id },
                   value: MapStateResponse(state));
        }
    }
}
