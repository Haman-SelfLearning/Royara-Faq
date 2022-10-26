using Infrastructure.Dtos;

namespace DataAccess.Interfaces
{
    public interface IFaqService
    {
        void Add(AddFaqDto addFaqDto);
        FaqDto GetById(Guid id);
        IEnumerable<FaqDto> GetAll();
        void Delete(Guid id);
        void Update(Guid id, UpdateFaqDto updateFaqDto);
    }
}
