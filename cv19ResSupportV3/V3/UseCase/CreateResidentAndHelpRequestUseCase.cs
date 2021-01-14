using System;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.Factories.Commands;
using cv19ResSupportV3.V3.UseCase.Interfaces;

namespace cv19ResSupportV3.V3.UseCase
{
    public class CreateResidentAndHelpRequestUseCase : ICreateResidentAndHelpRequestUseCase
    {
        private readonly ICreateResidentUseCase _createResidentUseCase;
        private readonly ICreateHelpRequestUseCase _createHelpRequestUseCase;

        public CreateResidentAndHelpRequestUseCase(ICreateResidentUseCase createResidentUseCase, ICreateHelpRequestUseCase createHelpRequestUseCase)
        {
            _createResidentUseCase = createResidentUseCase;
            _createHelpRequestUseCase = createHelpRequestUseCase;
        }
        public int Execute(CreateResidentAndHelpRequest command)
        {
            var resident = _createResidentUseCase.Execute(command.ToCreateResidentCommand());

            return  _createHelpRequestUseCase.Execute(resident.Id, command.ToCreateHelpRequestCommand());

        }
    }
}
