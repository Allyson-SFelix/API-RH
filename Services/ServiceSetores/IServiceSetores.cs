﻿using API_ARMAZENA_FUNCIONARIOS.ViewModel.Request;
using API_ARMAZENA_FUNCIONARIOS.ViewModel.Response;

namespace API_ARMAZENA_FUNCIONARIOS.Services.ServiceSetores
{
    public interface IServiceSetores
    {
        public Task<bool> SalvarSetor(SetoresRequest setor);

        public Task<bool> AtualizarSetor(string nome, SetoresRequest setorNovo);

        public Task<bool> RemoverSetor(string nome);

        public Task<SetoresResponse?> PegarSetor(string nome);

        public Task<List<SetoresResponse>> ListarSetores();
    }
}
