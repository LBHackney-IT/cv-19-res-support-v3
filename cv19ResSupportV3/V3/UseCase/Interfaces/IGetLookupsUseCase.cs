using System.Collections.Generic;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Domain.Queries;

namespace cv19ResSupportV3.V3.UseCase
{
    public interface IGetLookupsUseCase
    {
        List<LookupDomain> Execute(LookupQuery requestParams);
    }
}
