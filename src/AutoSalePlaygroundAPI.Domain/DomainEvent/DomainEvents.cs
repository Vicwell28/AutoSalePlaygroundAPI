using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSalePlaygroundAPI.Domain.DomainEvent
{
    public static class DomainEvents
    {
        private static List<IDomainEvent> _events = new List<IDomainEvent>();

        public static IReadOnlyCollection<IDomainEvent> Events => _events.AsReadOnly();

        public static void Raise(IDomainEvent domainEvent)
        {
            _events.Add(domainEvent);
        }

        public static void Clear() => _events.Clear();
    }

    public interface IDomainEvent
    {
        DateTime OccurredOn { get; }
    }
}
