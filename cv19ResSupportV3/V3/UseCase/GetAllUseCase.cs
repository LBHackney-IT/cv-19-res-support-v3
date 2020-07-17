using cv19ResRupportV3.V1.Boundary.Response;
using cv19ResRupportV3.V1.Factories;
using cv19ResRupportV3.V1.Gateways;
using cv19ResRupportV3.V1.UseCase.Interfaces;

namespace cv19ResRupportV3.V1.UseCase
{
    //TODO: Rename class name and interface name to reflect the entity they are representing eg. GetAllClaimantsUseCase
    public class GetAllUseCase : IGetAllUseCase
    {
        private readonly IExampleGateway _gateway;
        public GetAllUseCase(IExampleGateway gateway)
        {
            _gateway = gateway;
        }

        public ResponseObjectList Execute()
        {
            return new ResponseObjectList { ResponseObjects = _gateway.GetAll().ToResponse() };
        }
    }
}
