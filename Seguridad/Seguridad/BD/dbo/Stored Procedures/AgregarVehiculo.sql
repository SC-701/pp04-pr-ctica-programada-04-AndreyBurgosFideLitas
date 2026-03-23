
CREATE   PROCEDURE [dbo].[AgregarVehiculo]
(
    @Id UNIQUEIDENTIFIER = NULL,
    @IdModelo UNIQUEIDENTIFIER,
    @Placa VARCHAR(20),
    @Color VARCHAR(40),
    @Anio INT,
    @Precio DECIMAL(18,2),
    @CorreoPropietario VARCHAR(150),
    @Telefono VARCHAR(30)
)
AS
BEGIN
    SET NOCOUNT ON;

    SET @Id = ISNULL(@Id, NEWID());
    BEGIN TRANSACTION;

    INSERT INTO dbo.Vehiculo
    (
        Id,
        IdModelo,
        Placa,
        Color,
        Anio,
        Precio,
        CorreoPropietario,
        Telefono
    )
    VALUES
    (
        @Id,
        @IdModelo,
        @Placa,
        @Color,
        @Anio,
        @Precio,
        @CorreoPropietario,
        @Telefono
    );

    SELECT @Id AS Id;
    COMMIT TRANSACTION;
END