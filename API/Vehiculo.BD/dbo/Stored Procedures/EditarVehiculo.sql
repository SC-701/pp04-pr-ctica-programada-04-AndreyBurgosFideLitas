
CREATE   PROCEDURE [dbo].[EditarVehiculo]
(
    @Id UNIQUEIDENTIFIER,
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
    BEGIN TRANSACTION;

    UPDATE dbo.Vehiculo
    SET
        IdModelo = @IdModelo,
        Placa = @Placa,
        Color = @Color,
        Anio = @Anio,
        Precio = @Precio,
        CorreoPropietario = @CorreoPropietario,
        Telefono = @Telefono
    WHERE Id = @Id;

    SELECT @Id AS Id;
    COMMIT TRANSACTION;
END