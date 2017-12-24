using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Vanguard.Framework.Core.Cqrs;
using Vanguard.Framework.Core.Repositories;

namespace Vanguard.Framework.Http
{
    /// <inheritdoc />
    public class CrudController<TModel> : CrudController<Guid, TModel>
        where TModel : IUniqueEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CrudController{TModel}"/> class.
        /// </summary>
        /// <param name="commandDispatcher">The command dispatcher.</param>
        /// <param name="queryDispatcher">The query dispatcher.</param>
        public CrudController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
            : base(commandDispatcher, queryDispatcher)
        {
        }

        /// <inheritdoc />
        [HttpGet("{id:guid}")]
        public override IActionResult GetById([FromRoute]Guid id)
        {
            return base.GetById(id);
        }

        /// <inheritdoc />
        [HttpPut("{id:guid}")]
        public override IActionResult Update([FromRoute]Guid id, [FromBody, Required]TModel model)
        {
            return base.Update(id, model);
        }

        /// <inheritdoc />
        [HttpDelete("{id:guid}")]
        public override IActionResult Delete([FromRoute]Guid id)
        {
            return base.Delete(id);
        }
    }
}
