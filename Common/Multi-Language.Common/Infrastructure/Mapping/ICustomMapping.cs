using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multi_language.Common.Infrastructure.Mapping
{
    using AutoMapper;

    public interface ICustomMapping
    {
        void CreateMappings(IMapperConfigurationExpression config);
    }
}
