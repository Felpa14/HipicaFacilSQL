CREATE TABLE [dbo].[Cliente] (
    [ID]       INT            IDENTITY (1, 1) NOT NULL,
    [Nome]     NVARCHAR (MAX) NOT NULL,
    [Email]    NVARCHAR (MAX) NOT NULL,
    [Endereco] NVARCHAR (MAX) NOT NULL,
    [Cpf]      NVARCHAR (MAX) NOT NULL,
    [Telefone] NVARCHAR (MAX) NOT NULL,
    [Cep] NVARCHAR(MAX) NOT NULL, 
    [Cidade] NVARCHAR(MAX) NOT NULL, 
    [Complemento] NVARCHAR(MAX) NULL, 
    CONSTRAINT [PK_Cliente] PRIMARY KEY CLUSTERED ([ID] ASC)
);

