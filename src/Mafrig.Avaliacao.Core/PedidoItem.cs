namespace Mafrig.Avaliacao.Core
{
    public class PedidoItem : Entidade
    {
        public PedidoItem(Animal animal, int quantidade)
        {
            Animal = animal;
            Quantidade = quantidade;
        }

        public Animal Animal { get; set; }
        public int Quantidade { get; set; }
    }
}
