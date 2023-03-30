using Microsoft.AspNetCore.Mvc;
using Prepa.Models;
using Prepa.Services;

namespace Prepa.Controllers
{
    public class PupilController : ControllerBase
    {
        private IPupilService _IPupilService;

        public PupilController(IPupilService PupilService)
        {
            _IPupilService = PupilService;

        }
        [HttpPost("/group/{groupId}/participant")]
        public async Task<ActionResult<int>> AddNewPupil([FromBody] AddPupilDto group,int groupId)
        {
            if (!ModelState.IsValid)
            {

                return UnprocessableEntity(ModelState);
            }
            try
            {
                var res = await _IPupilService.CreatePupil(group,groupId);
                return res;
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpDelete("/group/{groupId}/participant/{participantId}")]
        public async Task<ActionResult> DeletePupil(int participantId, int groupId)
        {
            try
            {
                await _IPupilService.DeletePupil(participantId, groupId);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

    }
}
