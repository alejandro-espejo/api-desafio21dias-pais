namespace api_desafio21dias.Servicos
{
    public class AlunoServico
    {
        public static async Task<bool> ValidarAluno(int id)
        {
            using (var http = new HttpClient())
            {
                using (var response = await http.GetAsync($"http://localhost:5259/alunos/{id}"))
                {
                    return response.IsSuccessStatusCode;
                }
            }
        }
    }
}