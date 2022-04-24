using JosephHungerman.Data.Models;
using JosephHungerman.Models.Dtos;

namespace JosephHungerman.Services.Interfaces;

public interface IResumeService
{
    Task<ResponseDto> GetResumeDetailsAsync();
    Task<ResponseDto> AddResumeAsync(Resume resume);
}