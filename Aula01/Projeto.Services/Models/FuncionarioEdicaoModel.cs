using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations; //biblioteca de validações

namespace Projeto.Services.Models
{
    public class FuncionarioEdicaoModel
    {
        [Required(ErrorMessage = "Informe o id do funcionário.")]
        public int IdFuncionario { get; set; }

        [MinLength(6, ErrorMessage = "Informe no mínimo {1} caracteres.")]
        [MaxLength(150, ErrorMessage = "Informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Informe o nome do funcionário")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o salário do funcionário")]
        public decimal Salario { get; set; }


        [Required(ErrorMessage = "Informe a data de admissão do funcionário")]
        public DateTime DataAdmissao { get; set; }
    }
}
