using HR_Medical_Records_Management_System.Dtos.Request;
using HR_Medical_Records_Management_System.Models;
using HR_Medical_Records_Management_System.Responses;
using HR_Medical_Records_Management_System.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HR_Medical_Records_Management_System.Controllers
{
    /// <summary>
    /// Controller responsible for managing medical records. It provides endpoints for
    /// retrieving, adding, updating, deleting, and filtering medical records.
    /// </summary>
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

        /// <summary>
        /// Endpoint used to get a medical record by its ID.
        /// </summary>
        /// <param name="id">The ID of the medical record to retrieve.</param>
        /// <returns>A BaseResponse containing the medical record or a NotFound response if the record does not exist.</returns>
        [HttpGet("getRecord${id}")]
        public async Task<ActionResult<BaseResponse<TMedicalRecord>>> GetMedicalRecord([FromRoute]int id)
        {
            return Ok( await _medicalRecordService.GetByIdAsync(id) );
        }

        /// <summary>
        /// Endpoint used to add a new medical record.
        /// </summary>
        /// <param name="record">The medical record data to be added.</param>
        /// <returns>A BaseResponse containing the added medical record or an error message.</returns>
        [HttpPost("addRecord")]
        public async Task<ActionResult<BaseResponse<TMedicalRecord>>> AddMedicalRecord([FromBody] PostMedicalRecordDto record)
        {
            return Ok(await _medicalRecordService.PostAsync(record));
        }

        /// <summary>
        /// Endpoint used to update an existing medical record.
        /// </summary>
        /// <param name="record">The updated medical record data.</param>
        /// <returns>A BaseResponse containing the updated medical record or an error message.</returns>
        [HttpPut("updateRecord")]
        public async Task<ActionResult<BaseResponse<TMedicalRecord>>> UpdateMedicalRecord([FromBody] UpdateMedicalRecordDto record)
        {
            return Ok(await _medicalRecordService.PutAsync(record));
        }

        /// <summary>
        /// Endpoint used to logically delete a medical record by updating its status.
        /// </summary>
        /// <param name="record">The medical record data with deletion details.</param>
        /// <returns>A BaseResponse indicating success or failure of the delete operation.</returns>
        [HttpDelete("deleteRecord")]
        public async Task<ActionResult<BaseResponse<TMedicalRecord>>> DeleteMedicalRecord([FromBody]DeleteMedicalRecordDto record)
        {
            return Ok(await _medicalRecordService.DeleteAsync(record));
        }

        // <summary>
        /// Endpoint used to get medical records filtered based on provided criteria.
        /// </summary>
        /// <param name="filter">The filter criteria to search for medical records.</param>
        /// <returns>A BaseResponse containing a list of medical records that match the filter.</returns>
        [HttpGet("getFilteredRecords")]
        public async Task<ActionResult<BaseResponse<List<TMedicalRecord>>>> GetFilteredMedicalRecords([FromQuery] MedicalRecordsFiltersDto filter)
        {
            return Ok(await _medicalRecordService.GetFilteredListAsync(filter));
        }




    }
}
