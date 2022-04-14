using JosephHungerman.Models.Dtos;

namespace JosephHungerman.Services.Interfaces;

public interface IResumeService
{
    Task<ResponseDto> GetResumeDetailsAsync();
}