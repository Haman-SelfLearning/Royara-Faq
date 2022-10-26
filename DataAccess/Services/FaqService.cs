using DataAccess.Interfaces;
using Ganss.Xss;

using Infrastructure.Context;
using Infrastructure.Dtos;
using Model.Entities;

namespace DataAccess.Services
{
    public class FaqService : IFaqService
    {
        private DataContext _context;
        private HtmlSanitizer _sanitizer = new HtmlSanitizer();
        public FaqService(DataContext context)
        {
            _context = context;
        }
        public void Add(AddFaqDto addFaqDto)
        {
            
            Faq faq = new Faq()
            {
                Answer = _sanitizer.Sanitize(addFaqDto.Answer),
                Question = _sanitizer.Sanitize(addFaqDto.Question),
                CreateDate = DateTime.Now,
                Id = Guid.NewGuid(),
                IsRemoved = false,
                Updatetime = DateTime.Now,
            };
            _context.Faqs.Add(faq);
            _context.SaveChanges();
        }

        public FaqDto GetById(Guid id)
        {
            var data = _context.Faqs.FirstOrDefault(s => s.Id == id);
            if (data == null)
            {
                throw new Exception("متن یافت نشد");
            }

            FaqDto faqDto = new FaqDto()
            {
                Question = data.Question,
                Answer = data.Answer,
                CreateDate = data.CreateDate,
                Id = data.Id,
                UpdateTime = data.Updatetime,
            };
            return faqDto;
        }

        public IEnumerable<FaqDto> GetAll()
        {
            var data = _context.Faqs.Select(s => new FaqDto()
            {
                CreateDate = s.CreateDate,
                Answer = s.Answer,
                Id = s.Id,
                Question = s.Question,
                UpdateTime = s.Updatetime
            })
                .ToList();
            return data;
        }

        public void Delete(Guid id)
        {
            var data = _context.Faqs.SingleOrDefault(s => s.Id == id);
            if (data == null)
                throw new Exception("موردی یافت نشد");

            data.Updatetime=DateTime.Now;
            data.IsRemoved = true;
            _context.Faqs.Update(data);
            _context.SaveChanges();

        }

        public void Update(Guid id,UpdateFaqDto updateFaqDto)
        {
            var data = _context.Faqs.SingleOrDefault(s => s.Id == id);
            if (data == null)
                throw new Exception("موردی یافت نشد");
            
            data.Answer =_sanitizer.Sanitize(updateFaqDto.Answer);
            data.Question=_sanitizer.Sanitize(updateFaqDto.Question);
            data.Updatetime=DateTime.Now;
            _context.Faqs.Update(data);
            _context.SaveChanges();

        }
    }
}
