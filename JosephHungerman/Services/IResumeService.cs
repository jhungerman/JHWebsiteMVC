using JosephHungerman.Models.Dtos;

namespace JosephHungerman.Services;

public interface IResumeService
{
    Task<ResponseDto> GetResumeDetailsAsync();
}