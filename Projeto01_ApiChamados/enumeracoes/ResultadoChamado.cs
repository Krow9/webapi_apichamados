using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projeto01_ApiChamados.enumeracoes
{
    public enum ResultadoChamado
    {
        CHAMADO_ENVIADO_OK, //pagamento realizado com sucesso
        CHAMADO_JA_REALIZADO, //o pagamento já foi feito
        //CARTAO_INVALIDO, //não existe o cartão informado
        //LIMITE_INDISPONIVEL, //saldo insuficiente no cartão
        //PAGAMENTO_NAO_REALIZADO, //pagamento ainda não realizado (somente para PUT)
        CHAMADO_FINALIZADO    //pagamento já finalizado
    }
}