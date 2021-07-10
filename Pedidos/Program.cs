using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Pedidos.Domain;
using Pedidos.ValueObjects;

namespace Pedidos
{
    class Program
    {
        static void Main(string[] args)
        {
            // using var db = new Data.ApplicationContext();

            // var exists = db.Database.GetPendingMigrations().Any();
            // if (exists)
            // {
            //     // codigo
            // }

            //InserirDados();
            //InserirDadosEmMassa();
            //CadastrarPedido();
            // ConsultaPedido();
            // AtualizarDados();
            // RemoverRegistro();
        }

        private static void RemoverRegistro()
        {
            using var db = new Data.ApplicationContext();
            // var cliente = db.Clientes.Find(1);
            var cliente = new Cliente { Id = 1 };
            
            db.Remove(cliente);
            db.Clientes.Remove(cliente);
            db.Entry(cliente).State = EntityState.Deleted;
            
            db.SaveChanges();
        }

        private static void AtualizarDados() 
        {
            using var db = new Data.ApplicationContext();

            // var cliente = db.Clientes.Find(1);
            // cliente.Nome = "John doe Updated Again";
            var cliente = new Cliente 
            {
                Id = 1
            };

            var clienteDesconectado = new 
            {
                Nome = "Carry On",
                Telefone = "123456789"
            };

            db.Attach(cliente);
            db.Entry(cliente).CurrentValues.SetValues(clienteDesconectado);
            // db.Clientes.Update(cliente);
            db.SaveChanges();
        }

        private static void ConsultaPedido()
        {
            using var db = new Data.ApplicationContext();
            var pedido = db.Pedido
                .Include(p => p.Itens)
                .ThenInclude(p => p.Produto)
                .ToList();

            Console.WriteLine(pedido.Count);
        }

        private static void CadastrarPedido()
        {
            using var db = new Data.ApplicationContext();

            var cliente = db.Clientes.FirstOrDefault();
            var produto = db.Produtos.FirstOrDefault();

            var pedido = new Pedido 
            {
                ClienteId = cliente.Id,
                IniciadoEm = DateTime.Now,
                FinalizadoEm = DateTime.Now.AddSeconds(30),
                Obersavao = "Pedido Teste",
                Status = StatusPedido.Analise,
                TipoFrete = TipoFrete.SemFrente,
                Itens = new List<PedidoItem>
                {
                    new PedidoItem
                    {
                        ProdutoId = produto.Id,
                        Desconto = 0,
                        Quantidade = 1,
                        Valor = 10,
                    }
                }
            };

            db.Pedido.Add(pedido);
            var registro = db.SaveChanges();

            Console.Write($"Pedido cadastrado: {registro}");
        }

        private static void ConsultarDados()
        {
            using var db = new Data.ApplicationContext();

            // var clientes = db.Clientes.Where(c => c.Id > 0).ToList();
            var clientes = db.Clientes
                .Where(c => c.Id > 0)
                .OrderBy(c => c.Id)
                .ToList();

            foreach(var cliente in clientes) 
            {
                Console.WriteLine($"Consultando Cliente: {cliente.Id}");
                // db.Clientes.Find(cliente.Id);
                db.Clientes.FirstOrDefault(c => c.Id == cliente.Id);
            }
        }

        private static void InserirDados() 
        {
            var produto = new Produto 
            {
                Descricao = "Produto Teste",
                CodigoBarras = "1234567891234",
                Valor = 10m,
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Ativo = true
            };

            using var db = new Data.ApplicationContext();
            db.Produtos.Add(produto);
            // db.Set<Produto>().Add(produto);
            // db.Entry(produto).State = EntityState.Added;
            // db.Add(produto);

            var registros = db.SaveChanges();
            Console.WriteLine($"Total registros: {registros}");
        }

        private static void InserirDadosEmMassa() 
        {
            var produto = new Produto 
            {
                Descricao = "Produto Teste",
                CodigoBarras = "1234567891234",
                Valor = 10m,
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Ativo = true
            };

            var cliente = new Cliente 
            {
                Nome = "John Doe",
                Email = "john@email.com",
                Telefone = "1123456789",
                CEP = "14000000",
                Cidade = "laland",
                Estado = "SP"
            };

            using var db = new Data.ApplicationContext();
            db.AddRange(produto, cliente);

            var registros = db.SaveChanges();
            Console.WriteLine($"Total registros: {registros}");
        }
    }
}
