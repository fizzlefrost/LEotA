using System;
using System.IO;
using System.Security.Principal;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Calabonga.Microservices.Core.Validators;
using Calabonga.UnitOfWork;
using Calabonga.UnitOfWork.Controllers.Factories;
using Calabonga.UnitOfWork.Controllers.Managers;
using LEotA.Engine.Entities;
using LEotA.Engine.Web.ViewModels.NewsViewModels;
using Microsoft.AspNetCore.Http;

namespace LEotA.Engine.Web.Infrastructure.Engine.EntityManagers
{
    public class NewsManager: EntityManager<NewsViewModel, News, NewsCreateViewModel, NewsUpdateViewModel>
    {
        private static IUnitOfWork _unitOfWork;

        public NewsManager(IMapper mapper, 
            IViewModelFactory<NewsCreateViewModel, 
                NewsUpdateViewModel> viewModelFactory, 
            IUnitOfWork unitOfWork,
            IEntityValidator<News> validator) : base(mapper, viewModelFactory, validator)
        {
            _unitOfWork = unitOfWork;
        }

        protected override IIdentity? GetIdentity() => null;
        
        public static async Task<News> LargePost(IFormFile textFile, string description, string name, string author, string time)
        {
            var repository = _unitOfWork.GetRepository<News>();
            var news = new News();
            using (var ms = new MemoryStream())
            {
                textFile.CopyTo(ms);
                var fileBytes = ms.ToArray();
                var text = Encoding.UTF8.GetString(fileBytes);
                news = new News()
                {
                    Id = new Guid(),
                    Name = name,
                    Description = description,
                    Author = author,
                    Text = text,
                    Time = DateTime.Now
                };
                await repository.InsertAsync(news);
                _unitOfWork.SaveChanges();
            }

            return news;
        }

        // public static Dictionary<String, Object> parse(byte[] json){
        //     string jsonStr = Encoding.UTF8.GetString(json);
        //     return JsonConvert.DeserializeObject<Dictionary<String, Object>>(jsonStr);
        // }
    }
}