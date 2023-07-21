using System;
using Business.Abstract;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]

    public class RoleCategory : ControllerBase
	{
		IRoleService _roleService;

		public RoleCategory(IRoleService roleService)
		{
			_roleService = roleService;
		}

        [Authorize]
        [HttpPost("AddRole")]
        public IActionResult AddRole(Role role)
        {
            var result = _roleService.Add(role);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [Authorize]
        [HttpPost("DeleteRole")]
        public IActionResult DeleteRole(int roleId)
        {
            var role = _roleService.GetByRoleId(roleId);
            var result = _roleService.Delete(role.Data);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("GetByRoleId")]
        public IActionResult GetByRoleId(int roleId)
        {
            var result = _roleService.GetByRoleId(roleId);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("GetRole")]
        public IActionResult GetRole()
        {
            var result = _roleService.GetAll();
            return result.Success ? Ok(result) : BadRequest(result);
        }

    }
}

