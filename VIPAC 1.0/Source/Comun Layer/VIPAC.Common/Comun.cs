namespace VIPAC.Common
{
    public class Comun<TValor>
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public TValor Valor { get; set; }
    }
}