namespace $rootnamespace$.CommandHandlers
{
    using $rootnamespace$.Commands;
    using System;
    using Vanguard.Framework.Core.Cqrs;

    /// <summary>
    /// The $userfriendlycommandname$ command handler.
    /// </summary>
    public sealed class $commandname$CommandHandler : ICommandHandler<$commandname$Command>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="$commandname$CommandHandler"/> class.
        /// </summary>
        public $commandname$CommandHandler()
        {
        }

        /// <inheritdoc />
        public void Execute($commandname$Command command)
        {
            throw new NotImplementedException();
        }
    }
}
