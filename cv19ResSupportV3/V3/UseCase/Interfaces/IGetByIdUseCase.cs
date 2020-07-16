using cv19ResRupportV3.V1.Boundary.Response;

namespace cv19ResRupportV3.V1.UseCase.Interfaces
{
    public interface IGetByIdUseCase
    {
        ResponseObject Execute(int id);
    }
}
