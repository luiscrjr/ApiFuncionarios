using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Projeto.Data.Entities;
using Projeto.Services.Models;
using AutoMapper;

namespace Projeto.Services.Profiles
{
    public class EntityToModelProfile: Profile
    {
        public EntityToModelProfile()
        {
            CreateMap<Funcionario, FuncionarioConsultaModel>();
        }
    }
}
