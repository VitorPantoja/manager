using AutoMapper;
namespace Manager.API.Controllers;
using Utilities;
using ViewModels;
using Core.Exceptions;
using Service.DTO;
using Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
public class UserController : ControllerBase
{
    private readonly IMapper _mapper;
    
    private readonly IUserService _userService;
    
    public UserController(IMapper mapper, IUserService userService)
    {
        _mapper = mapper;
        _userService = userService;
    }

    [HttpPost]
    [Route("/api/v1/users/create")]
    public async Task<IActionResult> Create([FromBody] CreateUserViewModel createUserViewModel)
    {
        try
        {
            var userDTO = _mapper.Map<UserDTO>(createUserViewModel);
            var userCreated = await _userService.Create(userDTO);
            return Ok(new ResultViewModel
            {
                Message = "Usuário criado com sucesso",
                Success = true,
                Data = userCreated
            });
        }
        catch (DomainException e)
        {
            return BadRequest(Responses.DomainErrorMessage(e.Message, e.Errors));
        }
        catch (Exception e)
        {
            return StatusCode(500, Responses.ApplicationErrorMessage());
        }
    }
}