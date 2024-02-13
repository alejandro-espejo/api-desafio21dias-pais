namespace api_desafio21dias.ModelViews
{
    public record HomeView
    {
        public string Informacao => "Bem vindo ao sistema";

        public List<dynamic> Endpoints => new (){
            new 
            { 
                Item = new { Documentacao = "/swagger" }
            },
            new 
            { 
                Itens = new List<dynamic>() {
                    new { Path = "/pais" },
                },
            }
        };
    }
}