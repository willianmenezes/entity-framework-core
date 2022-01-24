using Learn_Entity_Core.Data;
using Learn_Entity_Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Learn_Entity_Core
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsultaDePedidosComCarregamentoAdiantado();
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

        private static void CarregarDadosAdiantados()
        {
            using var db = new ApplicationContext();

            var cliente = db.Clientes.FirstOrDefault();
            var produto = db.Produtos.FirstOrDefault();

            var pedido = new Pedido()
            {
                ClienteId = cliente.Id,
                IniciadoEm = DateTime.Now,
                FinalizadoEm = DateTime.Now,
                Observacao = "Teste carregamento Adiantado",
                Status = ValueObjects.StatusPedido.Analise,
                TipoFrete = ValueObjects.TipoFrete.SemFrete,
                Itens = new List<PedidoItem>()
                {
                    new PedidoItem()
                    {
                        Desconto = 0,
                        ProdutoId = produto.Id,
                        Quantidade = 10,
                        Valor = 100,
                    }
                }
            };

            db.Pedidos.Add(pedido);
            db.SaveChanges();
        }

        private static void ConsultaDePedidosComCarregamentoAdiantado()
        {
            using var db = new ApplicationContext();

            var pedidos = db.Pedidos
                .Include(p => p.Itens)
                .ThenInclude(i => i.Produto)
                .ToList();
            Console.WriteLine(pedidos.Count);
        }
    }
}
