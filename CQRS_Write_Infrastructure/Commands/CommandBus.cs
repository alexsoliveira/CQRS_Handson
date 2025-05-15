using CQRS_Write_Domain.Commands;
using CQRS_Write_Domain.Events;
using System.Reflection;

namespace CQRS_Write_Infrastructure.Commands
{
    public class CommandBus : ICommandBus
    {
        private static Dictionary<Type, List<Action<ICommand>>> commandHandlerDictionary = new Dictionary<Type, List<Action<ICommand>>>();
        private static Dictionary<Type, List<Action<IEvent>>> eventHandlerDictionary = new Dictionary<Type, List<Action<IEvent>>>();

        public void Publish<T>(T @event) where T : IEvent
        {
            List<Action<IEvent>> eventActions;
            if (eventHandlerDictionary.TryGetValue(@event.GetType(), out eventActions))
            {
                foreach (var eventHandlerMethod in eventActions)
                {
                    eventHandlerMethod(@event);
                }
            }
            else
            {
                throw new InvalidOperationException($"Evento não foi encontrado {@event}");
            }
        }

        public void Send<T>(T command) where T : ICommand
        {
            List<Action<ICommand>> commandActions;
            if (commandHandlerDictionary.TryGetValue(command.GetType(), out commandActions))
            {
                foreach (var commandHandlerMethod in commandActions)
                {
                    commandHandlerMethod(command);
                }
            }
            else
            {
                throw new InvalidOperationException($"Comando não foi encontrado {command}");
            }
        }

        public void RegisterCommandHandlers(ICommandHandler commandHandler)
        {
            var commandHandlerMethod = commandHandler.GetType().GetMethods()
                .Where(m => m.GetParameters()
                .Any(p => p.ParameterType.GetInterfaces().Contains(typeof(ICommand))));
            foreach (var method in commandHandlerMethod)
            {
                ParameterInfo commandParameterInfo = method.GetParameters()
                    .Where(p => p.ParameterType.GetInterfaces().Contains(typeof(ICommand))).FirstOrDefault();
                if (commandParameterInfo == null) continue;

                Type commandParameterType = commandParameterInfo.ParameterType;

                List<Action<ICommand>> commandActions;
                if(!commandHandlerDictionary.TryGetValue(commandParameterType, out commandActions))
                {
                    commandActions = new List<Action<ICommand>>();
                    commandHandlerDictionary.Add(commandParameterType, commandActions);
                }

                commandActions.Add(x => method.Invoke(commandHandler, new object[] { x }));
            }
        }

        public void RegisterEventHandlers(IEventHandler eventHandler)
        {
            var eventHandlerMethod = eventHandler.GetType().GetMethods()
                .Where(m => m.GetParameters()
                .Any(p => p.ParameterType.GetInterfaces().Contains(typeof(IEvent))));
            foreach (var method in eventHandlerMethod)
            {
                ParameterInfo eventParameterInfo = method.GetParameters()
                    .Where(p => p.ParameterType.GetInterfaces().Contains(typeof(IEvent))).FirstOrDefault();
                if (eventParameterInfo == null) continue;

                Type eventParameterType = eventParameterInfo.ParameterType;

                List<Action<IEvent>> eventActions;
                if (!eventHandlerDictionary.TryGetValue(eventParameterType, out eventActions))
                {
                    eventActions = new List<Action<IEvent>>();
                    eventHandlerDictionary.Add(eventParameterType, eventActions);
                }

                eventActions.Add(x => method.Invoke(eventHandler, new object[] { x }));
            }
        }        
    }
}
