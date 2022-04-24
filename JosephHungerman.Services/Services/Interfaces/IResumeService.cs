using JosephHungerman.Data.Models;
using JosephHungerman.Services.Models.Dtos;

namespace JosephHungerman.Services.Services.Interfaces;

public interface IResumeService
{
    Task<ResponseDto> GetResumeDetailsAsync();
    Task<ResponseDto> AddResumeAsync(Resume resume);
}