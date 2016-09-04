using Multi_language.Common.Infrastructure.Mapping;
using Multi_language.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace Multi_Language.API.Models
{
    public class LanguagesApiModel : IMapFrom<Languages>
    {
        public int IdLanguage { get; set; }
        public string Name { get; set; }
        
        public string Initials { get; set; }
        
        public string Culture { get; set; }
        
        public byte[] Picture { get; set; }

        public bool IsActive { get; set; }
        
        public string UserName { get; set; }

        public DateTime? Datechanged { get; set; }

        public DateTime? DateCreated { get; set; }

        //public void CreateMappings(IConfiguration config)
        //{
        //    Mapper.Initialize(cfg => cfg.CreateMap<Languages, LanguagesApiModel>()
        //    .ForMember(m => m.Datechanged, opt => opt.MapFrom(c => c.Datechanged == DateTime.Now)));
        //}
    }
}