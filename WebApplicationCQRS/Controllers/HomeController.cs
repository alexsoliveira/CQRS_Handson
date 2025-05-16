using System.Diagnostics;
using CQRS_Read_Application.People;
using CQRS_Read_Infrastructure.Persistence;
using CQRS_Read_Infrastructure.Persistence.People;
using CQRS_Write_Application.People;
using CQRS_Write_Domain.Commands;
using CQRS_Write_Domain.People;
using Microsoft.AspNetCore.Mvc;
using WebApplicationCQRS.Models;

namespace WebApplicationCQRS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private ICommandBus _commandBus;
        private IPersonService _personService;
        private ICommandEventRepository _commandEventRepository;
        private IContext contexto;
        private IPersonRepository _personRepository;        

        public HomeController(ILogger<HomeController> logger, 
            ICommandBus commandBus, 
            IPersonService personService, 
            ICommandEventRepository commandEventRepository, 
            IContext context, 
            IPersonRepository personRepository)
        {
            _logger = logger;
            _commandBus = commandBus;
            _personService = personService;
            _commandEventRepository = commandEventRepository;            
            _personRepository = personRepository;
            this.contexto = new Context(personRepository);
        }

        public IActionResult Index()
        {
            _commandBus.RegisterCommandHandlers(new PersonCommandHandlers(_personService, _commandEventRepository));
            _commandBus.RegisterEventHandlers(new PersonEventHandlers(_personService));

            _commandBus.Send(new PersonCreateCommand(CQRS_Write_Domain.People.PersonClass.Administrdor,
                "Teste Administrador 1", 38));
            _commandBus.Send(new PersonCreateCommand(CQRS_Write_Domain.People.PersonClass.Administrdor,
                "Teste Administrador 2", 48));
            _commandBus.Send(new PersonCreateCommand(CQRS_Write_Domain.People.PersonClass.Comum,
                "Teste Comum 1", 25));
            _commandBus.Send(new PersonCreateCommand(CQRS_Write_Domain.People.PersonClass.Comum,
                "Teste Comum 2", 50));

            var lista = _personRepository.Get();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
