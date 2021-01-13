using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.Factories.Commands;
using cv19ResSupportV3.V3.UseCase.Interfaces;

namespace cv19ResSupportV3.V3.UseCase
{
    public class CreateResidentAndHelpRequestUseCase : ICreateResidentAndHelpRequestUseCase
    {
        private readonly IFindResidentUseCase _findResidentUseCase;
        private readonly ICreateResidentUseCase _createResidentUseCase;
        private readonly ICreateHelpRequestUseCase _createHelpRequestUseCase;
        public CreateResidentAndHelpRequestUseCase(IFindResidentUseCase findResidentUseCase, ICreateResidentUseCase createResidentUseCase, ICreateHelpRequestUseCase createHelpRequestUseCase)
        {
            _findResidentUseCase = findResidentUseCase;
            _createResidentUseCase = createResidentUseCase;
            _createHelpRequestUseCase = createHelpRequestUseCase;
        }
        public int Execute(CreateResidentAndHelpRequest command)
        {
            // resident = _findResidentUseCase.Execute(command);
            //if resident
            // update resident
            //else
            //create resident
            _createResidentUseCase.Execute(command.ToCreateResidentCommand());
            return _createHelpRequestUseCase.Execute(command.ToCreateHelpRequestCommand());
        }
    }
}
