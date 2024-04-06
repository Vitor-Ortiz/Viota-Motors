namespace Viota_Motors.Models
{
    public class Carro
    {
      public int Numero {get; set;}  
      public string Nome {get; set;}  
      public string Descricao {get; set;}  
      public string Cor {get; set;}  
      public List<string> Tipo {get; set;} = [];  
      public string Imagem {get; set;}  
    }
}