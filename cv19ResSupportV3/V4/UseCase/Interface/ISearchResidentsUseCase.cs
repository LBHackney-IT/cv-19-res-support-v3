using System.Collections.Generic;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V4.Boundary.Requests;

namespace cv19ResSupportV3.V4.UseCase.Interface
{
    public interface ISearchResidentsUseCase
    {
        List<ResidentResponseBoundary> Execute(FindResident requestParams);
    }
}
