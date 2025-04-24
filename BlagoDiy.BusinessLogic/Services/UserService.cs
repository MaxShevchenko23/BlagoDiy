using AutoMapper;
using BlagoDiy.BusinessLogic.Models;
using BlagoDiy.DataAccessLayer.Entites;
using BlagoDiy.DataAccessLayer.Repositories;
using BlagoDiy.DataAccessLayer.UnitOfWork;

namespace BlagoDiy.BusinessLogic.Services;

public class UserService
{
    private readonly IMapper mapper;
    private readonly UserRepository userRepository;

    public UserService(IMapper _Mapper, IUnitOfWork unitOfWork)
    {
        userRepository = unitOfWork.UserRepository;
        mapper = _Mapper;
    }
    
    public async Task<User> GetUserByIdAsync(int id)
    {
        return await userRepository.GetByIdAsync(id);
    }
    
    public async Task<User?> CreateUserAsync(UserPost userDto)
    {
        var entity = mapper.Map<User>(userDto);
        await userRepository.AddAsync(entity);
        
        entity.CreatedAt = DateTime.Now;
        
        var createdUser = await userRepository.GetUserByEmailAndPasswordAsync(userDto.Email,userDto.Password);
        
        return createdUser;
    }
    
    public async Task<User?> LoginAsync(string email, string password)
    {
        var user = await userRepository.GetUserByEmailAndPasswordAsync(email, password);
        return user;
    }
    
    public async Task UpdateUserAsync(UserPost userDto, int userId)
    {
        var entity = mapper.Map<User>(userDto);
        entity.Id = userId;
         
        await userRepository.UpdateAsync(entity);
    }
    
    public async Task DeleteUserAsync(int id)
    {
        var user = await userRepository.GetByIdAsync(id);
        if (user != null)
        {
            await userRepository.DeleteAsync(id);
        }
    }
}