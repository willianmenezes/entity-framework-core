using Learn_Entity_Core.Data;
using Learn_Entity_Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace Learn_Entity_Core
{
    class Program
    {
        static void Main(string[] args)
        {
            InserirRegistrosEmMassa();
        }

        private static void InserirDados()
        {
            var produto = new Produto
            {
                Ativo = true,
                CodigoDeBarras = "123",
                Descricao = "Produto de testes",
                TipoProduto = ValueObjects.TipoProduto.MercadoriaParaRevenda,
                Valor = 100
            };

            // adicionando pela propriedade que mapeamos no contexto
            using var db = new ApplicationContext();
            db.Produtos.Add(produto);

            //adicionando chamando o DbSet
            db.Set<Produto>().Add(produto);

            // forcando o mapeamento no entry
            db.Entry(produto).State = EntityState.Added;

            //adicionar pelo db
            db.Add(produto);

            //salvando a entidade no banco de dados, antes so estava mapeados.
            db.SaveChanges();
        }

        private static void InserirRegistrosEmMassa()
        {
            var produto = new Produto
            {
                Ativo = true,
                CodigoDeBarras = "123",
                Descricao = "Produto de testes",
                TipoProduto = ValueObjects.TipoProduto.MercadoriaParaRevenda,
                Valor = 100
            };

            var cliente = new Cliente
            {
                CEP = "14150000",
                Cidade = "Serrana",
                Estado = "SP",
                Nome = "Willian",
                Telefone = "16991324774"
            };

            using var db = new ApplicationContext();

            db.AddRange(produto, cliente);
            db.SaveChanges();
        }
    }
}
