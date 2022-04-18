using JosephHungerman.Models.Dtos;
using JosephHungerman.Models.Work;

namespace JosephHungerman.Services.Interfaces;

public interface IResumeService
{
    Task<ResponseDto> GetResumeDetailsAsync();
    Task<ResponseDto> UpdateResumeAsync(Resume resume);
}