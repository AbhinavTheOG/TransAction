﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using TransAction.API.Authorization;
using TransAction.Data.Models;

namespace TransAction.API.Controllers
{
    [Route("api/roles")]
    public class RoleController : BaseController
    {

        public RoleController(IHttpContextAccessor httpContextAccessor, ILogger<RoleController> logger) :
            base(httpContextAccessor, logger)
        { }

        [HttpGet()]
        public IActionResult GetRoles()
        {
            var roles = _transActionRepo.GetRoles();
            var getRoles = _mapper.Map<IEnumerable<RoleDto>>(roles);
            return Ok(getRoles);

        }

        [HttpGet("{id}", Name = "GetThatRole")]
        public IActionResult GetRoleById(int id)
        {
            try
            {
                var getRoles = _transActionRepo.GetRoles().FirstOrDefault(c => c.RoleId == id);

                if (getRoles == null)
                {
                    return NotFound();
                }
                var getRole = _transActionRepo.GetRole(id);
                var getRoleResult = _mapper.Map<RoleDto>(getRole);
                return Ok(getRoleResult);

            }

            catch (Exception)
            {
                return StatusCode(500, "A problem happened while handeling your request");
            }

        }

        [ClaimRequirement(AuthorizationTypes.ADMIN_CLAIM)]
        [HttpPost()]
        public IActionResult CreateRole([FromBody] RoleCreateDto createRole)
        {
            if (createRole == null)
            {
                return BadRequest();
            }
            if (createRole.Description == null || createRole.Name == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_transActionRepo.RoleExists(createRole.Name))
            {
                return BadRequest();
            }

            var newRole = _mapper.Map<TraRole>(createRole);


            _transActionRepo.CreateRole(newRole);

            if (!_transActionRepo.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            var createdPointOfInterestToReturn = _mapper.Map<RoleDto>(newRole);
            return CreatedAtRoute("GetThatRole", new { id = createdPointOfInterestToReturn.RoleId }, createdPointOfInterestToReturn);


        }

        [ClaimRequirement(AuthorizationTypes.ADMIN_CLAIM)]
        [HttpPut("{id}")]
        public IActionResult UpdateRole(int id, [FromBody] RoleUpdateDto updateRole)
        {
            var roleEntity = _transActionRepo.GetRole(id);
            if (roleEntity == null) return NotFound();
            if (updateRole == null) return NotFound();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _mapper.Map(updateRole, roleEntity);


            if (!_transActionRepo.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return NoContent();
        }
    }
}