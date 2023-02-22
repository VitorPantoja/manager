﻿using Manager.Service.DTO;

namespace Manager.Service.Interfaces;

public interface IUserService
{
    Task<UserDTO> Create(UserDTO userDTO);
    Task<UserDTO> Update(UserDTO userDTO);
    Task Remove(long id);
    Task<UserDTO> Get(long id);
    Task<List<UserDTO>> Get();
    Task<List<UserDTO>> SearchByName(string name);
    Task<List<UserDTO>> SearchByEmail(string email);
    Task<UserDTO> GetByEmail(string email);
}