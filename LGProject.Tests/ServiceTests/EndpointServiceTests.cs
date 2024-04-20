using L_GProject.Data.Repository;
using L_GProject.DTOs;
using L_GProject.Models;
using L_GProject.Service;
using L_GProject.Service.Exceptions;
using L_GProject.Service.Impl;
using Moq;

namespace LGProject.Tests.ServiceTests
{
    public class EndpointServiceTests
    {
        private readonly Mock<IEndpointRepository> _endpointRepositoryMock;
        private readonly IEndpointService _endpointService;

        public EndpointServiceTests()
        {
            _endpointRepositoryMock = new Mock<IEndpointRepository>();
            _endpointService = new EndpointServiceImpl(_endpointRepositoryMock.Object);
        }

        [Fact]
        public void InsertEndpoint_WhenSerialNumberDoesNotExist_ShouldAddEndpoint()
        {
            _endpointRepositoryMock.Setup(repo => repo.FindEndpointBySerialNumber(It.IsAny<string>())).Returns((Endpoint)null);

            var endpointDto = new EndpointDTO
            {
                SerialNumber = "123456",
                MeterModelId = 1,
                MeterNumber = 100,
                MeterFirmwareVersion = "1.0.0",
                SwitchState = 1
            };

            _endpointService.InsertEndpoint(endpointDto);

            _endpointRepositoryMock.Verify(repo => repo.AddEndpoint(It.IsAny<Endpoint>()), Times.Once);
        }

        [Fact]
        public void InsertEndpoint_WhenSerialNumberExists_ShouldThrowSerialNumberAlreadyExistsException()
        {
            _endpointRepositoryMock.Setup(repo => repo.FindEndpointBySerialNumber(It.IsAny<string>())).Returns(new Endpoint());

            var endpointDto = new EndpointDTO
            {
                SerialNumber = "123456",
                MeterModelId = 1,
                MeterNumber = 100,
                MeterFirmwareVersion = "1.0.0",
                SwitchState = 1
            };

            Assert.Throws<SerialNumberAlreadyExistsException>(() => _endpointService.InsertEndpoint(endpointDto));
        }

        [Fact]
        public void DeleteEndpoint_ShouldCallDeleteEndpointInRepository()
        {
            string serialNumberToDelete = "123456";

            _endpointService.DeleteEndpoint(serialNumberToDelete);

            _endpointRepositoryMock.Verify(repo => repo.DeleteEndpoint(serialNumberToDelete), Times.Once);
        }

        [Fact]
        public void EditEndPoint_ShouldCallUpdateEndpointWithCorrectArguments()
        {
            string serialNumber = "123456";
            int switchState = 1;

            _endpointService.EditEndpoint(serialNumber, switchState);

            _endpointRepositoryMock.Verify(repo => repo.UpdateEndpoint(serialNumber, switchState), Times.Once);
        }

        [Fact]
        public void FindEndPointBySerialNumber_WhenEndpointExists_ReturnsEndpointDto()
        {
            var serialNumber = "123456";
            var endpointEntity = new Endpoint
            {
                SerialNumber = serialNumber,
                MeterModelId = 1,
                MeterNumber = 100,
                MeterFirmwareVersion = "1.0.0",
                SwitchState = 1
            };

            _endpointRepositoryMock.Setup(repo => repo.FindEndpointBySerialNumber(serialNumber)).Returns(endpointEntity);

            var result = _endpointService.FindEndPointBySerialNumber(serialNumber);

            Assert.NotNull(result);
            Assert.Equal(serialNumber, result.SerialNumber);
        }

        [Fact]
        public void FindEndPointBySerialNumber_WhenEndpointDoesNotExist_ThrowsSerialNumberNotFoundException()
        {
            var serialNumber = "123456";
            _endpointRepositoryMock.Setup(repo => repo.FindEndpointBySerialNumber(serialNumber)).Returns((Endpoint)null);

            Assert.Throws<SerialNumberNotFoundException>(() => _endpointService.FindEndPointBySerialNumber(serialNumber));
        }

        [Fact]
        public void ListAllEndpoints_ReturnsMappedEndpointDTOs()
        {
            var endpointEntities = new List<Endpoint>
            {
                new()
                {
                    SerialNumber = "123456",
                    MeterModelId = 1,
                    MeterNumber = 100,
                    MeterFirmwareVersion = "1.0.0",
                    SwitchState = 1
                },
                new() {
                    SerialNumber = "789012",
                    MeterModelId = 2,
                    MeterNumber = 200,
                    MeterFirmwareVersion = "2.0.0",
                    SwitchState = 0
                }
            };

            var expectedEndpointDTOs = new List<EndpointDTO>
            {
                new()
                {
                    SerialNumber = "123456",
                    MeterModelId = 1,
                    MeterNumber = 100,
                    MeterFirmwareVersion = "1.0.0",
                    SwitchState = 1
                },
                new()
                {
                    SerialNumber = "789012",
                    MeterModelId = 2,
                    MeterNumber = 200,
                    MeterFirmwareVersion = "2.0.0",
                    SwitchState = 0
                }
            };

            _endpointRepositoryMock.Setup(repo => repo.GetAllEndpoints()).Returns(endpointEntities);

            var result = _endpointService.ListAllEndpoints();

            Assert.NotNull(result);
            Assert.Equal(expectedEndpointDTOs.Count, result.Count());

            foreach (var expectedEndpointDTO in expectedEndpointDTOs)
            {
                var actualEndpointDTO = result.FirstOrDefault(e =>
                    e.SerialNumber == expectedEndpointDTO.SerialNumber &&
                    e.MeterModelId == expectedEndpointDTO.MeterModelId &&
                    e.MeterNumber == expectedEndpointDTO.MeterNumber &&
                    e.MeterFirmwareVersion == expectedEndpointDTO.MeterFirmwareVersion &&
                    e.SwitchState == expectedEndpointDTO.SwitchState);

                Assert.NotNull(actualEndpointDTO);
            }
        }
    }


}
