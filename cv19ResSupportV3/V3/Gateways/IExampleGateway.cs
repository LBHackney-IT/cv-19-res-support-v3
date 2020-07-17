using System.Collections.Generic;
using cv19ResRupportV3.V1.Domain;

namespace cv19ResRupportV3.V1.Gateways
{
    public interface IExampleGateway
    {
        Entity GetEntityById(int id);

        List<Entity> GetAll();
    }
}
