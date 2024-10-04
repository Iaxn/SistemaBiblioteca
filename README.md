# 📚 Sistema de Cadastro de Clientes para Biblioteca

## 📖 Descrição
Este programa permite o cadastro de clientes que desejam alugar livros em uma biblioteca. Os usuários podem registrar informações como nome do cliente, nome do livro, preço do aluguel, dias de locação e visualizar os registros.

## 🚀 Tecnologias
- **Linguagem:** C#
- **Framework:** .NET 8

## 🛠️ Funcionalidades
- **Cadastro de Cliente:** Permite registrar um novo aluguel de livro.
- **Revisar Clientes:** Visualiza todos os registros de aluguel.
- **Persistência de Dados:** Armazena os dados em um arquivo `alugueis.txt`, criando o arquivo se ele não existir.

## 📂 Estrutura do Código

### 1. Classe Aluguel
- **Propriedades:**
  - `NomeLivro`: Nome do livro alugado.
  - `NomePessoa`: Nome do locatário.
  - `Preco`: Preço do aluguel.
  - `Dias`: Número de dias que o livro será alugado.
  - `DataAquisicao`: Data de aquisição do livro.

- **Métodos:**
  - `ToString()`: Retorna uma string formatada com os dados do aluguel.
  - `ToCsv()`: Converte os dados do aluguel para o formato CSV.
  - `FromCsv()`: Cria um objeto `Aluguel` a partir de uma linha CSV.

### 2. Funções Principais
- **Main:** Controla o fluxo do programa, exibindo o menu de opções.
- **CadastrarCliente:** Coleta os dados do cliente e do aluguel, validando entradas.
- **RevisarClientes:** Exibe os aluguéis registrados e permite remover um aluguel pelo nome do cliente.
- **SaveAlugueis:** Salva os registros em um arquivo de texto.
- **LoadAlugueis:** Carrega os registros a partir de um arquivo de texto.

## ⚙️ Como Usar
1. Clone o repositório:
   ```bash
   git clone https://github.com/Iaxn/SistemaBiblioteca.git

## 📑 Exemplo de Uso
1. Ao iniciar o programa, você verá um menu como este:
```propeties
Menu:
1 - Cadastrar Cliente
2 - Revisar Clientes
3 - Sair do programa
Escolha uma opção:
```

## 📄 Licença
1. Este projeto, não possui licença

##🤝 Contribuições
Contribuições são bem-vindas! Sinta-se à vontade para abrir issues ou pull requests.
