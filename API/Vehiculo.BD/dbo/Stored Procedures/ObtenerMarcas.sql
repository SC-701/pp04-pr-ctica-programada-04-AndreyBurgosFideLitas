
CREATE PROCEDURE dbo.ObtenerMarcas
AS
BEGIN
    SET NOCOUNT ON;

    SELECT Id, Nombre
    FROM dbo.Marcas;
END