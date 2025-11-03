# 📦 INSTRUÇÕES DE INSTALAÇÃO

# 🛠︝ Instruções de Instalação - CATI

Siga os passos abaixo para instalar e executar o projeto CATI localmente.

## ✅ Pré-requisitos

- [.NET SDK 9.0](https://dotnet.microsoft.com/en-us/download)
- Visual Studio Code ou Visual Studio
- Git (opcional)

## 📥 Clonando o projeto

```bash
git clone https://github.com/TacticalCode7/ChamadosIA.git
cd CATI
```

> Ou copie os arquivos manualmente para uma pasta local.

## 📦 Restaurando dependências

```bash
dotnet restore
```

## 🧹 Limpando e compilando

```bash
dotnet clean
dotnet build
```

## ▶︝ Executando o projeto

```bash
dotnet run
```
## 🛠︝ Solução de erros comuns
execultar no Terminal: 
taskkill /F /IM dotnet.exe

depois:
dotnet clean
dotnet build
dotnet run

Acesse no navegador:

```
http://localhost:5000
```

## 🧪 Testando os dashboards

- Cliente: `http://localhost:5000/Cliente/Dashboard`
- Técnico: `http://localhost:5000/Tecnico/Dashboard`

## 🧪 Login de teste
- Informe que há um login de teste disponível:
- Email: tecnico@cati.com
- Senha: 123456

- Email: cliente@cati.com
- Senha: 123456


## 🧰 Dicas úteis

- Use `dotnet watch run` para reiniciar automaticamente após alterações
- Se ocorrer erro de arquivo bloqueado, finalize o processo com:
  ```bash
  taskkill /F /IM ChamadosIA.exe
  ```

## 📞 Suporte

Em caso de dúvidas, entre em contato com a equipe do CATI.



