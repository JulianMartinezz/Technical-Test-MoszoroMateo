using HR_Medical_Records_Management_System.Dtos.Request;
using HR_Medical_Records_Management_System.Models;
using HR_Medical_Records_Management_System.Responses;
using HR_Medical_Records_Management_System.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HR_Medical_Records_Management_System.Controllers
{
    //This class is used to manage the medical records controller
    [Route("api/[controller]/")]
    [ApiController]
    public class MedicalRecordController : ControllerBase
    {
        //This property is used to manage the medical record service
        private readonly IMedicalRecordService _medicalRecordService;


        //This constructor is used to create a new instance of the medical record controller
        public MedicalRecordController(IMedicalRecordService recordService)
        {
            _medicalRecordService = recordService;
        }

        //this endpoint is used to get a medical record by id
        [HttpGet("getRecord${id}")]
        public async Task<ActionResult<BaseResponse<TMedicalRecord>>> GetMedicalRecord([FromRoute]int id)
        {
            return Ok( await _medicalRecordService.GetByIdAsync(id) );
        }

        //this endpoint is used to add a medical record
        [HttpPost("addRecord")]
        public async Task<ActionResult<BaseResponse<TMedicalRecord>>> AddMedicalRecord([FromBody] PostMedicalRecordDto record)
        {
            return Ok(await _medicalRecordService.PostAsync(record));
        }

        //this endpoint is used to update a medical record
        [HttpPut("updateRecord")]
        public async Task<ActionResult<BaseResponse<TMedicalRecord>>> UpdateMedicalRecord([FromBody] UpdateMedicalRecordDto record)
        {
            return Ok(await _medicalRecordService.PutAsync(record));
        }

        //this endpoint is used to logical delete a medical record
        [HttpDelete("deleteRecord")]
        public async Task<ActionResult<BaseResponse<TMedicalRecord>>> DeleteMedicalRecord([FromBody]DeleteMedicalRecordDto record)
        {
            return Ok(await _medicalRecordService.DeleteAsync(record));
        }

        //this endpoint is used to get all the medical records
        [HttpGet("getRecords")]
        public async Task<ActionResult<BaseResponse<TMedicalRecord>>> GetMedicalRecords()
        {
            return Ok(await _medicalRecordService.GetListAsync());
        }

        //this endpoint is used to get all the medical records filtered
        [HttpPost("getFilteredRecords")]
        public async Task<ActionResult<BaseResponse<TMedicalRecord>>> GetFilteredMedicalRecords([FromBody] MedicalRecordsFiltersDto filter)
        {
            return Ok(await _medicalRecordService.GetMedicalRecordsFiltered(filter));
        }




    }
}
