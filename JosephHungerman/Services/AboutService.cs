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
    }
}
