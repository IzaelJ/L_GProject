using L_GProject.Models;

namespace L_GProject.Data.Repository
{
    public interface IEndpointRepository
    {
        Endpoint FindEndpointBySerialNumber(string serialNumber);
        IEnumerable<Endpoint> GetAllEndpoints();
        void AddEndpoint(Endpoint endpoint);
        void UpdateEndpoint(string serialNumber, int switchState);
        void DeleteEndpoint(string serialNumber);
    }
}
