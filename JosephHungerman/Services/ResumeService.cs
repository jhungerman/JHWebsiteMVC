using JosephHungerman.Data.Repositories;
using JosephHungerman.Models.Dtos;
using JosephHungerman.Models.Work;

namespace JosephHungerman.Services;

public class ResumeService : IResumeService
{
    private readonly IUnitOfWork _unitOfWork;

    public ResumeService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseDto> GetResumeDetailsAsync()
    {
        try
        {
            var resumes = await _unitOfWork.ResumeRepository.GetAsync(
                includeProperties:
                $"{nameof(Resume.WorkExperiences)},{nameof(Resume.Educations)},{nameof(Resume.Skills)},{nameof(Resume.Certifications)}");

            if (resumes == null || !resumes.Any())
            {
                return new ServiceResponseDtos<List<Resume>>.ServiceNotFoundExceptionResponse();
            }

            var resume = resumes.FirstOrDefault();

            return new ServiceResponseDtos<Resume>.ServiceSuccessResponse(resume!);
        }
        catch (Exception e)
        {
            return new ServiceResponseDtos<Resume>.ServiceExceptionResponse(e);
        }
    }
}