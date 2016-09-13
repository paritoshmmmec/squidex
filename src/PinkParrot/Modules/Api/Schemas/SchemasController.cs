﻿// ==========================================================================
//  SchemasController.cs
//  PinkParrot Headless CMS
// ==========================================================================
//  Copyright (c) PinkParrot Group
//  All rights reserved.
// ==========================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PinkParrot.Core.Schema;
using PinkParrot.Infrastructure.CQRS.Commands;
using PinkParrot.Infrastructure.Reflection;
using PinkParrot.Read.Models;
using PinkParrot.Read.Repositories;
using PinkParrot.Write.Schema.Commands;

namespace PinkParrot.Modules.Api.Schemas
{
    public class SchemasController : ControllerBase
    {
        private readonly IModelSchemaRepository modelSchemaRepository;
        
        public SchemasController(ICommandBus commandBus, IModelSchemaRepository modelSchemaRepository)
            : base(commandBus)
        {
            this.modelSchemaRepository = modelSchemaRepository;
        }

        [HttpGet]
        [Route("api/schemas/")]
        public async Task<List<ListSchemaDto>> Query()
        {
            var schemas = await modelSchemaRepository.QueryAllAsync(TenantId);

            return schemas.Select(s => SimpleMapper.Map(s, new ListSchemaDto())).ToList();
        }

        [HttpGet]
        [Route("api/schemas/{name}/")]
        public async Task<ActionResult> Get(string name)
        {
            var entity = await modelSchemaRepository.FindSchemaAsync(TenantId, name);

            if (entity == null)
            {
                return NotFound();
            }

            return Ok(ModelSchemaDto.Create(entity.Schema));
        }

        [HttpPost]
        [Route("api/schemas/")]
        public async Task<ActionResult> Create([FromBody] CreateSchemaDto model)
        {
            var command = SimpleMapper.Map(model, new CreateModelSchema { AggregateId = Guid.NewGuid() });

            await CommandBus.PublishAsync(command);

            return CreatedAtAction("Query", new EntityCreatedDto { Id = command.AggregateId });
        }

        [HttpPut]
        [Route("api/schemas/{name}/")]
        public async Task<ActionResult> Update(string name, [FromBody] ModelSchemaProperties schema)
        {
            var command = new UpdateModelSchema { Properties = schema };

            await CommandBus.PublishAsync(command);

            return NoContent();
        }

        [HttpDelete]
        [Route("api/schemas/{name}/")]
        public async Task<ActionResult> Delete(string name)
        {
            await CommandBus.PublishAsync(new DeleteModelSchema());

            return NoContent();
        }
    }
}