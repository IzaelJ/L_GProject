using L_GProject.DTOs;

namespace L_GProject.Service
{
    public interface IEndpointService
    {
        EndpointDTO FindEndPointBySerialNumber(string serialNumber);
        IEnumerable<EndpointDTO> ListAllEndpoints();
        void InsertEndpoint(EndpointDTO endpoint);
        void EditEndpoint(string serialNumber, int switchState);
        void DeleteEndpoint(string serialNumber);
    }
}
