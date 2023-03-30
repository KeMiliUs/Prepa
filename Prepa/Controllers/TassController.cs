using Microsoft.AspNetCore.Mvc;
using Prepa.Services;

namespace Prepa.Controllers
{
    [ApiController]
    public class TassController : ControllerBase
    {
        private ITossService _ITossService;

        public TassController(ITossService TossService)
        {
            _ITossService = TossService;
        }


        [HttpPost("/group/{id}/toss")]
        public async Task<ActionResult> MadeTass(int id)
        {
            try
            {
                var res = await _ITossService.TassGroup(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpGet("/group/{groupId}/participant/{participantId}/recipient")]
        public async Task<ActionResult> GetRec(int groupId,int participantId)
        {
            try
            {
                var res = await _ITossService.GetReceptionist(groupId,participantId);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

    }
}
