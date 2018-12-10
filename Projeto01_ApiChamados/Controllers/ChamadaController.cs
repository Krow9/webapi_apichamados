using Projeto01_ApiChamados.Dados;
using Projeto01_ApiChamados.enumeracoes;
using Projeto01_ApiChamados.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Projeto01_ApiChamados.Controllers
{
    public class ChamadaController : ApiController
    {
        static readonly ChamadosDao dao = new ChamadosDao();

        //HTTP GET - Lista todos os pagamentos
        public IEnumerable<Chamado> GetPagamentos()
        {
            return dao.BuscarTodos();
        }

        //HTTP GET/id - Busca um pagamento especifico
        //public Pagamento GetPagamento(int id)
        //{
        //    return dao.BuscarPagamento(id);
        //}

        //HTTP POST - Inclusão de pagamentos
        public HttpResponseMessage PostChamado(Chamado chamado)
        {
            ResultadoChamado resultado = dao.EfetuarPagamento(chamado);

            if (resultado == ResultadoChamado.CHAMADO_ENVIADO_OK)
            {
                var response = Request.CreateResponse<Chamado>(
                    HttpStatusCode.Created, chamado);

                string uri = Url.Link("DefaultApi", new { id = chamado.ChamadoId });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            else
            {
                string mensagem;

                switch (resultado)
                {
                    case ResultadoChamado.CHAMADO_JA_REALIZADO:
                        mensagem = "Este chamado já foi realizado antes"; break;
                    default:
                        mensagem = "Ocorreu um erro inesperado"; break;
                }


                var erro = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Erro no servidor"),
                    ReasonPhrase = mensagem
                };
                throw new HttpResponseException(erro);
            }
        }

        ////HTTP PUT - Alteração de Pagamento
        //public HttpResponseMessage PutPagamento(int id, Pagamento pagamento)
        //{
        //    pagamento.Id = id;
        //    ResultadoPagamento resultado = dao.AlterarPagamento(pagamento);

        //    if (resultado == ResultadoPagamento.PAGAMENTO_OK)
        //    {
        //        var response = Request.CreateResponse<Pagamento>(
        //            HttpStatusCode.Created, pagamento);

        //        string uri = Url.Link("DefaultApi", new { id = pagamento.Id });
        //        response.Headers.Location = new Uri(uri);
        //        return response;
        //    }
        //    else
        //    {
        //        string mensagem;

        //        switch (resultado)
        //        {
        //            case ResultadoPagamento.CARTAO_INVALIDO:
        //                mensagem = "O cartão informado não existe"; break;
        //            case ResultadoPagamento.LIMITE_INDISPONIVEL:
        //                mensagem = "Limite indisponível no cartão"; break;
        //            case ResultadoPagamento.PAGAMENTO_JA_REALIZADO:
        //                mensagem = "Este pagamento já foi realizado antes"; break;
        //            case ResultadoPagamento.PAGAMENTO_NAO_REALIZADO:
        //                mensagem = "Impossível alterar: pagamento não existe"; break;
        //            case ResultadoPagamento.PAGAMENTO_FINALIZADO:
        //                mensagem = "Impossível alterar: pagamento já finalizado"; break;
        //            default:
        //                mensagem = "Ocorreu um erro inesperado"; break;
        //        }


        //        var erro = new HttpResponseMessage(HttpStatusCode.BadRequest)
        //        {
        //            Content = new StringContent("Erro no servidor"),
        //            ReasonPhrase = mensagem
        //        };
        //        throw new HttpResponseException(erro);
        //    }
        //}

        //HTTP DELETE - Removendo um pagamento
        public HttpResponseMessage DeleteChamado(int id)
        {
            Chamado chamado = dao.RemoverChamado(id);

            if (chamado != null)
            {
                var response = Request.CreateResponse<Chamado>(
                    HttpStatusCode.Created, chamado);

                string uri = Url.Link("DefaultApi", new { id = chamado.ChamadoId });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            else
            {
                var erro = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Erro no servidor"),
                    ReasonPhrase = "Nenhum pagamento a ser removido"
                };
                throw new HttpResponseException(erro);
            }
        }

    }
}
