# 🔁 Repetidor de Frases com Streaming

[![Blazor WebAssembly](https://img.shields.io/badge/Blazor-WebAssembly-512BD4?logo=blazor&logoColor=white)](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor)
[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![License](https://img.shields.io/badge/license-MIT-green)](LICENSE)
[![GitHub last commit](https://img.shields.io/github/last-commit/seu-usuario/RepetidorFrases)](https://github.com/seu-usuario/RepetidorFrases)

Uma aplicação **Blazor WebAssembly** que repete uma frase N vezes com suporte a **streaming para grandes volumes** (N > 500), evitando congelamento da interface. Inclui histórico local, exportação de arquivo `.txt` e cópia para área de transferência.

---

## ✨ Funcionalidades

### 🎯 Principal
- **Repetição flexível** – Escolha a frase, o número de repetições (de 1 a 5.000) e o separador entre as repetições (nova linha, vírgula + espaço, espaço ou ponto e vírgula)

### 🔢 Personalização
- **Numeração automática** – Opção para numerar cada linha da repetição (ex: "1 - Olá", "2 - Olá")

### ⚡ Streaming inteligente
- **Geração assíncrona** – Quando o número de repetições for maior que 500, o sistema ativa o modo streaming
- **UI responsiva** – O texto aparece em blocos de 50 repetições, sem congelar a interface
- **Feedback visual** – Spinner animado indicando que a geração está em andamento
- **Cancelamento** – Botão "Limpar" interrompe a geração a qualquer momento

### 📋 Utilitários
- **Copiar resultado** – Copia todo o texto gerado para a área de transferência com um clique
- **Exportar como .txt** – Baixa o resultado em um arquivo de texto para o computador
- **Contador de caracteres** – Exibe o tamanho total do texto gerado em tempo real

### 📜 Histórico local
- **Persistência** – Salva automaticamente as últimas 5 frases utilizadas no navegador
- **Recuperação rápida** – Clique em qualquer item do histórico para recarregar a frase e suas configurações
- **Armazenamento local** – Os dados permanecem salvos mesmo após fechar o navegador

### 🎨 Interface
- **Design moderno** – CSS personalizado sem dependências externas (sem Bootstrap)
- **Responsivo** – Adapta-se automaticamente para dispositivos móveis
- **Animações suaves** – Transições e efeitos hover nos botões e itens do histórico
- **Tema claro** – Cores suaves com gradiente de fundo

### 🛡️ Validações
- **Frase obrigatória** – Impede geração com campo vazio
- **Limite seguro** – Máximo de 5.000 repetições para evitar sobrecarga
- **Cancelamento seguro** – Utiliza `CancellationToken` para interromper gerações em andamento

---

## 🚀 Tecnologias utilizadas

- [Blazor WebAssembly .NET 10](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor) – C# rodando no navegador
- `IAsyncEnumerable<string>` – streaming de dados sem travar a UI
- `localStorage` – persistência do histórico
- `navigator.clipboard` – cópia para área de transferência
- `DotNetStreamReference` – download de arquivos via JavaScript interop
- CSS3 puro – grid responsivo, animações, variáveis CSS

---


---

## 🛠️ Como executar localmente

### Pré-requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Navegador moderno (Chrome, Edge, Firefox)

### Passos

```bash
# 1. Clone o repositório
git clone https://github.com/christianocj/phrase-repeater-blazorwasm
entre no diretorio raiz do projeto

# 2. Restaure os pacotes
dotnet restore

# 3. Execute o projeto
dotnet run

# 4. Abra o navegador em https://localhost:5001 ou na porta correspondente