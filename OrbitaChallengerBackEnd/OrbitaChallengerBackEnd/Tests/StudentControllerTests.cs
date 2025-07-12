using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using OrbitaChallengerBackEnd.Controllers;
using OrbitaChallengerBackEnd.Interfaces;
using OrbitaChallengerBackEnd.Models;
using OrbitaChallengerBackEnd.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

public class StudentControllerTests
{
    private readonly Mock<IStudentRepository> _repoMock;
    private readonly StudentController _controller;

    public StudentControllerTests()
    {
        _repoMock = new Mock<IStudentRepository>();
        _controller = new StudentController(_repoMock.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOk_WhenSuccessAndDataNotNull()
    {
        var resultVm = new ResultViewModel<IEnumerable<Student>>(true, "ok", new List<Student> { new Student() });
        _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(resultVm);

        var result = await _controller.GetAll();

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(resultVm, okResult.Value);
    }

    [Fact]
    public async Task GetAll_ReturnsNoContent_WhenDataNullOrNotSuccess()
    {
        var resultVm = new ResultViewModel<IEnumerable<Student>>(true, "ok", null);
        _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(resultVm);

        var result = await _controller.GetAll();

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task GetAll_ReturnsBadRequest_OnException()
    {
        _repoMock.Setup(r => r.GetAllAsync()).ThrowsAsync(new System.Exception("fail"));

        var result = await _controller.GetAll();

        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Contains("An error occurred", badRequest.Value.ToString());
    }

    [Fact]
    public async Task GetByName_ReturnsBadRequest_WhenNameIsEmpty()
    {
        var result = await _controller.GetByName("");

        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Name cannot be empty.", badRequest.Value);
    }

    [Fact]
    public async Task GetByName_ReturnsNotFound_WhenNotSuccessOrDataNull()
    {
        var resultVm = new ResultViewModel<List<Student>>(false, "not found", null);
        _repoMock.Setup(r => r.GetByNameAsync("John")).ReturnsAsync(resultVm);

        var result = await _controller.GetByName("John");

        var notFound = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Contains("Student not found", notFound.Value.ToString());
    }

    [Fact]
    public async Task GetByName_ReturnsOk_WhenSuccessAndDataNotNull()
    {
        var resultVm = new ResultViewModel<List<Student>>(true, "ok", new List<Student> { new Student() });
        _repoMock.Setup(r => r.GetByNameAsync("John")).ReturnsAsync(resultVm);

        var result = await _controller.GetByName("John");

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(resultVm, okResult.Value);
    }

    [Fact]
    public async Task Add_ReturnsBadRequest_WhenModelStateInvalid()
    {
        _controller.ModelState.AddModelError("Name", "Required");
        var result = await _controller.Add(new Student());

        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Contains("Invalid data provided", badRequest.Value.ToString());
    }

    [Fact]
    public async Task Add_ReturnsOk_WhenSuccess()
    {
        var student = new Student();
        var resultVm = new ResultViewModel<bool>(true, "added", true);
        _repoMock.Setup(r => r.AddAsync(student)).ReturnsAsync(resultVm);

        var result = await _controller.Add(student);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(resultVm, okResult.Value);
    }

    [Fact]
    public async Task Add_ReturnsOkWithMessage_WhenNotSuccess()
    {
        var student = new Student();
        var resultVm = new ResultViewModel<bool>(false, "fail", false);
        _repoMock.Setup(r => r.AddAsync(student)).ReturnsAsync(resultVm);

        var result = await _controller.Add(student);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Contains("fail", okResult.Value.ToString());
    }

    [Fact]
    public async Task Add_ReturnsBadRequest_OnException()
    {
        var student = new Student();
        _repoMock.Setup(r => r.AddAsync(student)).ThrowsAsync(new System.Exception("fail"));

        var result = await _controller.Add(student);

        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Contains("Error adding student", badRequest.Value.ToString());
    }

    [Fact]
    public async Task Edit_ReturnsBadRequest_WhenModelStateInvalid()
    {
        _controller.ModelState.AddModelError("Name", "Required");
        var result = await _controller.Edit(new Student());

        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Contains("Invalid data provided", badRequest.Value.ToString());
    }

    [Fact]
    public async Task Edit_ReturnsOk_WhenSuccess()
    {
        var student = new Student();
        var resultVm = new ResultViewModel<bool>(true, "edited", true);
        _repoMock.Setup(r => r.EditAsync(student)).ReturnsAsync(resultVm);

        var result = await _controller.Edit(student);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(resultVm, okResult.Value);
    }

    [Fact]
    public async Task Edit_ReturnsOk_WhenNotSuccess()
    {
        var student = new Student();
        var resultVm = new ResultViewModel<bool>(false, "fail", false);
        _repoMock.Setup(r => r.EditAsync(student)).ReturnsAsync(resultVm);

        var result = await _controller.Edit(student);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(resultVm, okResult.Value);
    }

    [Fact]
    public async Task Edit_ReturnsBadRequest_OnException()
    {
        var student = new Student();
        _repoMock.Setup(r => r.EditAsync(student)).ThrowsAsync(new System.Exception("fail"));

        var result = await _controller.Edit(student);

        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Contains("An error occurred", badRequest.Value.ToString());
    }

    [Fact]
    public async Task GetBy_ReturnsBadRequest_WhenSearchTermEmpty()
    {
        var result = await _controller.GetBy("");

        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Search term cannot be empty.", badRequest.Value);
    }

    [Fact]
    public async Task GetBy_ReturnsNotFound_WhenNotSuccess()
    {
        var resultVm = new ResultViewModel<IEnumerable<Student>>(false, "fail", null);
        _repoMock.Setup(r => r.GetByAsync("term")).ReturnsAsync(resultVm);

        var result = await _controller.GetBy("term");

        var notFound = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal(resultVm, notFound.Value);
    }

    [Fact]
    public async Task GetBy_ReturnsOk_WhenSuccess()
    {
        var resultVm = new ResultViewModel<IEnumerable<Student>>(true, "ok", new List<Student> { new Student() });
        _repoMock.Setup(r => r.GetByAsync("term")).ReturnsAsync(resultVm);

        var result = await _controller.GetBy("term");

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(resultVm, okResult.Value);
    }

    [Fact]
    public async Task GetBy_ReturnsBadRequest_OnException()
    {
        _repoMock.Setup(r => r.GetByAsync("term")).ThrowsAsync(new System.Exception("fail"));

        var result = await _controller.GetBy("term");

        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Contains("An error occurred", badRequest.Value.ToString());
    }

    [Fact]
    public async Task Delete_ReturnsBadRequest_WhenNotSuccess()
    {
        var resultVm = new ResultViewModel<bool>(false, "fail", false);
        _repoMock.Setup(r => r.DeleteAsync("123")).ReturnsAsync(resultVm);

        var result = await _controller.Delete("123");

        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(resultVm, badRequest.Value);
    }

    [Fact]
    public async Task Delete_ReturnsOk_WhenSuccess()
    {
        var resultVm = new ResultViewModel<bool>(true, "deleted", true);
        _repoMock.Setup(r => r.DeleteAsync("123")).ReturnsAsync(resultVm);

        var result = await _controller.Delete("123");

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(resultVm, okResult.Value);
    }

    [Fact]
    public async Task Delete_ReturnsBadRequest_OnException()
    {
        _repoMock.Setup(r => r.DeleteAsync("123")).ThrowsAsync(new System.Exception("fail"));

        var result = await _controller.Delete("123");

        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Contains("An error occurred", badRequest.Value.ToString());
    }

    [Fact]
    public async Task GetByRA_ReturnsBadRequest_WhenRAEmpty()
    {
        var result = await _controller.GetByRA("");

        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("RA cannot be empty.", badRequest.Value);
    }

    [Fact]
    public async Task GetByRA_ReturnsNotFound_WhenNotSuccess()
    {
        var resultVm = new ResultViewModel<Student>(false, "fail", null);
        _repoMock.Setup(r => r.GetByRAAsync("123")).ReturnsAsync(resultVm);

        var result = await _controller.GetByRA("123");

        var notFound = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal(resultVm, notFound.Value);
    }

    [Fact]
    public async Task GetByRA_ReturnsOk_WhenSuccess()
    {
        var resultVm = new ResultViewModel<Student>(true, "ok", new Student());
        _repoMock.Setup(r => r.GetByRAAsync("123")).ReturnsAsync(resultVm);

        var result = await _controller.GetByRA("123");

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(resultVm, okResult.Value);
    }

    [Fact]
    public async Task GetByRA_ReturnsBadRequest_OnException()
    {
        _repoMock.Setup(r => r.GetByRAAsync("123")).ThrowsAsync(new System.Exception("fail"));

        var result = await _controller.GetByRA("123");

        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Contains("An error occurred", badRequest.Value.ToString());
    }
}
