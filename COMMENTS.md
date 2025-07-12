# COMMENTS.md

## 🏗️ Arquitetura Utilizada

O projeto segue uma arquitetura frontend-backend desacoplada, composta por:

- **Frontend:** Desenvolvido com Vue 3 + Vuetify 3, seguindo o padrão de componentes reutilizáveis e rotas baseadas em `vue-router`.
- **Backend:** ASP.NET Core 8, com arquitetura em camadas, endpoints REST e suporte a autenticação via JWT.
- A comunicação entre as camadas é feita via HTTP usando Axios, com baseURL configurado separadamente em um arquivo `api.js`.

A separação clara entre responsabilidades de visualização (Vue) e regras de negócio (ASP.NET Core) permite uma escalabilidade futura e testes isolados.

---

## 📦 Bibliotecas de Terceiros Utilizadas

### Frontend (Vue)
- **Vue 3** – Framework principal para a camada de apresentação
- **Vuetify 3** – Framework de UI com Material Design
- **vue-router** – Gerenciamento de rotas SPA
- **axios** – Cliente HTTP para consumir a API
- **@fortawesome/fontawesome-free** – Ícones visuais
- **vue-the-mask** – Máscara de campos como CPF
- **eslint** – Linter para padronização e qualidade do código

### Backend (ASP.NET Core)
- **ASP.NET Core 8**
- **Entity Framework Core** – ORM para acesso a dados
- **CORS Middleware** – Permite chamadas cross-origin do frontend (localhost:8080)
- **JWT Bearer Authentication** – Segurança e controle de acesso com token

---

## ✅ Funcionalidades Implementadas

- Consulta de alunos com busca por nome
- Cadastro, edição e exclusão de alunos
- Feedback visual (snackbar) com mensagens de sucesso ou erro
- Diálogo de confirmação para exclusão
- Integração com backend seguro via token JWT
- Correção de CORS no backend
- Correções de ESLint e atualização de slots para compatibilidade com Vue 3
- Títulos das colunas na tabela (Registro Acadêmico, Nome, CPF, Ações)
- Botões com ícones e texto: **Editar** e **Excluir**

---

## 🔧 Melhorias Futuras

Se houvesse mais tempo, as melhorias a seguir seriam implementadas:
- Completar a implementação da autorização JWT
- Melhorar a segurança da aplicação incluindo variáveis de ambiente
- Adicionar o banco de dados em um conteiner para melhorar a portabilidade
- Criar autenticação e cadastro de usuários no frontend
- Responsividade total para dispositivos móveis
- Melhorar a organização dos componentes Vue com pastas `views`, `components`, `services`, etc.

---

## ❗ Requisitos Não Entregues

- Filtros mais avançados (como por CPF ou RA)
- Validação robusta de formulário (ex: validação de CPF)
- Layout adaptado para mobile

