using JosephHungerman.Data.Models;
using JosephHungerman.Data.Repositories;
using JosephHungerman.Services.Models.Dtos;
using JosephHungerman.Services.Services.Interfaces;

namespace JosephHungerman.Services.Services;

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
                $"{nameof(Resume.WorkExperiences)},{nameof(Resume.Educations)},{nameof(Resume.Skills)},{nameof(Resume.Certifications)},{nameof(Resume.WorkExperiences)}.{nameof(WorkExperience.WorkDetails)}");

            if (resumes == null || !resumes.Any())
            {
                return new ServiceResponseDtos<List<Resume>>.ServiceNotFoundExceptionResponse();
            }

            var resume = resumes.OrderByDescending(r => r.Id).FirstOrDefault();
            return new ServiceResponseDtos<Resume>.ServiceSuccessResponse(resume!);
        }
        catch (Exception e)
        {
            return new ServiceResponseDtos<Resume>.ServiceExceptionResponse(e);
        }
    }

    public async Task<ResponseDto> AddResumeAsync(Resume resume)
    {
        try
        {
            var response = await _unitOfWork.ResumeRepository.AddAsync(resume);

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