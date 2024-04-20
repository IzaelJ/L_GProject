using L_GProject.Models;
namespace L_GProject.Data.Repository
{
    public class EndpointRepository : IEndpointRepository


    {
        private Dictionary<string, Endpoint> _endpoints;

        public EndpointRepository()
        {
            _endpoints = new Dictionary<string, Endpoint>();
        }

        public void AddEndpoint(Endpoint endpoint)
        {
            _endpoints.Add(endpoint.SerialNumber, endpoint);
        }

        public void DeleteEndpoint(string serialNumber)
        {
            _endpoints.Remove(serialNumber);
        }

        public Endpoint FindEndpointBySerialNumber(string serialNumber)
        {
            return _endpoints.TryGetValue(serialNumber, out Endpoint? value) ? value : null;
        }

        public IEnumerable<Endpoint> GetAllEndpoints()
        {
            return _endpoints.Values;
        }

        public void UpdateEndpoint(string serialNumber, int switchState)
        {
            _endpoints.TryGetValue(serialNumber, out var endpoint);
            endpoint.SwitchState = switchState;
            _endpoints[serialNumber] = endpoint;
        }
    }
}
