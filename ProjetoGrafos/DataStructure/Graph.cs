﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoGrafos.DataStructure
{
    /// <summary>
    /// Classe que representa um grafo.
    /// </summary>
    public class Graph
    {

        #region Atributos

        /// <summary>
        /// Lista de nós que compõe o grafo.
        /// </summary>
        private List<Node> nodes;

        #endregion

        #region Propridades

        /// <summary>
        /// Mostra todos os nós do grafo.
        /// </summary>
        public Node[] Nodes
        {
            get { return this.nodes.ToArray(); }
        }

        #endregion

        #region Construtores

        /// <summary>
        /// Cria nova instância do grafo.
        /// </summary>
        public Graph()
        {
            this.nodes = new List<Node>();
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Encontra o nó através do seu nome.
        /// </summary>
        /// <param name="name">O nome do nó.</param>
        /// <returns>O nó encontrado ou nulo caso não encontre nada.</returns>
        private Node Find(string name)
        {

            return nodes.Find(no => no.Name.Equals(name));
            
        }

        /// <summary>
        /// Adiciona um nó ao grafo.
        /// </summary>
        /// <param name="name">O nome do nó a ser adicionado.</param>
        /// <param name="info">A informação a ser armazenada no nó.</param>
        public void AddNode(string name)
        {
            AddNode(name, null);
        }

        /// <summary>
        /// Adiciona um nó ao grafo.
        /// </summary>
        /// <param name="name">O nome do nó a ser adicionado.</param>
        /// <param name="info">A informação a ser armazenada no nó.</param>
        public void AddNode(string name, object info)
        {
            nodes.Add(new Node(name, info));
        }

        /// <summary>
        /// Remove um nó do grafo.
        /// </summary>
        /// <param name="name">O nome do nó a ser removido.</param>
        public void RemoveNode(string name)
        {
            nodes.ForEach(no => no.Edges.RemoveAll(e => e.To.Name.Equals(name)));
            nodes.Remove(Find(name));
            //nodes.Remove(nodes.Find(no => no.Edges.RemoveAll(e => e.To.Equals(name))));
        }

        /// <summary>
        /// Adiciona o arco entre dois nós associando determinado custo.
        /// </summary>
        /// <param name="from">O nó de origem.</param>
        /// <param name="to">O nó de destino.</param>
        /// <param name="cost">O cust associado.</param>
        public void AddEdge(string from, string to, double cost)
        {
            Find(from).AddEdge(Find(to), cost);
            //Find(from).Edges.Add(new Edge(Find(from),Find(to),cost));
        }

        /// <summary>
        /// Obtem todos os nós vizinhos de determinado nó.
        /// </summary>
        /// <param name="node">O nó origem.</param>
        /// <returns></returns>
        public Node[] GetNeighbours(string from)
        {
            List<Node> lista = new List<Node>();
            //lista.Add(nodes.ForEach(no => no.Edges.ForEach(e => e.To.Name.Equals(from))));//unfinished
            foreach (Node n in nodes)
                foreach (Edge e in n.Edges)
                    if (e.From.Name.Equals(from))
                        lista.Add(e.To);

            return lista.ToArray();
        }

        /// <summary>
        /// Valida um caminho, retornando a lista de nós pelos quais ele passou.
        /// </summary>
        /// <param name="nodes">A lista de nós por onde passou.</param>
        /// <param name="path">O nome de cada nó na ordem que devem ser encontrados.</param>
        /// <returns></returns>
        public bool IsValidPath(ref Node[] nodes, params string[] path)
        {
            Node a;
            List<Node> lista = new List<Node>();
            lista.Add(Find(path[0]));
            for(int i=1;i<path.Length;)
            {
                a = Find(path[i - 1]);
                if (Find(path[i]) == null && i == path.Length)
                {
                    
                    return true;
                }
                if (GetNeighbours(a.Name).Contains(Find(path[i])))
                {
                    lista.Add(Find(path[i]));
                    i++;
                }
                else return false;
            }
            nodes = lista.ToArray();
            return true;
        }

        #endregion

    }
}
