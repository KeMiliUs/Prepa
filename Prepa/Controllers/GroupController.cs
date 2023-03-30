using Microsoft.AspNetCore.Mvc;
using Prepa.Models.Group;
using Prepa.Services;

namespace Prepa.Controllers
{
    [ApiController]
    public class GroupController : ControllerBase
    {
        private IGroupControlService _IGroupControlService;

        public GroupController(IGroupControlService GroupControlService)
        {
            _IGroupControlService = GroupControlService;

        }
        [HttpPost("/group")]
        public async Task<ActionResult<int>> AddNewGroup([FromBody] CreateGroupDto group)
        {
            if (!ModelState.IsValid)
            {

                return UnprocessableEntity(ModelState);
            }
            try
            {
                var res = await _IGroupControlService.CreateGroup(group);
                return res;
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpGet("/groups")]
        public SendAllGroupDto[] GetAllGroups()
        {
            var res = _IGroupControlService.GetAllGroups();
            return res;
        }
        [HttpPut("/group/{id}")] 
        public async Task<ActionResult> ChangeGroup([FromBody] ChangeGroupDto model,int id)
        {
            if (!ModelState.IsValid)
            {

                return UnprocessableEntity(ModelState);
            }
            try
            {
                await _IGroupControlService.ChangeGroup(model,id);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpDelete("/group/{id}")]
        public async Task<ActionResult> DeleteGroup(int id)
        {
            
            try
            {
                await _IGroupControlService.DeleteGroup(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

    }

}
