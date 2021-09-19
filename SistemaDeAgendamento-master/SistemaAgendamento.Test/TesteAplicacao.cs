using System;
using Microsoft.EntityFrameworkCore;
using Moq;
using SistemaAgendamento.Core.Commands;
using SistemaAgendamento.Core.Models;
using SistemaAgendamento.Infrastructure;
using SistemaAgendamento.Service.Handlers;
using SistemaAgendamento.Services.Handlers;
using Xunit;

namespace SistemaAgendamento.Test
{
    public class TesteAplicacao
    {
        [Fact]
        static void InclusaoAgendamentos()
        {
            //arrange
            var comando = new CadastrarAgendamento("Parecis", new Sala(1, "302"),
                DateTime.Now, new DateTime(2019, 12, 31));

            var options = new DbContextOptionsBuilder<Infrastructure.DbContext>()
                .UseInMemoryDatabase("DbTarefasContext")
                .Options;
            Infrastructure.DbContext contexto = new Infrastructure.DbContext(options);
            var repo = new RepositorioAgendamento(contexto);

            var handler = new CadastraAgendamentoHandler(repo);

            //act
            handler.Execute(comando);

            //assert
            var tarefa = repo.ObtemAgendamentos(t => t.Titulo == "Parecis");
            Assert.NotNull(tarefa);
        }

        [Fact]
        static void ExcecaoIncluirAgendamento()
        {
            //arrange
            var comando = new CadastrarAgendamento("Parecis",new Sala(1,"302"),
                DateTime.Now, new DateTime(2019,12,31) );

            var mock = new Mock<IRepositorioAgendamento>();
            mock.Setup(r => r.IncluirAgendamento(It.IsAny<AgendamentoModel>()))
                .Throws(new Exception("Houve um erro na inclusao de tarefas"));
            var repo = mock.Object;
            var hardler = new CadastraAgendamentoHandler(repo);

            //act
            CommandResult resultado = hardler.Execute(comando);

            //assert
            Assert.True(resultado.IsSuccess);
        }

        [Fact]
        static void AgendamentoAlteracaoCount()
        {
            //arrange
            var comando = new GerenciaFimAgendamento();

            var mock = new Mock<IRepositorioAgendamento>();
            mock.Setup(r => r.AtualizarAgendamentos(It.IsAny<AgendamentoModel[]>()));
            var repo = mock.Object;

            var hardler = new GerenciaFimAgendamentoHandler(repo);

            //act
            hardler.Execute(comando);

            //assert
            mock.Verify(r => r.AtualizarAgendamentos(It.IsAny<AgendamentoModel>()), Times.AtLeast(1));
            
        }
    }
}
