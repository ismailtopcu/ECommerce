using AutoMapper;
using ECommerce.DtoLayer.Dtos.Roles;
using ECommerce.EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<AppRole> _roleManager;
        public RoleController(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var values = await _roleManager.Roles.ToListAsync();
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(CreateRoleDto roleName)
        {
            
            await _roleManager.CreateAsync(new AppRole
            {
                Name = roleName.RoleName
            });
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var role = _roleManager.Roles.FirstOrDefault(y => y.Id == id);
            if (role == null) { return BadRequest(); }
            await _roleManager.DeleteAsync(role);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRole(UpdateRoleDto updateRoleDto)
        {
            var role = _roleManager.Roles.FirstOrDefault(x=>x.Id == updateRoleDto.Id);
            if (role == null) { return BadRequest(); }
            role.Name = updateRoleDto.Name;

            await _roleManager.UpdateAsync(role);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneRole(int id)
        {
            var role = await _roleManager.Roles.FirstOrDefaultAsync(y => y.Id == id);
            if (role == null) { return BadRequest(); }
            return Ok(role);
        }
    }
}
