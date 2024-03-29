﻿using System.Collections.Generic;
using DataLawyer.Dominio.Modelo;
using DataLawyer.Persistencia.Repositorio;

namespace DataLawyer.Servico
{
    public class ServicoDeProcesso
    {
        private static ServicoDeProcesso _instancia = null;
        public static ServicoDeProcesso Instancia => _instancia ?? new ServicoDeProcesso();
        private ServicoDeProcesso() { }

        private RepositorioDeProcesso _persistencia = RepositorioDeProcesso.Instancia;

        public Processo Obtenha(int id) => _persistencia.Obtenha(id);

        public IEnumerable<Processo> Obtenha() => _persistencia.Obtenha();

        public void Grave(Processo processo) => _persistencia.Grave(processo);

        public void Exclua(int id) => _persistencia.Exclua(id);
    }
}