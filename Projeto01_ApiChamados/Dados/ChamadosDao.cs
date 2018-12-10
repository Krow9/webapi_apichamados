using Projeto01_ApiChamados.enumeracoes;
using Projeto01_ApiChamados.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Projeto01_ApiChamados.Dados
{
    public class ChamadosDao
    {
        public IEnumerable<Chamado> BuscarTodos()
        {
            using (var ctx = new ChamadoEntities())
            {
                return ctx.TB_Chamado.ToList();
            }
        }

        //public Chamado BuscarPagamento(int id)
        //{
        //    using (var ctx = new ChamadoEntities())
        //    {
        //        return ctx.TB_Chamado.FirstOrDefault(p => p.Id == id);
        //    }
        //}

        public ResultadoChamado EfetuarPagamento(Chamado chamado)
        {
            using (var ctx = new ChamadoEntities())
            {
                //verifica se o pagamento já foi realizado
                var pagto = ctx.TB_Chamado.FirstOrDefault(p => p.Descricao == chamado.Descricao);
                if (pagto != null)
                {
                    return ResultadoChamado.CHAMADO_JA_REALIZADO;
                }

                ctx.TB_Chamado.Add(chamado);
                ctx.SaveChanges();
                return ResultadoChamado.CHAMADO_ENVIADO_OK;
            }
        }

        //public ResultadoPagamento AlterarPagamento(Pagamento pagamento)
        //{
        //    using (var ctx = new PagamentosEntities())
        //    {
        //        //verifica se o cartão existe
        //        var cartao = ctx.TBCartoes
        //            .FirstOrDefault(c => c.NumCartao.Equals(pagamento.NumCartao));
        //        if (cartao == null)
        //        {
        //            return ResultadoPagamento.CARTAO_INVALIDO;
        //        }

        //        //verifica se o pagamento já foi realizado
        //        var pagto = ctx.TBPagamentos
        //            .Where(c => c.NumPedido.Equals(pagamento.NumPedido));
        //        if (pagto.Count() == 0) //#1
        //        {
        //            return ResultadoPagamento.PAGAMENTO_NAO_REALIZADO;
        //        }

        //        //verifica o status                
        //        if (pagamento.Status == 2) //#2
        //        {
        //            return ResultadoPagamento.PAGAMENTO_FINALIZADO;
        //        }


        //        //verifica se há saldo suficiente no cartão
        //        var listaPagamentos = ctx.TBPagamentos
        //            .Where(c => c.NumCartao.Equals(pagamento.NumCartao)
        //                && c.Status == 1
        //                && !c.NumPedido.Equals(pagamento.NumPedido)); //#3

        //        double total = 0;
        //        if (listaPagamentos.Count() > 0)
        //        {
        //            total = listaPagamentos.Sum(p => p.Valor);
        //        }

        //        if ((total + pagamento.Valor) > cartao.Limite)
        //        {
        //            return ResultadoPagamento.LIMITE_INDISPONIVEL;
        //        }

        //        ctx.Entry<Pagamento>(pagamento).State = EntityState.Modified;
        //        ctx.SaveChanges();
        //        return ResultadoPagamento.PAGAMENTO_OK;
        //    }
        //}

        public Chamado RemoverChamado(int id)
        {
            using (var ctx = new ChamadoEntities())
            {
                var chamado = ctx.TB_Chamado
                    .FirstOrDefault(p => p.ChamadoId == id);
                if (chamado == null)
                {
                    return null;
                }
                ctx.Entry<Chamado>(chamado).State = EntityState.Deleted;
                ctx.SaveChanges();
                return chamado;
            }
        }
    }
}