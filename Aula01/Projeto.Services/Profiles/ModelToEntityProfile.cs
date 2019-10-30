using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Projeto.Data.Entities;
using Projeto.Services.Models;
using AutoMapper;

namespace Projeto.Services.Profiles
{
    public class ModelToEntityProfile : Profile
    {
        public ModelToEntityProfile()
        {
            CreateMap<FuncionarioCadastroModel, Funcionario>();
            CreateMap<FuncionarioEdicaoModel, Funcionario>();
        }
    }
}
