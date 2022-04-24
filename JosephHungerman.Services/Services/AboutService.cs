using JosephHungerman.Data.Models;
using JosephHungerman.Data.Repositories;
using JosephHungerman.Services.Models.Dtos;
using JosephHungerman.Services.Services.Interfaces;

namespace JosephHungerman.Services.Services
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
                var sections = await _unitOfWork.SectionRepository.GetAsync(includeProperties: $"{nameof(Section.Paragraphs)}", orderBy: s => s.OrderBy(o => o.OrderIndex));

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
                var currentSections = await _unitOfWork.SectionRepository.GetAsync(includeProperties: nameof(Section.Paragraphs));

                if (currentSections == null || !currentSections.Any())
                {
                    return new ServiceResponseDtos<List<Section>>.ServiceNotFoundExceptionResponse();
                }

                List<Section> results = new();
                foreach (var currentSection in currentSections)
                {
                    var matchSection = sections.FirstOrDefault(s => s.Id == currentSection.Id);
                    if (matchSection == null)
                    {
                        await _unitOfWork.SectionRepository.DeleteAsync(currentSection);
                    }
                    else
                    {
                        var paragraphToRemove = new Paragraph();
                        foreach (var currentSectionParagraph in currentSection.Paragraphs)
                        {
                            var matchParagraph =
                                matchSection.Paragraphs.FirstOrDefault(p => p.Id == currentSectionParagraph.Id);

                            if (matchParagraph == null)
                            {
                                paragraphToRemove = currentSectionParagraph;
                                await _unitOfWork.ParagraphRepository.DeleteAsync(currentSectionParagraph);
                            }
                        }

                        if (!string.IsNullOrEmpty(paragraphToRemove.Content)) currentSection.Paragraphs.Remove(paragraphToRemove);
                        var result = await _unitOfWork.SectionRepository.UpdateAsync(currentSections.FirstOrDefault(cs => cs.Id == matchSection.Id)!);
                        results.Add(result);
                    }

                }

                var saveSuccessful = await _unitOfWork.SaveChangesAsync();

                if (saveSuccessful)
                {
                    return new ServiceResponseDtos<List<Section>>.ServiceSuccessResponse(results.OrderBy(r => r.OrderIndex).ToList());
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
