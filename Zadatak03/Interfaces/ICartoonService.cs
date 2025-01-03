using System.ServiceModel;

namespace Zadatak03.Interfaces
{
    [ServiceContract]
    public interface ICartoonService
    {
        [OperationContract]
        string QueryCartoons(string xpathQuery);
    }
}
