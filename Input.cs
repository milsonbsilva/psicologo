using System.Globalization;

internal class Input
{
    Controle_Programa controle_programa = new();

    internal void Menu()
    {
        bool rodando = false;
        while (rodando == false)
        {
            Console.WriteLine("O que você deseja fazer?");
            Console.WriteLine("1 - Cadastrar um novo usuário");
            Console.WriteLine("2 - Ver usuários cadastrados");
            Console.WriteLine("3 - Excluir usuário");
            Console.WriteLine("4 - Modificar registro");
            Console.WriteLine("0 - Fechar o programa");

            string input = Console.ReadLine();

            while (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Comando inválido, por favor, digite um número entre 0 e 4.");
                input = Console.ReadLine();
            }

            switch (input)
            {
                case "0":
                    rodando = true;
                    Environment.Exit(0);
                    break;
                case "1":
                    Add();
                    break;
                case "2":
                    controle_programa.Get();
                    break;
                case "3":
                    Excluir();
                    break;
                case "4":
                    Atualizar();
                    break;
                case "5":
                    //ProcessReport();
                    break;

                default:
                    Console.WriteLine("Comando inválido, por favor, digite um número entre 0 e 4.");
                    break;
            }

        }
    }

    internal string SetPrimeiroNome()
    {
        Console.WriteLine("Qual o primeiro nome do usuário?");
        string input = Console.ReadLine();
        if (input == "0") Menu();
        return input;
    }

    internal string SetSegundoNome()
    {
        Console.WriteLine("Qual o segundo nome do usuário?");
        string input = Console.ReadLine();
        if (input == "0") Menu();
        return input;
    }

    internal string SetEmail()
    {
        Console.WriteLine("Qual o email do usuário?");
        string? input = Console.ReadLine();
        if (input == "0") Menu();
        return input;
    }

    internal string SetCelular()
    {
        Console.WriteLine("Qual o celular do usuário?");
        string input = Console.ReadLine();
        if (input == "0") Menu();
        return input;
    }

    internal string SetSenha()
    {
        Console.WriteLine("Digite a senha");
        string input = Console.ReadLine();
        if (input == "0") Menu();
        return input;
    }

    internal char SetGenero()
    {
        Console.WriteLine("Qual o gênero do usuário? Digite 'M' ou 'F'");
        char input = Convert.ToChar(Console.ReadLine());
        if (input == '0') Menu();
        return input;
    }

    private void Add()
    {
        var primeiroNome = SetPrimeiroNome();
        var segundoNome = SetSegundoNome();
        var email = SetEmail();
        var celular = SetCelular();
        var senha = SetSenha();
        var genero = SetGenero();

        Gerenciador gerenciador = new Gerenciador();

        gerenciador.PrimeiroNome = primeiroNome;
        gerenciador.SegundoNome = segundoNome;
        gerenciador.Email = email;
        gerenciador.Celular = celular;
        gerenciador.Senha = senha;
        gerenciador.Genero = genero;

        controle_programa.Criar(gerenciador);
    }

    private void Excluir()
    {
        controle_programa.Get();
        Console.WriteLine("Por favor, digite o id do usuário que gostaria de excluir. 0 para retornar ao menu inicial");
        string input = Console.ReadLine();

        while (!Int32.TryParse(input, out _) || string.IsNullOrEmpty(input) || Convert.ToInt32(input) < 0);
        {   
            Console.WriteLine("Por favor, selecione um id válido");
            input = Console.ReadLine();
        }

        var id = Convert.ToInt32(input);
        if (id == 0) Menu();
        var gerenciador = controle_programa.GetId(id);

        while (gerenciador.Id == 0)
        {
            Console.WriteLine($"O registro com o id: {id} não existe");
            Excluir();
        }

        controle_programa.Excluir(id);
    }

    private void Atualizar()
    {
        controle_programa.Get();
        Console.WriteLine("Por favor, selecione o id do usuário que deseja atualizar");
        string input = Console.ReadLine();

        while (!Int32.TryParse(input, out _) || string.IsNullOrEmpty(input) || Convert.ToInt32(input) < 0);
        {   
            Console.WriteLine("Por favor, selecione um id válido");
            input = Console.ReadLine();
        }

        var id = Convert.ToInt32(input);
        if (id == 0) Menu();
        var gerenciador = controle_programa.GetId(id);

        while (gerenciador.Id == 0)
        {
            Console.WriteLine($"O registro com o id: {id} não existe");
            Atualizar();
        }

        bool atualizar = true;
        while (atualizar == true)
        {
            Console.WriteLine("Digite 1 para atualizar o primeiro nome");
            Console.WriteLine("Digite 2 para atualizar o segundo nome");
            Console.WriteLine("Digite 3 para o email");
            Console.WriteLine("Digite 4 para o celular");
            Console.WriteLine("Digite 5 para a senha");
            Console.WriteLine("Digite 6 para o gênero");
            Console.WriteLine("Digite s para salvar");
            Console.WriteLine("Digite 0 para voltar ao menu inicial");
            input = Console.ReadLine();

            switch (input)
            { 
                case "1":
                    gerenciador.PrimeiroNome = SetPrimeiroNome();
                    break;
                case "2":
                    gerenciador.SegundoNome = SetSegundoNome();
                    break;
                case "3":
                    gerenciador.Email = SetEmail();
                    break;
                case "4":
                    gerenciador.Celular = SetCelular();
                    break;
                case "5":
                    gerenciador.Senha = SetSenha();
                    break;
                case "6":
                    gerenciador.Genero = SetGenero();
                    break;
                case "s":
                    atualizar = false;
                    break;
                case "0":
                    Menu();
                    atualizar = false;
                    break;
                default:
                    Console.WriteLine("Digite uma opção válida");
                    break;
            }
        }
        controle_programa.Atualizar(gerenciador);
        Menu();

    }

}