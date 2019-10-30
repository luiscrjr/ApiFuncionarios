using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Dapper;
using Projeto.Data.Contracts;
using Projeto.Data.Entities;
using System.Linq;

namespace Projeto.Data.Repositories
{
    public class FuncionarioRepository : IFuncionarioRepository
    {
        private readonly string connectionString;

        public FuncionarioRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Inserir(Funcionario obj)
        {
            var query = "insert into funcionario (Nome, Salario, DataAdmissao)"
                        + "values (@Nome, @Salario, @Dataadmissao)";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(query, obj);
            }
        }

        public void Alterar(Funcionario obj)
        {
            var query = "update funcionario set Nome = @Nome, Salario = @Salario, "
                        + "DataAdmissao = @DataAdmissao where IdFuncionario = @IdFuncionario";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(query, obj);
            }
        }

        public void Excluir(int id)
        {
            var query = "delete from funcionario where IdFuncionario = @IdFuncionario";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(query, new { IdFuncionario = id});
            }
        }

        public List<Funcionario> ObterTodos()
        {
            var query = "select * from Funcionario";

            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Query<Funcionario>(query).ToList();
            }
        }

        public Funcionario ObterPorId(int id)
        {
            var query = "select * from Funcionario where IdFuncionario = @IdFuncionario";

            using (var connection = new SqlConnection(connectionString))
            {
                return connection.QuerySingleOrDefault<Funcionario>(query, new { IdFuncionario = id });
            }
        }
    }
}
