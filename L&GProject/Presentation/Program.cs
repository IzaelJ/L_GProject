using L_GProject.Data.Repository;
using L_GProject.Presentation;
using L_GProject.Service;
using L_GProject.Service.Impl;

namespace LGProject.Presentation
{
    class Program
    {
        private readonly IEndpointService _endpointService;

        public Program(IEndpointService endpointService)
        {
            _endpointService = endpointService;
        }
        static void Main(string[] args)
        {
            IEndpointRepository endpointRepository = new EndpointRepository();
            IEndpointService endpointService = new EndpointServiceImpl(endpointRepository);
            Program program = new Program(endpointService);
            Controller controller = new Controller(endpointService);
            program.Run(controller);

        }
        private void Run(Controller controller)
        {
            while (true)
            {
                ConsoleUI.ShowMenu();

                string input = ConsoleUI.GetUserChoice("1-6");

                switch (input)
                {
                    case "1":
                        Console.WriteLine("\nInsert option selected.");
                        controller.InsertAction();
                        break;
                    case "2":
                        Console.WriteLine("\nEdit option selected.");
                        controller.EditSwitchState();
                        break;
                    case "3":
                        Console.WriteLine("\nDelete option selected.");
                        controller.DeleteEndpoint();
                        break;
                    case "4":
                        Console.WriteLine("\nList All option selected.");
                        controller.ListAllEndpoints();
                        break;
                    case "5":
                        Console.WriteLine("\nFind By Serial option selected.");
                        controller.RetrieveEndpointBySerialNumber();
                        break;
                    case "6":
                        Console.WriteLine("\nExiting the program...");
                        return;
                    default:
                        Console.WriteLine("\nInvalid choice. Please enter a number between 1 and 6.");
                        break;
                }

                Console.WriteLine();
            }
        }
    }
}
