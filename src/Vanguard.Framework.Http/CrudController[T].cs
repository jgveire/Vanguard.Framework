namespace Vanguard.Framework.Http
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Mvc;
    using Vanguard.Framework.Core.Cqrs;
    using Vanguard.Framework.Core.Models;

    /// <summary>
    /// The Create, Read, Update and Delete (CRUD) controller.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public class CrudController<TModel> : CrudController<Guid, TModel>
        where TModel : IUniqueModel
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

        /// <summary>
        /// Gets an item by its identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// An <see cref="T:Microsoft.AspNetCore.Mvc.OkObjectResult" /> response when the item is found
        /// otherwise an <see cref="T:Microsoft.AspNetCore.Mvc.NotFoundObjectResult" /> response.
        /// </returns>
        [HttpGet("{id:guid}")]
        public override ActionResult<TModel> GetById([FromRoute]Guid id)
        {
            return base.GetById(id);
        }

        /// <summary>
        /// Updates an item with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="model">The model to update.</param>
        /// <returns>
        /// An <see cref="T:Microsoft.AspNetCore.Mvc.NoContentResult" /> response.
        /// </returns>
        [HttpPut("{id:guid}")]
        public override IActionResult Update([FromRoute]Guid id, [FromBody, Required]TModel model)
        {
            return base.Update(id, model);
        }

        /// <summary>
        /// Deletes an item with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// An <see cref="T:Microsoft.AspNetCore.Mvc.NoContentResult" /> response.
        /// </returns>
        [HttpDelete("{id:guid}")]
        public override IActionResult Delete([FromRoute]Guid id)
        {
            return base.Delete(id);
        }
    }
}
