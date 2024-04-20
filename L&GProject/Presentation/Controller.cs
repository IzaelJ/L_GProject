using L_GProject.DTOs;
using L_GProject.Service;
using L_GProject.Service.Exceptions;
namespace L_GProject.Presentation
{
    public class Controller
    {
        public enum SwitchState
        {
            Disconnected = 0,
            Connected = 1,
            Armed = 2
        }

        private readonly IEndpointService _endpointService;

        public Controller(IEndpointService endpointService)
        {
            _endpointService = endpointService;
        }
        public void InsertAction()
        {
            EndpointDTO endpointDTO = new EndpointDTO();
            do
            {
                Console.WriteLine("\nWrite the Endpoint Serial Number: ");
                endpointDTO.SerialNumber = Console.ReadLine();
            } while (string.IsNullOrEmpty(endpointDTO.SerialNumber));

            string[] validModelIds = { "16", "17", "18", "19" };
            bool isValidModelId = false;

            do
            {
                ConsoleUI.ShowMeterModelIdOptions();

                string input = Console.ReadLine();

                if (int.TryParse(input, out int modelIdIndex) && modelIdIndex >= 1 && modelIdIndex <= 4)
                {
                    endpointDTO.MeterModelId = int.Parse(validModelIds[modelIdIndex - 1]);
                    isValidModelId = true;
                    Console.WriteLine($"\nMeter Model Id value set to {endpointDTO.MeterModelId}.");
                }
                else
                {
                    Console.WriteLine("\nInvalid choice. Please enter a number between 1 and 4.");
                }
            } while (!isValidModelId);

            do
            {
                Console.WriteLine("\nWrite the Meter Number: ");
                string meterNumberString = Console.ReadLine();

                if (int.TryParse(meterNumberString, out int meterNumber))
                {
                    endpointDTO.MeterNumber = meterNumber; 
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer value.");
                }
            } while (true);

            do
            {
                Console.WriteLine("\nWrite the Firmware Version: ");
                endpointDTO.MeterFirmwareVersion = Console.ReadLine();
            } while (string.IsNullOrEmpty(endpointDTO.MeterFirmwareVersion));

            bool isValidSwitchState = false;

            do
            {
                ConsoleUI.ShowSwitchStateOptions();
                string input = Console.ReadLine();

                if (Enum.TryParse(input, out SwitchState switchState))
                {
                    if (Enum.IsDefined(typeof(SwitchState), switchState))
                    {
                        endpointDTO.SwitchState = (int)switchState;
                        isValidSwitchState = true;
                        Console.WriteLine($"\nSwitch State value set to {switchState}.");
                    }
                    else
                    {
                        Console.WriteLine("\nInvalid choice. Please enter a valid switch state.");
                    }
                }
                else
                {
                    Console.WriteLine("\nInvalid choice. Please enter a valid switch state.");
                }
            } while (!isValidSwitchState);

            try
            {
                _endpointService.InsertEndpoint(endpointDTO);
                Console.WriteLine("Endpoint registered with success.");

            }
            catch(SerialNumberAlreadyExistsException e) {
                Console.WriteLine(e.Message);
            }
        }
        public void EditSwitchState()
        {
            string serialNumber;
            do
            {
                Console.WriteLine("\nWrite the Endpoint Serial Number: ");
                serialNumber = Console.ReadLine();

                try
                {
                    _endpointService.FindEndPointBySerialNumber(serialNumber);
                }

                catch (SerialNumberNotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }

            } while (string.IsNullOrEmpty(serialNumber));

            bool isValidSwitchState = false;
            do
            {
                ConsoleUI.ShowSwitchStateOptions();
                string input = Console.ReadLine();

                if (Enum.TryParse(input, out SwitchState switchState))
                {
                    if (Enum.IsDefined(typeof(SwitchState), switchState))
                    {
                        _endpointService.EditEndpoint(serialNumber, (int)switchState);
                        isValidSwitchState = true;
                        Console.WriteLine($"\nSwitch State value set to {switchState}.");
                    }
                    else
                    {
                        Console.WriteLine("\nInvalid choice. Please enter a valid switch state.");
                    }
                }
                else
                {
                    Console.WriteLine("\nInvalid choice. Please enter a valid switch state.");
                }
            } while (!isValidSwitchState);
        }

        public void DeleteEndpoint()
        {
            string serialNumber;
            do
            {
                Console.WriteLine("\nWrite the Endpoint Serial Number: ");
                serialNumber = Console.ReadLine();

                try
                {
                    _endpointService.FindEndPointBySerialNumber(serialNumber);
                }

                catch (SerialNumberNotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }

            } while (string.IsNullOrEmpty(serialNumber));
            
            bool validAnswer = false;
            do
            {
                ConsoleUI.DeleteConfirmation(serialNumber);
                string input = Console.ReadLine();

                if (input.Equals("1"))
                {
                    _endpointService.DeleteEndpoint(serialNumber);
                    Console.WriteLine("Endpoint deleted.");
                    validAnswer = true;
                }
                else if (input.Equals("2"))
                {
                    Console.WriteLine("\nReturning...");
                    validAnswer = true;
                    return;
                }
                else {
                    Console.WriteLine("\nInvalid choice. Please enter a valid switch state.");
                }
            } while (!validAnswer);
        }

        public void ListAllEndpoints()
        {
            IEnumerable<EndpointDTO> endpoints = _endpointService.ListAllEndpoints();
            Console.WriteLine("\n===List of Endpoints:===");
            endpoints.ToList().ForEach(endpoint => ConsoleUI.ShowEndpoint(endpoint));
        }

        public void RetrieveEndpointBySerialNumber()
        {
            EndpointDTO endpointDTO = new EndpointDTO();
            do
            {
                Console.WriteLine("\nWrite the Endpoint Serial Number: ");
                endpointDTO.SerialNumber = Console.ReadLine();
                try
                {
                   Console.WriteLine("\n===Endpoint:==="); 
                   endpointDTO = _endpointService.FindEndPointBySerialNumber(endpointDTO.SerialNumber);
                   ConsoleUI.ShowEndpoint(endpointDTO);
                }

                catch (SerialNumberNotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }

            } while (string.IsNullOrEmpty(endpointDTO.SerialNumber));
        }

    }
}
