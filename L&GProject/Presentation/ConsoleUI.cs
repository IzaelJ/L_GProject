using L_GProject.DTOs;

namespace L_GProject.Presentation
{
    class ConsoleUI
    {
        public static void ShowMenu()
        {
            Console.WriteLine("=== Menu ===");
            Console.WriteLine("1) Insert a new endpoint");
            Console.WriteLine("2) Edit an existing endpoint");
            Console.WriteLine("3) Delete an existing endpoint");
            Console.WriteLine("4) List all endpoints");
            Console.WriteLine("5) Find a endpoint by 'Endpoint Serial Number'");
            Console.WriteLine("6) Exit");
        }

        public static void ShowMeterModelIdOptions()
        {
            Console.WriteLine("=== Choose the Meter Model Id ===");
            Console.WriteLine("1) NSX1P2W");
            Console.WriteLine("2) NSX1P3W");
            Console.WriteLine("3) NSX1P3W");
            Console.WriteLine("4) NSX1P3W");
        }

        public static void ShowSwitchStateOptions()
        {
            Console.WriteLine("=== Choose the Switch State ===");
            Console.WriteLine("0) Disconnected");
            Console.WriteLine("1) Connected");
            Console.WriteLine("2) Armed");
        }
        public static void DeleteConfirmation(string serialNumber)
        {
            Console.WriteLine($"Are you sure that you want to delete the endpoint with the serial: '{serialNumber}'?");
            Console.WriteLine("1) Yes");
            Console.WriteLine("2) No");
        }

        public static void ConfirmWindow()
        {
            Console.WriteLine($"Are you sure?");
            Console.WriteLine("1) Yes");
            Console.WriteLine("2) No");
        }

        public static void ShowEndpoint(EndpointDTO endpoint)
        {
             Console.WriteLine($"Serial Number: {endpoint.SerialNumber}");
             Console.WriteLine($"Meter Model ID: {endpoint.MeterModelId}");
             Console.WriteLine($"Meter Number: {endpoint.MeterNumber}");
             Console.WriteLine($"Meter Firmware Version: {endpoint.MeterFirmwareVersion}");
             Console.WriteLine($"Switch State: {endpoint.SwitchState}");
             Console.WriteLine("-----------------------------------");

        }
            public static string GetUserChoice(string choices)
        {
            Console.Write($"Enter your choice ({choices}): ");
            return Console.ReadLine();
        }
    }
}
