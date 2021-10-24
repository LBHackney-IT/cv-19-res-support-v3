using cv19ResSupportV3.V3.Gateways;
using cv19ResSupportV3.V4.UseCase.Interface;

namespace cv19ResSupportV3.V4.UseCase
{
    public class DeleteCallHandlerUseCase : IDeleteCallHandlerUseCase
    {
        private readonly ICallHandlerGateway _callHandlerGateway;

        public DeleteCallHandlerUseCase(ICallHandlerGateway residentGateway)
        {
            _callHandlerGateway = residentGateway;
        }

        public bool Execute(int id)
            => _callHandlerGateway.DeleteCallHandler(id);
    }
}
