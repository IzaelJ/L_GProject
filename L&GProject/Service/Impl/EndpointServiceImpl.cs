using L_GProject.Data.Repository;
using L_GProject.DTOs;
using L_GProject.Models;
using L_GProject.Service.Exceptions;

namespace L_GProject.Service.Impl
{
    public class EndpointServiceImpl : IEndpointService
    {
        private readonly IEndpointRepository _endpointRepository;

        public EndpointServiceImpl(IEndpointRepository endpointRepository)
        {
            _endpointRepository = endpointRepository;
        }

        public void DeleteEndpoint(string serialNumber)
        {
            _endpointRepository.DeleteEndpoint(serialNumber);
        }

        public void EditEndpoint(string serialNumber, int switchState)
        {
            _endpointRepository.UpdateEndpoint(serialNumber, switchState);
        }

        public EndpointDTO FindEndPointBySerialNumber(string serialNumber)
        {
            Endpoint endpoint = _endpointRepository.FindEndpointBySerialNumber(serialNumber);
            if (endpoint == null)
            {
                throw new SerialNumberNotFoundException($"The serial number '{serialNumber}' was not found.");
            }

            return MapEndpointEntityToEndpointDto(endpoint);
        }

        public void InsertEndpoint(EndpointDTO endpoint)
        {
            Endpoint existingEndpoint = _endpointRepository.FindEndpointBySerialNumber(endpoint.SerialNumber);
            if (existingEndpoint != null)
            {
                throw new SerialNumberAlreadyExistsException($"\nA endpoint with the serial number: '{endpoint.SerialNumber}' is already registered.");
            }
            _endpointRepository.AddEndpoint(MapEndpointDtoToEndpointEntity(endpoint));

        }

        public IEnumerable<EndpointDTO> ListAllEndpoints()
        {
            IEnumerable<Endpoint> endpoints = _endpointRepository.GetAllEndpoints();

            IEnumerable<EndpointDTO> endpointDTOs = endpoints.Select(endpoint => MapEndpointEntityToEndpointDto(endpoint));

            return endpointDTOs;
        }

        private Endpoint MapEndpointDtoToEndpointEntity(EndpointDTO endpoint)
        {
            Endpoint endpointEntity = new Endpoint();
            endpointEntity.SerialNumber = endpoint.SerialNumber;
            endpointEntity.MeterModelId = endpoint.MeterModelId;
            endpointEntity.MeterNumber = endpoint.MeterNumber;
            endpointEntity.MeterFirmwareVersion = endpoint.MeterFirmwareVersion;
            endpointEntity.SwitchState = endpoint.SwitchState;
            return endpointEntity;
        }
        private EndpointDTO MapEndpointEntityToEndpointDto(Endpoint endpoint)
        {
            EndpointDTO endpointDto = new EndpointDTO();
            endpointDto.SerialNumber = endpoint.SerialNumber;
            endpointDto.MeterModelId = endpoint.MeterModelId;
            endpointDto.MeterNumber = endpoint.MeterNumber;
            endpointDto.MeterFirmwareVersion = endpoint.MeterFirmwareVersion;
            endpointDto.SwitchState = endpoint.SwitchState;
            return endpointDto;
        }
    }
}
