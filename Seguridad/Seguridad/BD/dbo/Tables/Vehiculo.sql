CREATE TABLE [dbo].[Vehiculo] (
    [Id]                UNIQUEIDENTIFIER NOT NULL,
    [IdModelo]          UNIQUEIDENTIFIER NOT NULL,
    [Placa]             VARCHAR (20)     NOT NULL,
    [Color]             VARCHAR (40)     NOT NULL,
    [Anio]              INT              NOT NULL,
    [Precio]            DECIMAL (18, 2)  NOT NULL,
    [CorreoPropietario] VARCHAR (150)    NOT NULL,
    [Telefono]          VARCHAR (30)     NOT NULL,
    CONSTRAINT [PK_Vehiculo] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Vehiculo_Modelos] FOREIGN KEY ([IdModelo]) REFERENCES [dbo].[Modelos] ([Id]),
    CONSTRAINT [UQ_Vehiculo_Placa] UNIQUE NONCLUSTERED ([Placa] ASC)
);

