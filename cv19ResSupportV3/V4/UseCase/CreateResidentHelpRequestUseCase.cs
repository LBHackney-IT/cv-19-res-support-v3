using cv19ResSupportV3.V3.Gateways;
using cv19ResSupportV3.V4.Boundary.Requests;
using cv19ResSupportV3.V4.Boundary.Response;
using cv19ResSupportV3.V4.Factories;
using cv19ResSupportV3.V4.UseCase.Interface;

namespace cv19ResSupportV3.V4.UseCase
{
    public class CreateResidentHelpRequestUseCase : ICreateResidentHelpRequestUseCase
    {
        private readonly IHelpRequestGateway _gateway;

        public CreateResidentHelpRequestUseCase(IHelpRequestGateway gateway)
        {
            _gateway = gateway;
        }
        public int Execute(int id, ResidentHelpRequestRequest request)
        {
            var gwResponse = _gateway.CreateHelpRequest(id, request.ToCommand());
            return gwResponse;
        }
    }
}
