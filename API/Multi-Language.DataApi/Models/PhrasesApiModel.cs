using Multi_language.Common.Infrastructure.Mapping;
using Multi_language.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace Multi_Language.DataApi.Models
{
    public class PhrasesApiModel: IMapFrom<Phrases>, ICustomMapping
    {
        public int IdProject { get; set; }

        public string LanguageInitials { get; set; }

        public int IdPhraseContext { get; set; }

        public string PhraseText { get; set; }

        public void CreateMappings(IMapperConfigurationExpression config)
        {
            config.CreateMap<Phrases, PhrasesApiModel>()
                .ForMember(p =>  p.IdProject , opt => opt.MapFrom(p => p.PhraseContext.IdProject));
        }
    }
}