﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Vanguard.Framework.Core;
using Vanguard.Framework.Core.Cqrs;
using Vanguard.Framework.Core.Repositories;

namespace Vanguard.Framework.Http
{
    /// <summary>
    /// The Create, Read, Update and Delete (CRUD) controller class.
    /// </summary>
    /// <typeparam name="TIdentifier">The type of the identifier.</typeparam>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class CrudController<TIdentifier, TModel> : ControllerBase
        where TModel : IUniqueEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CrudController{TIdentifier, TModel}"/> class.
        /// </summary>
        /// <param name="commandDispatcher">The command dispatcher.</param>
        /// <param name="queryDispatcher">The query dispatcher.</param>
        public CrudController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            Guard.ArgumentNotNull(commandDispatcher, nameof(commandDispatcher));
            Guard.ArgumentNotNull(queryDispatcher, nameof(queryDispatcher));

            CommandDispatcher = commandDispatcher;
            QueryDispatcher = queryDispatcher;
        }

        /// <summary>
        /// Gets the command dispatcher.
        /// </summary>
        /// <value>
        /// The command dispatcher.
        /// </value>
        protected ICommandDispatcher CommandDispatcher { get; }

        /// <summary>
        /// Gets the query dispatcher.
        /// </summary>
        /// <value>
        /// The query dispatcher.
        /// </value>
        protected IQueryDispatcher QueryDispatcher { get; }

        /// <summary>
        /// Gets the number of items that conform with the specified find criteria.
        /// </summary>
        /// <param name="findCriteria">The find criteria.</param>
        /// <returns>The number of items that conform with the specified find criteria.</returns>
        [HttpGet("count")]
        public IActionResult Count(FindCriteria findCriteria)
        {
            var query = new CountQuery<TModel>(findCriteria);
            var result = QueryDispatcher.Dispatch(query);
            return Ok(result);
        }

        /// <summary>
        /// Gets the items that conform with the specified find criteria.
        /// </summary>
        /// <param name="findCriteria">The find criteria.</param>
        /// <returns>A collection of items that conform with the specified find criteria.</returns>
        [HttpGet]
        public IActionResult Find(FindCriteria findCriteria)
        {
            var query = new FindQuery<TModel>(findCriteria);
            var result = QueryDispatcher.Dispatch(query);
            return Ok(result);
        }

        /// <summary>
        /// Gets an item by its identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// An <see cref="OkObjectResult"/> response when the item is found
        /// otherwise an <see cref="NotFoundObjectResult"/> response.
        /// </returns>
        [HttpGet("{id}")]
        public IActionResult GetById(TIdentifier id)
        {
            var query = new GetQuery<TModel>(id);
            var result = QueryDispatcher.Dispatch(query);
            return OkOrNotFound(result);
        }

        /// <summary>
        /// Creates an item.
        /// </summary>
        /// <param name="model">The model to add.</param>
        /// <returns>An <see cref="CreatedAtRouteResult"/> response.</returns>
        [HttpPost]
        public IActionResult Create([FromBody, Required]TModel model)
        {
            var command = new AddCommand<TModel>(model);
            CommandDispatcher.Dispatch(command);
            return CreatedAtRoute(nameof(GetById), model.Id, model);
        }

        /// <summary>
        /// Updates an item with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="model">The model to update.</param>
        /// <returns>An <see cref="NoContentResult"/> response.</returns>
        [HttpPut("{id}")]
        public IActionResult Update(TIdentifier id, [FromBody, Required]TModel model)
        {
            var command = new UpdateCommand<TModel>(id, model);
            CommandDispatcher.Dispatch(command);
            return new NoContentResult();
        }

        /// <summary>
        /// Deletes an item with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>An <see cref="NoContentResult"/> response.</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(TIdentifier id)
        {
            var command = new DeleteCommand<TModel>(id);
            CommandDispatcher.Dispatch(command);
            return new NoContentResult();
        }
    }
}
