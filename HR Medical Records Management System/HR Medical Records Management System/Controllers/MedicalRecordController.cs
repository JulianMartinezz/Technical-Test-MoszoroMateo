using HR_Medical_Records_Management_System.Dtos.Request;
using HR_Medical_Records_Management_System.Models;
using HR_Medical_Records_Management_System.Responses;
using HR_Medical_Records_Management_System.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HR_Medical_Records_Management_System.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class MedicalRecordController : ControllerBase
    {
        private readonly IMedicalRecordService _medicalRecordService;

        public MedicalRecordController(IMedicalRecordService recordService)
        {
            _medicalRecordService = recordService;
        }


        [HttpGet("getRecord${id}")]
        public async Task<ActionResult<BaseResponse<TMedicalRecord>>> GetMedicalRecord([FromRoute]int id)
        {
            return Ok( await _medicalRecordService.GetByIdAsync(id) );
        }

        [HttpPost("addRecord")]
        public async Task<ActionResult<BaseResponse<TMedicalRecord>>> AddMedicalRecord([FromBody] PostMedicalRecordDto record)
        {
            return Ok(await _medicalRecordService.PostAsync(record));
        }

        [HttpPut("updateRecord")]
        public async Task<ActionResult<BaseResponse<TMedicalRecord>>> UpdateMedicalRecord([FromBody] UpdateMedicalRecordDto record)
        {
            return Ok(await _medicalRecordService.PutAsync(record));
        }

        [HttpDelete("deleteRecord")]
        public async Task<ActionResult<BaseResponse<TMedicalRecord>>> DeleteMedicalRecord([FromBody]DeleteMedicalRecordDto record)
        {
            return Ok(await _medicalRecordService.DeleteAsync(record));
        }

        [HttpGet("getRecords")]
        public async Task<ActionResult<BaseResponse<TMedicalRecord>>> GetMedicalRecords()
        {
            return Ok(await _medicalRecordService.GetListAsync());
        }

        [HttpPost("getFilteredRecords")]
        public async Task<ActionResult<BaseResponse<TMedicalRecord>>> GetFilteredMedicalRecords([FromBody] MedicalRecordsFiltersDto filter)
        {
            return Ok(await _medicalRecordService.GetMedicalRecordsFiltered(filter));
        }




    }
}
