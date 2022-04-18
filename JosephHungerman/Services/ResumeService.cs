using JosephHungerman.Data.Repositories;
using JosephHungerman.Models.Dtos;
using JosephHungerman.Models.Work;
using JosephHungerman.Services.Interfaces;

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
            var resume = await _unitOfWork.ResumeRepository.GetFirstAsync(
                includeProperties:
                $"{nameof(Resume.WorkExperiences)},{nameof(Resume.Educations)},{nameof(Resume.Skills)},{nameof(Resume.Certifications)},{nameof(Resume.WorkExperiences)}.{nameof(WorkExperience.WorkDetails)}");

            if (resume == null)
            {
                return new ServiceResponseDtos<List<Resume>>.ServiceNotFoundExceptionResponse();
            }

            return new ServiceResponseDtos<Resume>.ServiceSuccessResponse(resume!);
        }
        catch (Exception e)
        {
            return new ServiceResponseDtos<Resume>.ServiceExceptionResponse(e);
        }
    }

    public async Task<ResponseDto> UpdateResumeAsync(Resume resume)
    {
        try
        {
            var response = await _unitOfWork.ResumeRepository.UpdateAsync(resume);

            var saveSuccessful = await _unitOfWork.SaveChangesAsync();

            if (saveSuccessful)
            {
                return new ServiceResponseDtos<Resume>.ServiceSuccessResponse(response);
            }

            return new ServiceResponseDtos<Resume>.ServiceDbExceptionResponse();
        }
        catch (Exception e)
        {
            return new ServiceResponseDtos<Resume>.ServiceExceptionResponse(e);
        }
    }
}