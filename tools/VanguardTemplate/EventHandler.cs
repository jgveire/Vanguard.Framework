namespace $rootnamespace$.EventHandlers
{
    using $rootnamespace$.Events;
    using System;
    using Vanguard.Framework.Core.Cqrs;

    /// <summary>
    /// The $userfriendlyeventname$ event handler.
    /// </summary>
    public sealed class $eventname$EventHandler : IEventHandler<$eventname$Event>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="$eventname$EventHandler"/> class.
        /// </summary>
        public $eventname$EventHandler()
        {
        }

        /// <inheritdoc />
        public void Handle($eventname$Event domainEvent)
        {
            throw new NotImplementedException();
        }
    }
}
