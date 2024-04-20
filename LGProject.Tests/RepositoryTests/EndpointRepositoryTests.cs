
using L_GProject.Data.Repository;
using L_GProject.Models;

namespace LGProject.Tests.RepositoryTests
{
    public class EndpointRepositoryTests
    {
        [Fact]
        public void AddEndpoint_ShouldAddToDictionary()
        {
            var repository = new EndpointRepository();
            var endpoint = new Endpoint { SerialNumber = "123456" };

            repository.AddEndpoint(endpoint);

            Assert.Contains(endpoint, repository.GetAllEndpoints());
        }

        [Fact]
        public void DeleteEndpoint_ShouldRemoveFromDictionary()
        {
            var repository = new EndpointRepository();
            var endpoint = new Endpoint { SerialNumber = "123456" };
            repository.AddEndpoint(endpoint);

            repository.DeleteEndpoint(endpoint.SerialNumber);

            Assert.DoesNotContain(endpoint, repository.GetAllEndpoints());
        }

        [Fact]
        public void FindEndpointBySerialNumber_ExistingSerialNumber_ShouldReturnEndpoint()
        {
            var repository = new EndpointRepository();
            var endpoint = new Endpoint { SerialNumber = "123456" };
            repository.AddEndpoint(endpoint);

            var result = repository.FindEndpointBySerialNumber("123456");

            Assert.NotNull(result);
            Assert.Equal(endpoint, result);
        }

        [Fact]
        public void FindEndpointBySerialNumber_NonExistingSerialNumber_ShouldReturnNull()
        {
            var repository = new EndpointRepository();

            var result = repository.FindEndpointBySerialNumber("NonExistingSerialNumber");

            Assert.Null(result);
        }

        [Fact]
        public void UpdateEndpoint_ShouldUpdateSwitchState()
        {
            var repository = new EndpointRepository();
            var endpoint = new Endpoint { SerialNumber = "123456", SwitchState = 1 };
            repository.AddEndpoint(endpoint);

            repository.UpdateEndpoint("123456", 0);

            var updatedEndpoint = repository.FindEndpointBySerialNumber("123456");
            Assert.Equal(0, updatedEndpoint.SwitchState);
        }
    }
}
