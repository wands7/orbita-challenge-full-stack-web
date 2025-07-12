using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrbitaChallengerBackEnd.Interfaces;
using OrbitaChallengerBackEnd.Models;
using OrbitaChallengerBackEnd.ViewModels;

namespace OrbitaChallengerBackEnd.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepository;

        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        //[Authorize]
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _studentRepository.GetAllAsync();

                if (result.Success && result.Data != null)
                    return Ok(result);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "An error occurred while retrieving students.", Details = ex.Message });
            }
        }

        //[Authorize]
        [HttpGet("getByName")]
        public async Task<IActionResult> GetByName([FromQuery] string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                    return BadRequest("Name cannot be empty.");

                var result = await _studentRepository.GetByNameAsync(name);

                if (!result.Success || result.Data == null)
                    return NotFound(new { Message = "Student not found." });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "An error occurred while retrieving student by name.", Details = ex.Message });
            }
        }
        //[Authorize]
        [HttpPost("add")]
        public async Task<ActionResult> Add([FromBody] Student student)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new { Message = "Invalid data provided." });


                var result = await _studentRepository.AddAsync(student);

                if (result.Success)
                    return Ok(result);

                return Ok(new { Message = result.Message, Details = result.Data });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Error adding student.", Details = ex.Message });
            }

        }
        //[Authorize]
        [HttpPost("edit")]
        public async Task<ActionResult> Edit([FromBody] Student student)
        {

            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ResultViewModel<bool>(false, "Invalid data provided."));

                var result = await _studentRepository.EditAsync(student);

                if (result.Success)
                    return Ok(result);

                return Ok(result);

            }
            catch (Exception ex)
            {

                return BadRequest(new { Message = "An error occurred.", Details = ex.Message });

            }
        }
        //[Authorize]
        [HttpGet("search")]
        public async Task<ActionResult<ResultViewModel<Student>>> GetBy([FromQuery] string searchTerm)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchTerm))
                    return BadRequest("Search term cannot be empty.");


                var result = await _studentRepository.GetByAsync(searchTerm);

                if (!result.Success)
                    return NotFound(result);


                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "An error occurred.", Details = ex.Message });
            }
        }
        //[Authorize]
        [HttpPost("delete/{ra}")]
        public async Task<IActionResult> Delete(string ra)
        {
            try
            {
                var result = await _studentRepository.DeleteAsync(ra);

                if (!result.Success)
                    return BadRequest(result);

                return Ok(result);


            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "An error occurred.", Details = ex.Message });

            }
        }
        //[Authorize]
        [HttpGet("getByRA/{ra}")]
        public async Task<ActionResult> GetByRA(string RA)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(RA))
                    return BadRequest("RA cannot be empty.");


                var result = await _studentRepository.GetByRAAsync(RA);

                if (!result.Success)
                    return NotFound(result);


                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "An error occurred.", Details = ex.Message });
            }
        }
    }
}
