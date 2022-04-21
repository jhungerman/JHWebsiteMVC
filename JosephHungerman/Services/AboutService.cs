using JosephHungerman.Data.Repositories;
using JosephHungerman.Models.About;
using JosephHungerman.Models.Dtos;
using JosephHungerman.Services.Interfaces;

namespace JosephHungerman.Services
{
    public class AboutService : IAboutService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AboutService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDto> GetSectionsAsync()
        {
            try
            {
                var sections = await _unitOfWork.SectionRepository.GetAsync(includeProperties: $"{nameof(Section.Paragraphs)}");

                if (sections == null || !sections.Any())
                {
                    return new ServiceResponseDtos<List<Section>>.ServiceNotFoundExceptionResponse();
                }

                return new ServiceResponseDtos<List<Section>>.ServiceSuccessResponse(sections.ToList());
            }
            catch (Exception e)
            {
                return new ServiceResponseDtos<List<Section>>.ServiceExceptionResponse(e);
            }
        }

        public async Task<ResponseDto> UpdateSectionsAsync(IList<Section> sections)
        {
            try
            {
                var currentSections = await _unitOfWork.SectionRepository.GetAsync();

                if (currentSections == null || !currentSections.Any())
                {
                    return new ServiceResponseDtos<List<Section>>.ServiceNotFoundExceptionResponse();
                }

                IList<Section> itemsToDelete = currentSections.Except(sections).ToList();
                if (itemsToDelete.Any())
                {
                    await _unitOfWork.SectionRepository.DeleteAllAsync(itemsToDelete);
                }

                var result = await _unitOfWork.SectionRepository.UpdateAllAsync(sections);

                var saveSuccessful = await _unitOfWork.SaveChangesAsync();

                if (saveSuccessful)
                {
                    return new ServiceResponseDtos<List<Section>>.ServiceSuccessResponse(result.ToList());
                }

                return new ServiceResponseDtos<List<Section>>.ServiceDbExceptionResponse();
            }
            catch (Exception e)
            {
                return new ServiceResponseDtos<List<Section>>.ServiceExceptionResponse(e);
            }
        }
    }
}
